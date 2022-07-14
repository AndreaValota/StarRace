using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    public Vector3 lastPos;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] public Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerBody.position, -transform.up, out hit))
        {
            PlayerBody.useGravity = false;
            PlayerBody.MovePosition(new Vector3(GetComponent<Rigidbody>().position.x, hit.point.y + 1, GetComponent<Rigidbody>().position.z));
            lastPos = hit.point;
        }
        else
        {
            PlayerBody.useGravity = true;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") >= 0f)
        {
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }
        else
        {
            PlayerMovementInput = Vector3.zero;
        }

        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

    }

    private void MovePlayerCamera()
    {
        //xRot = PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        //PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

}