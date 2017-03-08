using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour {

    private CapsuleCollider ThisCollider;
    public bool CanSeeTarget;

	// Use this for initialization
	void Awake ()
    {
        ThisCollider = GetComponent<CapsuleCollider>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.forward,out hit, ThisCollider.radius))
        {
            print(hit.transform.gameObject.name);

            if (hit.transform.CompareTag("Player"))
                CanSeeTarget = true;
            else
                CanSeeTarget = false;
                
        }
	
	}
}
