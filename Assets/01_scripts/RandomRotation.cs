using UnityEngine;
using System.Collections;


public class RandomRotation : MonoBehaviour {

    public float speedRotation;
    private float xSpin;
    private float ySpin;
    private float zSpin;

    // Update is called once per frame
    void Start()
    {
        transform.rotation = Random.rotation;
    }
    void Update () {

        xSpin = Random.Range(0, 360) * speedRotation * Time.deltaTime;
        ySpin = Random.Range(0, 360) * speedRotation * Time.deltaTime;
        zSpin = Random.Range(0, 360) * speedRotation * Time.deltaTime;

        transform.Rotate(xSpin, ySpin, 0) ;
        
	
	}
}
