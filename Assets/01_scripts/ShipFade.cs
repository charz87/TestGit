using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipFade : MonoBehaviour {

    public Image myImage;
    private float myAlpha = 0;
    private float alphaTube = 0;
    public float speedFade = 0.5f;
    public float speedTube = 0.2f;
    private Color c;
    public GameObject lightTube;
    public bool fadeDone = false;
    


    // Use this for initialization
    void Start() {

        
        myImage = GetComponent<Image>();
        c = myImage.color;
       

       
	}

    // Update is called once per frame
    void FixedUpdate() {

        lightTube.GetComponent<Renderer>().material.SetFloat("_Opacity", alphaTube);

        myAlpha -= Time.deltaTime * speedFade;

        myAlpha = Mathf.Clamp(myAlpha, 0f, 1f);



        Debug.Log(alphaTube);

        c.a = myAlpha;
        DoFade(fadeDone);
       
       
    }

    public void DoFade(bool done)
    {
        if(!done)
        {
            FadeIn();
        }
        if(done)
        {
            FadeOut();
        }

    }

    

    public void FadeIn()
    {
        Debug.Log("Increasing Alpha");
        alphaTube += Time.deltaTime * speedTube;

        alphaTube = Mathf.Clamp(alphaTube, 0f, 1f); 

        if(alphaTube == 1)
        {
            fadeDone = !fadeDone;
            ShipMove.Front = !ShipMove.Front;
            myAlpha = 1;




        }
            
        myImage.color = c;

            
    }
    public void FadeOut()
    {
        Debug.Log("Decreasing Alpha");
        alphaTube -= Time.deltaTime * speedTube;

        alphaTube = Mathf.Clamp(alphaTube, 0f, 1f);

        myImage.color = c;


    }

}
