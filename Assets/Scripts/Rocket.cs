using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    //Game data
    Rigidbody rigidBody;
    AudioSource audioSource;

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
        ProcessInput();
	}


    //Process Input
    private void ProcessInput()
    {
        //Pode rodar e impulsionar ao mesmo tempo

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }

        if (Input.GetKey(KeyCode.Space)) //Thrust == impulso
        {

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up);

        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

        }

    }
}
