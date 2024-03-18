using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject alight;

    // Start is called before the first frame update
    void Start()
    {
        alight.transform.rotation= Quaternion.Euler(50, -66, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
