using UnityEngine;
using System.Collections;

public class Oscilate : MonoBehaviour {

    float timeCounter = 0;
    float x, y, z;

    public float speed = 5;
    public float width = 14;
    public float height = 17;

    

	// Use this for initialization
	void Start () {

        z = transform.position.z;

    }
	
	// Update is called once per frame
	void Update () {

        timeCounter += speed * Time.deltaTime;

        x = Mathf.Cos(timeCounter)*width;
        y = Mathf.Sin(timeCounter)*height;

        transform.position = new Vector3(x, y, z);
       

        
	
	}
}
