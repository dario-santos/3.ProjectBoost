using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    //Game data
    Rigidbody rigidBody;

	// Use this for initialization
	void Start()
    {

        //Não temos que dizer como vamos buscar o rigidbod, apenas que o temos que ir buscar
        rigidBody = GetComponent<Rigidbody>();
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
            print("Rotate A");
            transform.Rotate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            print("Rotate D");

            transform.Rotate(-Vector3.forward);
        }

        if(Input.GetKey(KeyCode.Space)) //Thrust == impulso
        {
            print("Thrust Space");

            rigidBody.AddRelativeForce(Vector3.up);

        }

    }
}
