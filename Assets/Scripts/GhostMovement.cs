using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    private Transform ghostTransform;

    private float squareDistance;
    private float squareDistanceAttack = 9;
    private float squareDistanceMove = 400;


    float maxHeight = 1.2f;
    float minHeight = 0.8f;

    private bool isMoving = false;
    private bool isLooking = false;
    private bool isAttacking = false;

    private float oscillationSpeed = 0.5f;
    private float moveSpeed = 4f;
    private float attackSpeed = 10f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("TagPlayer");
        playerTransform = player.transform;
        ghostTransform = this.transform;

    }
    private void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(GhostVerticalOscillation());
        StartCoroutine(CheckDistance());
    }

    private void Update()
    {
        //if(Input.GetMouseButton(1))
        //{
        //    print(squareDistance);
        //}

    }

    private float SquareDistancePlaneXZ(Vector3 pointA, Vector3 pointB)
    {
        float squareDistance = (pointA.x - pointB.x) * (pointA.x - pointB.x)
              + (pointA.z - pointB.z) * (pointA.z - pointB.z);
        return squareDistance;
    }

    private IEnumerator GhostVerticalOscillation()
    {
        float average = 1.5f;
        float amplitude;
        while (true)

        {
            amplitude = Random.Range(0.2f, 0.8f);
            while (ghostTransform.position.y < average + amplitude)
            {
                transform.Translate(Vector3.up * oscillationSpeed * Time.deltaTime);
                yield return null;
            }
            while (ghostTransform.position.y > average - amplitude)
            {
                transform.Translate(Vector3.down * oscillationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }


    private IEnumerator CheckDistance()
    {
        while (true)
        {
            squareDistance = SquareDistancePlaneXZ(playerTransform.position, ghostTransform.position);

            if (squareDistance >= squareDistanceMove)
            {
                isLooking = false;
                isMoving = false;
                isAttacking = false;
            }
            else if (squareDistance <= squareDistanceAttack)
            {
                isMoving = false;
                if (!isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(AttackTarget());
                }
            }
            else if (squareDistance > squareDistanceAttack && squareDistance < squareDistanceMove )
            {
                if (!isLooking)
                {
                    isLooking = true;
                    StartCoroutine(LookAtThePlayer());
                }

                if (!isMoving)
                {
                    isMoving = true;
                    StartCoroutine(MoveToTheTarget());
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveToTheTarget()
    {
        while (true)
        {
            if (!isMoving)
            {
                break;
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    private IEnumerator LookAtThePlayer()
    {
        while (true)
        {
            if (!isLooking)
            {
                break;
            }
            ghostTransform.LookAt(playerTransform);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator AttackTarget()
    {
        Vector3 startPoint;
        Vector3 targetPoint;
        float squareDistanceStep = 0.009f;

        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!isAttacking)
            {
                yield break;
            }
            startPoint = ghostTransform.position;
            targetPoint = playerTransform.position;
            while (true)
            {
                transform.position = Vector3.MoveTowards(ghostTransform.position, targetPoint, Time.deltaTime * attackSpeed);
                if (squareDistanceStep > SquareDistancePlaneXZ(targetPoint, ghostTransform.position))
                {
                    break;
                }
                yield return null;
            }
            while (true)
            {
                transform.position = Vector3.MoveTowards(ghostTransform.position, startPoint, Time.deltaTime * attackSpeed);
                if (squareDistanceStep > SquareDistancePlaneXZ(startPoint, ghostTransform.position))
                {
                    break;
                }
                yield return null;
            }
        }
    }

}
