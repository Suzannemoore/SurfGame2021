using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // allow varaible to be used in this class. 
    Rigidbody2D Rigidbody2DVar;

    // create a SurfaceEffector2D variable
    SurfaceEffector2D surfaceEffect;

    [SerializeField] float torqueAmount = 1f;

    // create a high speed for surfer
    [SerializeField] float fastSpeed = 20f;

    // create a normal speed for surfer
    [SerializeField] float normalSpeed = 6f;

    bool userPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        // We will be using Ridgidbody since is what causes movement in Unity
        Rigidbody2DVar = GetComponent<Rigidbody2D>();

        // Returns the first active loaded object of Type type.
        surfaceEffect = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (userPlaying)
        {
            // if true allow the player to move
            playMovement();
            speedRespond();
        }
      
    }

    public void stopSurfing()
    {
        // set userPlaying to false to stop surfing
        userPlaying = false;
    }

    private void speedRespond()
    {
        // if user pushes up arrow the fast speed will be assigned as the new speed
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffect.speed = fastSpeed;
        }

        // if user pushes down arrow the normal speed will be assigned as the new speed
        else
        {
            surfaceEffect.speed = normalSpeed;
        }
    }

    void playMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // torque to rotate when the user pushes the left arrow. 
            Rigidbody2DVar.AddTorque(torqueAmount);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // useing -torque to allow the user to rotate the other direction. 
            Rigidbody2DVar.AddTorque(-torqueAmount);
        }
    }
}
