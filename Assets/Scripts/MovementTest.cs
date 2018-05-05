using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    private Transform playerTransform;
    private CharacterController playerCharacterController;
    [SerializeField]
    private Camera playerCam;
    private Transform cameraTransform;

    private Vector3 vector001 = Vector3.forward;
    private Vector3 vector100 = Vector3.right;
    private Vector3 vectorM100 = Vector3.left;
    private Vector3 vector010 = Vector3.up;
    private Vector3 vector0M10 = Vector3.down;

    private float moveSpeed;
    private int rotationSpeed;
    private int gravity;
    private Vector3 moveDirection;

    private void Start()
    {
        playerTransform = this.transform;
        playerCharacterController = this.GetComponent<CharacterController>();
        cameraTransform = playerCam.transform;
        moveSpeed = 10;
        rotationSpeed = 100;
        gravity = 10;
    }

    void Update ()
    {
        moveDirection = vector001 * Input.GetAxis("Vertical");
        playerCharacterController.Move(transform.TransformDirection(moveDirection) * Time.deltaTime * moveSpeed);
        playerCharacterController.Move(transform.TransformDirection(vector100 * Input.GetAxis("Horizontal")) * Time.deltaTime * moveSpeed);

        playerTransform.Rotate(vector010 * Time.deltaTime * rotationSpeed*2 * Input.GetAxis("Mouse X"));
        cameraTransform.Rotate(vectorM100 * Time.deltaTime * rotationSpeed*0.5f * Input.GetAxis("Mouse Y"));
        if (!playerCharacterController.isGrounded)
        {
            playerCharacterController.Move(vector0M10 * Time.deltaTime * gravity);
        }



    }
}
