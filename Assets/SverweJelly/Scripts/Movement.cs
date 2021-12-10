using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float screenWidth;

    public float forwardSpeed;
    [SerializeField] float rotateSpeed;
    public bool ControlsOn;

    public float maxRightPos;
    public float maxLeftPos;
    [SerializeField] Transform rootObject;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float sensitivity;
    [SerializeField] private float sideLimit;
    private float curHor, lastHor = 0;
    private float delta;
    private float targetPoint;

    [SerializeField] float jumpForce;
    [SerializeField] GameObject rootRB;

    public float distanceToCheck = 0.5f;
    public bool isGrounded;

    private void Awake()
    {
        screenWidth = Screen.width;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.fixedDeltaTime;
    }

    public void JoystickOnDrag()
    {
        curHor = joystick.Horizontal;
        delta = curHor - lastHor;
        targetPoint = rootObject.position.x + delta * sensitivity;
        if (Mathf.Abs(targetPoint) <= sideLimit)
        {
            transform.position = new Vector3(transform.position.x + delta * sensitivity, transform.position.y, transform.position.z);
        }
        lastHor = curHor;
    }

    public void JoystickPointerUp()
    {
        lastHor = 0;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rootRB.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }
    }

    void CheckGround()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheck))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}