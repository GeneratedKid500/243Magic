using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    AnimalSwitch aS;

    [Header("Horizontal Movement")]
    public float velocity;

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

        Debug.Log(aS.rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && aS.hangCounter > 0f)
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
        aS.rb.velocity = new Vector2(horizontal * (Time.deltaTime * (velocity * 15)), aS.rb.velocity.y);
    }

    void Jump()
    {
        if (jump)
        {
            aS.rb.velocity = new Vector2(aS.rb.velocity.x, ((jumpForce * 15) * Time.fixedDeltaTime));
            jump = false;
        }

        if (!Input.GetKey(KeyCode.UpArrow) && aS.rb.velocity.y > 0)
        {
            aS.rb.velocity = new Vector2(aS.rb.velocity.x, aS.rb.velocity.y * 0.5f);
        }
    }
}
