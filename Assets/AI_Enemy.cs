using UnityEngine;
using System.Collections;

public class AI_Enemy : MonoBehaviour
{
    public float patrolSpeed;
    public float chaseSpeed;

    public enum ENEMY_STATE { PATROL, CHASE, ATTACK };

    public ENEMY_STATE CurrentState
    {
        get { return currentstate; }

        set
        {
            currentstate = value;
            StopAllCoroutines();

            switch (currentstate)
            {
                case ENEMY_STATE.PATROL:
                    StartCoroutine(AIPatrol());
                    break;

                case ENEMY_STATE.CHASE:
                    StartCoroutine(AIChase());
                    break;

                case ENEMY_STATE.ATTACK:
                    StartCoroutine(AIAttack());
                    break;

            }
        }
    }
    //-------------------------------------------------------------
    [SerializeField]
    private ENEMY_STATE currentstate = ENEMY_STATE.PATROL;

    //Reference to Line of Sight Component
    private LineSight ThisLineSight = null;

    //Reference to nav mesh agent
    private NavMeshAgent ThisAgent = null;

    //Reference to Transform
    private Transform ThisTransform = null;

    //Reference to Player Health
    public Health PlayerHealth = null;

    //Reference to player transform
    private Transform PlayerTransform = null;

    //Reference to patrol destination
    public Transform PatrolDestination = null;

    //Reference to Animator
    private Animator anim;

    //Damage amount per second
    public float MaxDamage = 10f;
    //-----------------------------------------------------
    // Use this for initialization
    void Awake()
    {
        ThisLineSight = GetComponent<LineSight>();
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisTransform = GetComponent<Transform>();
        PlayerTransform = PlayerHealth.GetComponent<Transform>();
        anim = GetComponent<Animator>();
        

    }
    //-------------------------------------------------------------
    void Start()
    {
        //Configure Starting State
        CurrentState = ENEMY_STATE.PATROL;

    }
    //-------------------------------------------------------------
    public IEnumerator AIPatrol()
    {
        //loop while patrolling
        while (currentstate == ENEMY_STATE.PATROL)
        {
            //Set strict search
            ThisLineSight.Sensitivity = LineSight.SightSensitivity.STRICT;

            //Set speed for agent
            ThisAgent.speed = patrolSpeed;

            //Walk Animation
            anim.SetBool("IsChase", false);
            anim.SetBool("IsPatrol", true);
            //Chase to patrol position
            ThisAgent.Resume();
            ThisAgent.SetDestination(PatrolDestination.position);

            //wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //if we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget)
            {
                ThisAgent.Stop();
                CurrentState = ENEMY_STATE.CHASE;
                yield break;

            }
            //Wait next frame
            yield return null;
        }

        yield break;
    }

    public IEnumerator AIChase()
    {
        //loop while chasing
        while (currentstate == ENEMY_STATE.CHASE)
        {
            //Set Loose Search
            ThisLineSight.Sensitivity = LineSight.SightSensitivity.LOOSE;

            //Set Speed for Chase
            ThisAgent.speed = chaseSpeed;

            //Run Animation
            anim.SetBool("IsChase", true);
            anim.SetBool("IsPatrol", false);

            //Chase to last know position
            ThisAgent.Resume();
            ThisAgent.SetDestination(ThisLineSight.LastKnowSightning);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //Have we reached destination?
            if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance)
            {
                //Stop Agent
                ThisAgent.Stop();
               

                //Reached destination but cannot see player
                if (!ThisLineSight.CanSeeTarget)
                    CurrentState = ENEMY_STATE.PATROL;
                else //Reached destination and can see player. Reached attacking distance
                    CurrentState = ENEMY_STATE.ATTACK;

                yield break;
            }
            //Wait for next frame
            yield return null;
        }
    }
    public IEnumerator AIAttack()
    {
        //loop while chasing and attacking
        while (currentstate == ENEMY_STATE.ATTACK)
        {
            //Chase to player position
            ThisAgent.Resume();
            ThisAgent.SetDestination(PlayerTransform.position);

            //Set Agent Speed
            ThisAgent.speed = 0f;

            //Animation Attack
            anim.SetBool("IsAttack", true);


            //wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //Has Player run away?
            if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance)
            {
                ThisAgent.Stop();
                //Change back to chase
                CurrentState = ENEMY_STATE.CHASE;
                anim.SetBool("IsAttack", false);
                yield break;
            }
            else
            {
                //Attack
                PlayerHealth.HealthPoints -= MaxDamage * Time.deltaTime;
                    ;
            }

            //Wait until next frame
            yield return null;

        }

        yield break;
    }

    //----------------------------------------------------------------
}
