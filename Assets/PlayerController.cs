using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float MaxSpeed = 10f;
    public float RotSpeed = 5;
    public float JumpForce = 50f;
    public float GroundDist = 0.1f;
    public bool IsGrounded = false;
    private Vector3 Velocity = Vector3.zero;
    public LayerMask GroundLayer;

    float Horz;
    float Vert;

    private Animator anim;

    private Transform ThisTransform = null;
    private CharacterController ThisController = null;

    // Use this for initialization
    void Awake()
    {

        ThisTransform = GetComponent<Transform>();
        ThisController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vert = Input.GetAxis("Vertical");
        Horz = Input.GetAxis("Horizontal");

        //Update Rotation
        ThisTransform.rotation *= Quaternion.Euler(0f, RotSpeed * Horz * Time.deltaTime, 0f);

        //Calculate Move Dir
        Velocity.z = Vert * MaxSpeed;

        //Create Animation
        //anim.SetFloat("Speed",Vert);

        if (Velocity.z != 0)
        {
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }


        //Are we Grounded
        IsGrounded = (DistanceToGround() < GroundDist) ? true : false;

        //Should we jump?
        if (Input.GetAxisRaw("Jump") != 0 && IsGrounded)
            Velocity.y = JumpForce;

        //Apply Gravity
        Velocity.y += Physics.gravity.y * Time.deltaTime;

        //Move
        ThisController.Move(ThisTransform.TransformDirection(Velocity) * Time.deltaTime);

        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack_1");
        }


        //Update Motion
        //ThisTransform.position += ThisTransform.forward * MaxSpeed * Vert * Time.deltaTime;
        //ThisController.SimpleMove(ThisTransform.forward * MaxSpeed * Vert);
    }

    public float DistanceToGround()
    {
        RaycastHit hit;
        float distanceToGround = 0;
        if (Physics.Raycast(ThisTransform.position, -Vector3.up, out hit, Mathf.Infinity, GroundLayer))
            distanceToGround = hit.distance;
        return distanceToGround;
    }





}
