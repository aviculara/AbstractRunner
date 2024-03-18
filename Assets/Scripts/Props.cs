using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Props : MonoBehaviour
{
    public GameObject door;
    public GameObject crate;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        Move MoveScript = GetComponent<Move>();
        if (MoveScript != null)
        {
            speed = MoveScript.speed;

        }
        else
        {
            print("Move script could not be found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Portal")
        {
            //speed = 0;
            //y = 0;
            door.transform.DOMoveX(door.transform.position.x - 12, 24f * Time.deltaTime * speed * 2);
            crate.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
