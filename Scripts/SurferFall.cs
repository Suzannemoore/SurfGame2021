using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurferFall : MonoBehaviour
{
    [SerializeField] float mainDelay = 0.4f;
    [SerializeField] ParticleSystem surferEffect;
    [SerializeField] AudioClip mainFall;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // when the player falls 
        if(collision.tag == "Water")
        {
            // get the public method stopSurfing from the PlayerControl class
            FindObjectOfType<PlayerControl>().stopSurfing();

            // play the crash partical effect 
            surferEffect.Play();

            // get audio for when player falls in water
            GetComponent<AudioSource>().PlayOneShot(mainFall);

            // here we use Invoke to give the player a few seconds before starting over the game. 
            Invoke("NewScene", mainDelay);

            Debug.Log("You fell!");
        }
    }

    void NewScene()
    {
        // using LoadScene method which will load the scene by its name or index in build settings. 
        // This will allow a reload of the scence.
        SceneManager.LoadScene(0);
    }
}
