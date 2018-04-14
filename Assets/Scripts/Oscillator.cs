using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {


    [SerializeField] Vector3 move;
    [Range(0, 1)] [SerializeField] float moveFactor;

    Vector3 startPosition;


	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}

	
	// Update is called once per frame
	void Update ()
    {

        transform.position = startPosition + (move * moveFactor);

	}
}
