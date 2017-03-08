using UnityEngine;
using System.Collections;

public class FloatingCells : MonoBehaviour {

    public float amplitude;
    public float speed;
    private float tempVal;
    private Vector3 tempPos;

	// Use this for initialization
	void Start () {

        tempVal = transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {

        tempPos.x = transform.position.x;
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.z = transform.position.z;
        transform.position = tempPos;
	
	}
}
