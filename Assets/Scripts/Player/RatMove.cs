using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    AnimalSwitch aS;

    [Header("Horizontal Movement")]
    public float velocity;
    public float maxSpeed = 12f;

    [Header("Jumping")]
    public float jumpForce = 15f;
    public float rayLength = 0.55f;
    [HideInInspector] public bool jump;

    void Start()
    {
        aS = GetComponent<AnimalSwitch>();
    }

    public void Move(Vector2 direction)
    {
        if (aS.groundLength != rayLength)
            aS.groundLength = rayLength;

        //jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && aS.isGrounded)
        {
            jump = true;
        }
    }

    public void FixedMove(Vector2 direction)
    {
        MoveChar(direction.x);
        Jump();
    }

    void MoveChar(float horizontal)
    {
        aS.rb.AddForce(Vector2.right * horizontal * velocity);

        if (Mathf.Abs(aS.rb.velocity.x) > maxSpeed)
        {
            aS.rb.velocity = new Vector2(Mathf.Sign(aS.rb.velocity.x) * maxSpeed, aS.rb.velocity.y);
        }
    }
    void Jump()
    {
        if (jump)
        {
            aS.rb.velocity = new Vector2(aS.rb.velocity.x, 0);
            aS.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }
}
