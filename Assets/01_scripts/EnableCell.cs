using UnityEngine;
using System.Collections;

public class EnableCell : MonoBehaviour {

    private Renderer rend;
    private float goodDistance = -65.0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        rend.enabled = false;
        if (CellMove.zLocation > goodDistance)
        {
            rend.enabled = true;
        }
        

        
	
	}
}
