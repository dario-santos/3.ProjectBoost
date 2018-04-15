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

    enum State {Alive, Dying, Trascending };
    State state = State.Alive;


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
        if (state == State.Alive)
        {
            RespondingToThrutInput();
            RespondingToRotateInput();
        }
        
	}


    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
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

        state = State.Trascending;
        Invoke("LoadNextScene", levelLoadDelay); //Carregar nível 2
    }


    private void StartDeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(m_dying);

        dyingParticles.Play();
        mainEngineParticles.Stop();

        state = State.Dying;
        Invoke("LoadFirstScene", levelLoadDelay); //Perma death
    }


    private void LoadNextScene()
    {

        winParticles.Stop();

        SceneManager.LoadScene(1);
        state = State.Alive;

    }


    private void LoadFirstScene()
    {
        dyingParticles.Stop();

        SceneManager.LoadScene(0);
        state = State.Alive;
    }

    private void RespondingToThrutInput()
    {

        if (Input.GetKey(KeyCode.Space)) //Thrust == impulso
        {
            ApplyThrut();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }
        }
    }

    private void ApplyThrut()
    {
        rigidBody.AddRelativeForce(Vector3.up * rcsThrut * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(m_mainEngine);
        }
        mainEngineParticles.Play();
    }


    //Process Input
    private void RespondingToRotateInput()
    {
        //Pode rodar e impulsionar ao mesmo tempo
        rigidBody.freezeRotation = true; //Take manual control of rotation

        float rotationThisFrame = rcsRotation * Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //Resume physics control


    }
}
