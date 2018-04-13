using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	// Use this for initialization
	void Start()
    {
		
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
        }
        else if(Input.GetKey(KeyCode.D))
        {
            print("Rotate D");
        }

        if(Input.GetKey(KeyCode.Space)) //Thrust == impulso
        {
            print("Thrust Space");
        }

    }
}
