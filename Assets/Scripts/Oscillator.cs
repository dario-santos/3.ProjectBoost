using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {


    [SerializeField] Vector3 move  = new Vector3(10f, 10f, 10f  );
    [SerializeField] float periodo = 2f;

    float moveFactor = 0f;
    Vector3 startPosition;


	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}

	
	// Update is called once per frame
	void Update ()
    {

        // sin(2*PI * f) = ONDA SINUSOIDAL
        // sin dá valores de -1 a 0 mas só queremos de 0 a 1
        // logo dividimos por 2 para ficar com resultados entre -0.5 e 0.5 
        // e somamos 0.5 para que fique entre 0 e 1

        if (periodo <= Mathf.Epsilon) { return; }

        float frequency = Time.time / periodo;
        float rawSinWave = Mathf.Sin(frequency * 2 * Mathf.PI);

        moveFactor = (rawSinWave / 2f) + 0.5f;
        Vector3 offset = move * moveFactor;
        transform.position = startPosition + offset;


    }
}
