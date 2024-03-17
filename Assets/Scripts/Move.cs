using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    
    public GameObject player;
    //public GameObject portal;
    public float speed = 10;
    public Positions position;
    public GameObject door;

    private GameObject[] portals;
    private int rotation;
    private float pi = 3.14159f;
    private float x;
    private bool moving = false;
    private int orientation = 1; //-1 on turning
    
    /*
    private bool onLeft = false;
    private bool onRight = false;
    private bool onMid = false;
    */
    // Start is called before the first frame update
    void Start()
    {
        position = Positions.OnMid;
        orientation = 1;
        portals = GameObject.FindGameObjectsWithTag("Rotate");
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
        //1m -> 360/2pi*r
        //pi = 3.14159
        /*
        if(Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            ball.transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * speed * 360 / (pi * 2.5f));
        }
        */
        foreach (GameObject portal in portals)
        {
            portal.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 60);
        }
        x = 0;

        if (Input.GetKeyDown(KeyCode.A) && position != Positions.OnLeft  && !moving)
        {
            if(position == Positions.OnMid)
            {
                position = Positions.OnLeft;
            }
            else if (position == Positions.OnRight)
            {
                position = Positions.OnMid;
            }
            transform.DOMoveX(transform.position.x - 4 * orientation, 0.25f).OnComplete(stopMove);
            moving = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) && position != Positions.OnRight && !moving)
        {
            if (position == Positions.OnMid)
            {
                position = Positions.OnRight;
            }
            else if (position == Positions.OnLeft)
            {
                position = Positions.OnMid;
            }
            transform.DOMoveX(transform.position.x + 4 * orientation, 0.25f).OnComplete(stopMove);
            moving = true;
        }

        player.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        transform.Rotate(new Vector3(1, 0, x) * Time.deltaTime * speed * 360 / (pi * 2.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Portal")
        {
            //speed = 0;
            player.transform.position = new Vector3(-14, player.transform.position.y, player.transform.position.z);
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector3(-14, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            orientation = -1;
            door.transform.DOMoveX(door.transform.position.x - 12, 24f * Time.deltaTime * speed * 2);
        }
        else if (other.tag == "Respawn")
        {
            //speed = 2;
            
        }
        else
        {
            speed = 0;
        }
        
    }

    private void stopMove()
    {
        moving = false;
    }
}
