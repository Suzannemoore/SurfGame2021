using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerLine : MonoBehaviour
{
    [SerializeField] float mainDelay = 1f;

    // use the particle system when user bumps surfer's head
    [SerializeField] ParticleSystem waterEffect;

    // create a variable for the finishline 
    [SerializeField] AudioClip mainAudioFinish; 


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        // make use only the surfer can trigger the finish line by adding the player tag
        if(collision.tag == "Player")
        {
            // get the mainAudio and play when the user crosses the finish line. 
            GetComponent<AudioSource>().PlayOneShot(mainAudioFinish);

            // here we use Invoke to give the player a few seconds before starting over the game. 
            Invoke("NewScene", mainDelay);

            // play the partical effect
            waterEffect.Play();

            // show when user has finished the surf
            Debug.Log("Finshed!");

            GetComponent<AudioSource>().Play();
        }

    }

    void NewScene()
    {
        // using LoadScene method which will load the scene by its name or index in build settings. 
        // This will allow a reload of the scence.
        SceneManager.LoadScene(0);
    }
}
