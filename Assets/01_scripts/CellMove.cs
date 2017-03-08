using UnityEngine;
using System.Collections;

public class CellMove : MonoBehaviour {

    private Transform initialTransform;
    public Transform finalTransform;
    public float speed = 0.02f;
    public static float zLocation;
    private Renderer rend;
    public float timerToMove = 5.0f;
    




    public static void LerpCellPosition(Transform t1, Transform t2, float t)
    {          
            t1.position = Vector3.Lerp(t1.position, t2.position, t);
            t1.rotation = Quaternion.Lerp(t1.rotation, t2.rotation, t);   

    }

    void Start()
    {
        initialTransform = transform;
     

    }
	
	// Update is called once per frame
	void Update () {

        if(!ShipMove.Front)
        {
            timerToMove -= Time.deltaTime;
            speed += Time.deltaTime * speed * 0.002f; ;
            speed = Mathf.Clamp(speed, 0.02f, 0.2f);
            if(timerToMove<=0f)
                LerpCellPosition(initialTransform, finalTransform, speed * Time.deltaTime);
        }

        Debug.Log(speed);
        
        zLocation = transform.position.z;
	
	}
}
