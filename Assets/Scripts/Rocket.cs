using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class Rocket : MonoBehaviour {


    [SerializeField] float rcsRotation = 250f;
    [SerializeField] float rcsThrut = 50f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip m_mainEngine;
    [SerializeField] AudioClip m_dying;
    [SerializeField] AudioClip m_win;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem dyingParticles;
    [SerializeField] ParticleSystem winParticles;

    //Game data
    Rigidbody rigidBody;
    AudioSource audioSource;


    bool canColllide = true;
    bool isTransitioning = false;


	// Use this for initialization
	void Start()
    {
        
        //Não temos que dizer como vamos buscar o rigidbod, apenas que o temos que ir buscar
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
       

    }


    // Update is called once per frame
    void Update()
    {
        //ToDo: Parar som quando morre
        if (!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
        
	}

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            canColllide = !canColllide;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {

            LoadNextLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || !canColllide)
            return;

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                //Don't do nothing
                break;

            case "Finish":
                StartSucessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }


    private void StartSucessSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(m_win);

        winParticles.Play();
        mainEngineParticles.Stop();

        isTransitioning = true;
        Invoke("LoadNextScene", levelLoadDelay); //Carregar nível 2
    }


    private void StartDeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(m_dying);

        dyingParticles.Play();
        mainEngineParticles.Stop();

        isTransitioning = true;
        Invoke("LoadFirstScene", levelLoadDelay); //Perma death
    }


    private void LoadNextLevel()
    {
        winParticles.Stop();

        LoadNextScene();

        isTransitioning = false;
    }

    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        int nextScene = (currentScene + 1) % SceneManager.sceneCountInBuildSettings;
        print(nextScene);
        SceneManager.LoadScene(nextScene);
        
    }

    private void LoadFirstScene()
    {
        dyingParticles.Stop();

        SceneManager.LoadScene(0);
        isTransitioning = false;
    }

    private void RespondToThrustInput()
    {

        if (Input.GetKey(KeyCode.Space)) //Thrust == impulso
        {
            ApplyThrust();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                StopThrust();
            }
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * rcsThrut * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(m_mainEngine);
        }
        mainEngineParticles.Play();
    }


    //Process Input
    private void RespondToRotateInput()
    {
        //Pode rodar e impulsionar ao mesmo tempo
        rigidBody.angularVelocity = Vector3.zero;

        float rotationThisFrame = rcsRotation * Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

    }
}
