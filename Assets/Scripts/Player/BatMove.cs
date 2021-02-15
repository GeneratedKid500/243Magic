using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    AnimalSwitch aS;

    [Header("Horizontal Movement")]
    public float groundVel;
    public float airVel;

    [Header("Vertical Movement")]
    public float upSpeed;

    [Header("Jumping")]
    public float rayLength = 1.1f;

    void Start()
    {
        aS = GetComponent<AnimalSwitch>();
    }

    public void Move(Vector2 direction)
    {
        if (aS.groundLength != rayLength)
            aS.groundLength = rayLength;
    }

    public void FixedMove(Vector2 direction)
    {
        MoveHoriz(direction.x);
        MoveVert(direction.y);
    }

    void MoveHoriz(float horizontal)
    {
        if (aS.isGrounded)
            aS.rb.velocity = new Vector2(horizontal * (Time.fixedDeltaTime * (groundVel * 15)), aS.rb.velocity.y);
        else
        {
            aS.rb.velocity = new Vector2(horizontal * (Time.fixedDeltaTime * (airVel * 15)), aS.rb.velocity.y);
        }
    }

    void MoveVert(float vertical)
    {
        aS.rb.velocity = new Vector2(aS.rb.velocity.x, (Time.fixedDeltaTime * (upSpeed * 15) * vertical));
    }
}
