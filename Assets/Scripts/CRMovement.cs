using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CRMovement : MonoBehaviour
{
    [SerializeField] bool isGrounded;
    [SerializeField] float jumpForce;
    public float JumpForce
    { 
        get => jumpForce;
        set => jumpForce = value;
    }
    [SerializeField] float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    [SerializeField] float gravityValue;
    public float GravityValue
    {
        get => gravityValue;
        set
        {
            gravityValue = value;
        }
    }
    [SerializeField] Vector3 velocity = new();
    CharacterController controller;
    int currentJumpCount;
    int maxJumpCount = 1;
    public int MaxJumpCount
    {
        get => maxJumpCount;
        set => maxJumpCount = value;
    }
    float jumpBoost = 1;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;

        float dX, dZ = 0; ;

        if (Input.GetKey(KeyCode.D)) dX = 1;
        else if (Input.GetKey(KeyCode.A)) dX = -1;
        else dX = 0;

        if (Input.GetKey(KeyCode.W)) dZ = 1;
        else if (Input.GetKey(KeyCode.S)) dZ = -1;
        else dZ = 0;

        dX *= moveSpeed;
        dZ *= moveSpeed;

        var camDirectionX = Camera.main.transform.right;
        var camDirectionZ = Camera.main.transform.forward;
        camDirectionX.y = 0;
        camDirectionZ.y = 0;
        camDirectionX.Normalize();
        camDirectionZ.Normalize();

        if (!isGrounded) velocity.y -= gravityValue * Time.deltaTime;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentJumpCount <= 0) return;
            velocity.y = jumpForce * jumpBoost;
            currentJumpCount--;
        }
        else if (isGrounded) currentJumpCount = MaxJumpCount;

        controller.Move((velocity + camDirectionX * dX + camDirectionZ * dZ) * Time.deltaTime);
    }
}
