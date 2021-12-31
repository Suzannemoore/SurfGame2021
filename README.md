# SurfGame2021
Surfing game created using C# and Unity 2021

Surfer Game 2021

Technical Requirements: Knowledge of C#, Unity Hub, Visual Studio or Visual Studio Code, Windows 11.

Main idea: The main idea of this project is to create a project in unity that will allow the user to jump while using side scrolling. This project is created not only for fun but to increase our learning in C# while becoming more confident in game development.  

We will be using Sprite shaping tool to create the layout for the game. This game requires the use of Edge Collider 2D to allow collision for the game. To shape the ocean in this game we will use the Edit Spline to move the nodes to shape our ocean by adding waves and straight paths. The game will also include particle effect or particle system to interact with the user while the surfer is in motion. 


![Picture1](https://user-images.githubusercontent.com/62191363/147821362-37b26085-ef52-4c1e-8a6d-087831d55b58.png)

For the main camera we will be adding a Cinemachine brain that will handle most of the logic for us while using only one camera. We will need to add a new package in the Packages: In Projects and select Packages: Unity Registry to add a new package called Cinemachine after clicking install. The Cinemachine will not create a new camera, but it will direct a single Unity camera for snap shots with the Virtual Cameras that will change and rotate the Unity camera. 

We will then right click on the Hierarchy on Unity and select the Cinemachine and add the Virtual Camera. Make sure to point to the sprite you want to follow and update the Screen X. 

When setting up the surfer for the game we will download a transparent image and under the parent class Surfer. A capsule collider 2D is added to the parent surfer. The direction is changed to horizontal.

To have reaction for when the surfer was crashed, we have added the Circle Collider 2D, changed the offset, and enable is trigger in the box collider component.  

To allow our surfer to move in this game we will be using a Surface Effector 2D. When adding the component Surface Effector 2D we will select the used by effector on the Edge Collider 2D. On the Surfer Rigidbody 2D the collision detection will be set to Continuous to make sure the surfer does not fall through the main sprite shape(ocean). 

To allow player control we will open a C# script and access Rigidbody2D and access torque to rotate when the user pushes the left arrow key.

    // allow varaible to be used in this class. 
        Rigidbody2D Rigidbody2DVar;

    [SerializeField] float torqueAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // We will be using Ridgidbody since is what causes movement in Unity
        Rigidbody2DVar = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // torque to rotate when the user pushes the left arrow. 
            Rigidbody2DVar.AddTorque(torqueAmount);
        }
    }
For this game we will create a end point to restart the game or lose when the surfer falls into the water. We will create a finish line in the water by adding a capsule 2D sprite and a circle 2D sprite as the child. A box collider 2D with a trigger will be added to create a collision for the user to cross the finish line.

To make sure to only have the surfer trigger the finish line we will add the tag Player for the Surfer. 

    void OnTriggerEnter2D(Collider2D collision)
    {
        // make use only the surfer can trigger the finish line by adding the player tag
        if(collision.tag == "Player")
        {
            // show when user has finished the surf
            Debug.Log("Finshed!");
        }

    }
    
To allow the player to fall we will add the tag Water to the Sprite that was created for the ocean.

    void OnTriggerEnter2D(Collider2D collision)
    {
        // when the player falls 
        if(collision.tag == "Water")
        {
            Debug.Log("You fell!");
        }
    }
    
To allow the user to have a reload of the scene, we will inport UnityEngine.SceneManagement to have access to the method SceneManger to restart the scene. We would also create a new scene and remove the samplescene. 

using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // make use only the surfer can trigger the finish line by adding the player tag
        if(collision.tag == "Player")
        {
            // using LoadScene method which will load the scene by its name or index in build settings. 
            // This will allow a reload of the scence.
            SceneManager.LoadScene(0);
        }

    }
}

We will be using an Invoke() for example 
  - public void Invoke(string methodName, float time);
  
This will give our play a few seconds before starting a new game. 

using UnityEngine;
using UnityEngine.SceneManagement;

    public class SurferFall : MonoBehaviour
    {
        [SerializeField] float mainDelay = 0.4f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // when the player falls 
        if(collision.tag == "Water")
        {
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

For the particle effect will be adding a water effect game object and component Particle System then change the color to a blue/white. The shape will be change to a sphere, the start lifetime will be between 0.3 seconds and 1 second. 

To create a particle effect for our player we copy the Water Effect and make a copy and place it under the Surfer. In the C# script WinnerLine we will create a new reference for the particle system and name it waterEffect. We will trigger are particle system by invoking the effect by using the Play() method and making sure the Play On Awake is uncheck on the Water Effect Inspector. We will do the same for the Surfer Effect when the surfer bumps its head. 

    // use the particle system when user bumps surfer's head
    [SerializeField] ParticleSystem waterEffect;


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        // make use only the surfer can trigger the finish line by adding the player tag
        if(collision.tag == "Player")
        {
            // here we use Invoke to give the player a few seconds before starting over the game. 
            Invoke("NewScene", mainDelay);

            // play the partical effect
            waterEffect.Play();

            // show when user has finished the surf
            Debug.Log("Finshed!");

        }

    }

Since we have most of our game complete, we will now allow the player to have high speed and a normal speed by allowing our players to use the up arrow to increase speed and down arrow to decrease speed. 

    // create a high speed for surfer
    [SerializeField] float fastSpeed = 20f;

    // create a normal speed for surfer
    [SerializeField] float normalSpeed = 6f;


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
        playMovement();
        speedRespond();
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

Another particle system will be used to add a water effect for our surfer. The surfer is on the water the water particles will show to the user. If the surfer is in the air the water particles will not show.

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

We will be adding sound effects using an AudioScource that is used for playing back sounds. This will be done by assigning an AudioSource to a GameObject and attach an audio clip in the Audio Source. We will be adding the component Audio Source to the surfer and update our C# script SurferFall.cs. We will be using AudioClip that is a container for audio data. We will get the component audio source and use the method PlayOneShot() which will play a sound once through the audio source. We then must add the audio by selecting Finish Line and adding the correct audio under Winner Line Script. 

NOTE: To get these WAV file sounds for free we used https://freesound.org/ (Account used be created). 

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

We will do the same for when the player crashes into the water.

    [SerializeField] AudioClip mainFall;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // when the player falls 
        if(collision.tag == "Water")
        {
            // play the crash partical effect 
            surferEffect.Play();

            // get audio for when player falls in water
            GetComponent<AudioSource>().PlayOneShot(mainFall);

            // here we use Invoke to give the player a few seconds before starting over the game. 
            Invoke("NewScene", mainDelay);

            Debug.Log("You fell!");
        }
    }
    
    


https://user-images.githubusercontent.com/62191363/147821684-49f4287a-4b93-43b5-9bcf-db5d231dd36a.mp4

