using UnityEngine;
using System.Collections;

public class CircularMove : MonoBehaviour {

    public float mPosition = 0F;
    public float mRadius = 2.5F;
    private Vector3 mInitialPosition;


    void Start()
    {
        mInitialPosition = transform.position;
    }

    void Update()
    {

        

        mPosition += Time.deltaTime * 1.0f; // 1.0f is the rotation speed in radians per sec.
        Vector3 pos = new Vector3(Mathf.Sin(mPosition), Mathf.Cos(mPosition), 0);
        transform.position = mInitialPosition + pos * mRadius;
        
        
        
    }
}
