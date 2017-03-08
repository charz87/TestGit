using UnityEngine;
using System.Collections;

public class LineSight : MonoBehaviour
{

    //------------------------------------
    //How sensitive should we be to sight
    public enum SightSensitivity { STRICT, LOOSE };

    //Sight Sensitivity
    public SightSensitivity Sensitivity = SightSensitivity.STRICT;

    //Can We see Target
    public bool CanSeeTarget = false;

    //FOV
    public float FieldOfView = 45f;

    //Reference to Target
    public Transform Target = null;

    //Reference to Eyes 
    public Transform EyePoint = null;

    //Reference to transform component
    private Transform ThisTransform = null;

    //Reference to Sphere Collider
    private SphereCollider ThisCollider = null;

    //Reference to last know object sightning, if any
    public Vector3 LastKnowSightning = Vector3.zero;

    //is it Clear Line of Sight
    public bool clearLineOfSight;

    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSightning = ThisTransform.position;

    }

    void FixedUpdate()
    {

        RaycastHit hit;

        if (Physics.Raycast(EyePoint.position,(Target.position-EyePoint.position).normalized, out hit, ThisCollider.radius*20f))
        {
            
            
            if (hit.transform.CompareTag("Player"))
                clearLineOfSight = true;
            else
                clearLineOfSight = false;

        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(EyePoint.position, (Target.position - EyePoint.position).normalized);
        
    }

    bool InFOV()
    {
        //Get Direction to target
        Vector3 DirToTarget = Target.position - EyePoint.position; /*Substract Vector Target position minus eyes position*/

        //Get angle between forward and look direction
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);

        //Are we within field of view?
        if (Angle <= FieldOfView)
            return true;

        //Not Within view
        return false;


    }
    void UpdateSight()
    {
        switch (Sensitivity)
        {
            case SightSensitivity.STRICT:
                CanSeeTarget = InFOV() && clearLineOfSight;
                break;

            case SightSensitivity.LOOSE:
                CanSeeTarget = InFOV() || clearLineOfSight;
                break;

        }
    }

    void OnTriggerStay(Collider other)
    {
        UpdateSight();

        //Update last know Sightning
        if (CanSeeTarget)
            LastKnowSightning = Target.position;

        print(CanSeeTarget);
    }
}
