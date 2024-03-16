using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject ball;
    public GameObject player;
    public GameObject portal;
    public float speed = 10;
    private int rotation;
    private float pi = 3.14159f;
    private float x;
    // Start is called before the first frame update
    void Start()
    {

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
        portal.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 60);
        x = 0;
        mainCamera.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.A))
        {
            x = 4;
        }

        player.transform.Translate(new Vector3(x, 0, 1) * Time.deltaTime * speed);
        ball.transform.Rotate(new Vector3(1, 0, x) * Time.deltaTime * speed * 360 / (pi * 2.5f));

    }
}
