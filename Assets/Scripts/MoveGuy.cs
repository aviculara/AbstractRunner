using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MoveGuy : MonoBehaviour
{
    public GameObject player;
    //public GameObject portal;
    public float speed = 10;
    public Positions position;
    public int sceneMultiplier = 1;
    public float sideMove=3.5f;
    public float sideTime=0.45f;
    public float jumpDist;
    public float jumpTime;
    public float jumpForce;

    [Header("Debug Params")]
    public float max = 23;
    public float zaxis = 90;

    private GameObject[] portals;
    private GameObject[] coins;
    //private float y=0;
    private bool moving = false;
    private bool jumping = false;
    private int orientation = 1; //-1 on turning
    public int currentSceneIndex;

    public GameManager myGameManager;

    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider playercollider;
    // Start is called before the first frame update
    void Start()
    {
        position = Positions.OnMid;
        orientation = 1;
        //y = 0;
        portals = GameObject.FindGameObjectsWithTag("Rotate");
        coins = GameObject.FindGameObjectsWithTag("Coin");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playercollider = GetComponent<CapsuleCollider>();
        if (currentSceneIndex == 1)
        {
            orientation = -1;
            sceneMultiplier = -1;
        }
    }
    public enum Positions
    {
        OnMid,
        OnLeft,
        OnRight
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject portal in portals)
        {
            portal.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 60);
        }

        foreach (GameObject coin in coins)
        {
            coin.transform.Rotate(new Vector3(0, max, zaxis) * Time.deltaTime);
        }
        //x = 0;

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && position != Positions.OnLeft && !moving)
        {
            if (position == Positions.OnMid)
            {
                position = Positions.OnLeft;
            }
            else if (position == Positions.OnRight)
            {
                position = Positions.OnMid;
            }
            transform.DOMoveX(transform.position.x - sideMove * orientation, sideTime).OnComplete(stopMove);
            moving = true;
            animator.SetTrigger("LeftTrig");
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && position != Positions.OnRight && !moving)
        {
            if (position == Positions.OnMid)
            {
                position = Positions.OnRight;
            }
            else if (position == Positions.OnLeft)
            {
                position = Positions.OnMid;
            }
            transform.DOMoveX(transform.position.x + sideMove * orientation, sideTime).OnComplete(stopMove);
            moving = true;
            animator.SetTrigger("RightTrig");
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !jumping)
        {

            //transform.DOMoveY(transform.position.y + jumpDist , jumpTime).OnComplete(Descend).SetEase(Ease.Linear);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //jumping = true;
            //moving = true;
            animator.SetTrigger("Jump");
            //transform.DOJump(transform.position, jumpDist, 1, jumpTime);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            jumping = true;
            //playercollider.center = new Vector3()
            playercollider.center = new Vector3(0.05f, 2.11f, -0.18f);
        }
        else
        {
            playercollider.center = new Vector3(0.05f, 0.83f, -0.18f);
            jumping = false;
        }

        player.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed * sceneMultiplier);
    }
    private void stopMove()
    {
        moving = false;
    }

    private void Descend()
    {
        //transform.DOMoveY(transform.position.y - jumpDist, jumpTime).SetEase(Ease.Linear);
        jumping = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            print("yep");
            //speed = 0;
            //y = 0;
            player.transform.position = new Vector3(-14, player.transform.position.y, player.transform.position.z);
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector3(-14, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            orientation = -1;
        }
        else if (other.CompareTag("Coin"))
        {
            myGameManager.IncreaseScore(1);
            other.gameObject.SetActive(false);

        }
        /*
        else if (other.CompareTag("Ramp"))
        {
            y = 0.753554f;
        }
        */
        else if (other.CompareTag("Finish"))
        {
            myGameManager.Win();
        }
        else
        {
            speed = 0;
            animator.SetTrigger("Fall");
            StartCoroutine(LoseAnim());
        }

    }

    IEnumerator LoseAnim()
    {
        yield return new WaitForSeconds(2);
        myGameManager.Lose();

    }

}
