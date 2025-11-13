using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.18f;
    public float  mouseSenvity = 3f;

    float xRotation = 0f;

    CharacterController cc;
     public Transform cam;
    Vector3 velocity;

    bool isGrounded;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        if (cam != null)
            cam = GetComponentInChildren<Camera>()?.transform;
    }
    private void Update()
    {
        HandleLook();
        HandleMove();
    }

    void HandleMove()
    {
        isGrounded = cc.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        cc.Move(move * moveSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenvity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenvity;
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        if (cam != null)
            cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
