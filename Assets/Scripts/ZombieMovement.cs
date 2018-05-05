using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent zombieNavMesh;
    private Transform zombieTransform;
    private Transform playerTransform;

    [SerializeField] GameObject bat;
    private Transform batTransform;

    private bool isMoving = false;
 //   private bool isLooking = false;
    private bool isAttacking = false;


    private float walkSpeed = 3f;
    private float runSpeed = 6f;
    private float attackSpeed = 0.5f;
    private float idleSpeed = 0f;

    private float squareDistance;
    private float squareDistanceAttack = 4;
    private float squareDistanceWalk = 625;
    private float squareDistanceRun = 25;

    private void Awake()
    {
        zombieNavMesh = this.GetComponent<NavMeshAgent>();
        zombieTransform = this.transform;
        playerTransform = GameObject.Find("Player").transform;
        batTransform = bat.transform;
    }


    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            print(zombieNavMesh.destination);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(CheckDistance());
    }



    private float SquareDistancePlaneXZ(Vector3 pointA, Vector3 pointB)
    {
        float squareDistance = (pointA.x - pointB.x) * (pointA.x - pointB.x)
              + (pointA.z - pointB.z) * (pointA.z - pointB.z);
        return squareDistance;
    }


    private IEnumerator CheckDistance()
    {
        while (true)
        {


            squareDistance = SquareDistancePlaneXZ(playerTransform.position, zombieTransform.position);

            if (squareDistance <= squareDistanceAttack)
            {
                zombieNavMesh.isStopped = true;
                print(" <= squareDistanceAttack");
                if (!isAttacking)
                {
                    print(" <= squareDistanceAttack  !isAttacking");
                    isAttacking = true;
                        StartCoroutine(AttackTarget());
                   
                    zombieNavMesh.speed = attackSpeed;
                }
                isMoving = false;
            }
            else if (squareDistance <= squareDistanceRun)
            {
                isAttacking = false;
                zombieNavMesh.isStopped = false;
                if (!isMoving)
                {
                    print(" <= squareDistanceRun");
                    isMoving = true;
                    StartCoroutine(MoveToTheTarget());
                    zombieNavMesh.speed = runSpeed;
                }
            }
            else if (squareDistance <= squareDistanceWalk)
            {
                zombieNavMesh.isStopped = false;
                isAttacking = false;
                print(" <= squareDistanceWalk");
                if (!isMoving)
                {
                    isMoving = true;
                    StartCoroutine(MoveToTheTarget());
                    zombieNavMesh.speed = walkSpeed;
                }
            }
            else 
            {
                zombieNavMesh.isStopped = true;
                isAttacking = false;
                isMoving = false;
                print(" <= squareDistanceWalk");
            }
            yield return null;
        }
    }



    private IEnumerator MoveToTheTarget()
    {
        print("go to the target");
        while (true)
        {
            if (!isMoving)
            {
                yield break;
            }
            zombieNavMesh.SetDestination(playerTransform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private IEnumerator AttackTarget()
    {
        
        float ttt = 0;// сделать по другому
        StartCoroutine(LookAtPlayer());
        while (true)
        {

            if (!isAttacking)
            {
                yield break;
            }
            while (ttt < 0.25)
            {
                ttt += Time.deltaTime;

                batTransform.Rotate(Vector3.up, Time.deltaTime * (-360));
                yield return null;

            }
            while (ttt < 0.5)
            {
                ttt += Time.deltaTime;

                batTransform.Rotate(Vector3.up, Time.deltaTime * 360);
                yield return null;

            }
            ttt = 0;
            
            yield return new WaitForSeconds(0.5f);
          //  StopCoroutine(LookAtPlayer());
        }
    }
 
    private IEnumerator LookAtPlayer()
    {
        while(true)
        {
            if (!isAttacking)
            {
                yield break;
            }
            zombieTransform.LookAt(playerTransform);
        yield return null;
        }
    }
 
}
