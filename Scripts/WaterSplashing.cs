using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashing : MonoBehaviour
{
    // create a variable for our water splash partical system
    [SerializeField] ParticleSystem waterSplash;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // play particals while surfing the water
        waterSplash.Play();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // when surfer is not on water stop
        if(collision.gameObject.tag == "Water")
        {
            // stop particals when in the air
            waterSplash.Stop();
        }
    }
}
