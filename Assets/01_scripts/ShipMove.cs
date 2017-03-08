using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

    public float speed = 180f;
    public static bool Front = true;
    private Quaternion myRotation;



	// Update is called once per frame
	void Update () {

        if (!Front) // I´m seeing Cell
        {
            myRotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation,myRotation, speed*Time.deltaTime);
            

        }

        if (Front) // I´m seeing Earth
        {
            myRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, myRotation, speed * Time.deltaTime);
           

        }


    }
}
