using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float acceleration = 10f;
    public float turnSpeed = 10f;
    
    public Rigidbody myRigidbody;
    public Transform controlDirection;
    public Transform displayObj;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myRigidbody.velocity += controlDirection.forward.normalized * acceleration * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            myRigidbody.velocity -= controlDirection.forward.normalized * acceleration * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            controlDirection.RotateAround(controlDirection.position, Vector3.up, turnSpeed * -Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            controlDirection.RotateAround(controlDirection.position, Vector3.up, turnSpeed * Time.deltaTime);
        }

        displayObj.position = myRigidbody.transform.position;
        displayObj.forward = controlDirection.forward;
    }
    
    
}