using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public float speed = 10;
    private int rotation;
    private float pi = 3.14159f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1m -> 360/2pi*r
        //pi = 3.14159
        player.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        ball.transform.Rotate(new Vector3(1,0,0) * Time.deltaTime * speed * 360 / (pi * 2.5f));
        
    }
}
