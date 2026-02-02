using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CRMovement : MonoBehaviour
{
    CharacterController controller;
    float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    bool isGrounded;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;

        float dX, dZ;

        if (Input.GetKey(KeyCode.W)) dX = 1;
        else if (Input.GetKey(KeyCode.S)) dX = -1;
        else dX = 0;

        if (Input.GetKey(KeyCode.D)) dZ = 1;
        else if (Input.GetKey(KeyCode.A)) dZ = -1;
        else dZ = 0;

        controller.Move(new Vector3(dX * moveSpeed, 0, dZ * moveSpeed));
    }
}
