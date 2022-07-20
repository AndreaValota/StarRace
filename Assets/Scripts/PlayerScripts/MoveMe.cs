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
        if (Physics.Raycast(PlayerBody.position, -PlayerBody.transform.up, out hit) && hit.transform.tag != "IgnoreSurface")
        {
            PlayerBody.useGravity = false;
            PlayerBody.MovePosition(hit.point + hit.normal); //GetComponent<Rigidbody>().position
            PlayerBody.MoveRotation(Quaternion.FromToRotation(PlayerBody.transform.up, hit.normal) * PlayerBody.rotation);
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
        if(PlayerBody.useGravity == true )
            PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        else
            PlayerBody.velocity = new Vector3(MoveVector.x, MoveVector.y, MoveVector.z);
    }

    private void MovePlayerCamera()
    {
        //xRot = PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        //PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

}