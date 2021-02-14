using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    AnimalSwitch aS;

    [Header("Horizontal Movement")]
    public float velocity;
    public float maxGroundSpeed = 4f;

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
        MoveChar(direction.x);
    }

    void MoveChar(float horizontal)
    {
        aS.rb.AddForce(Vector2.right * horizontal * velocity);

        if (Mathf.Abs(aS.rb.velocity.x) > maxGroundSpeed)
        {
            aS.rb.velocity = new Vector2(Mathf.Sign(aS.rb.velocity.x) * maxGroundSpeed, aS.rb.velocity.y);
        }
    }
}
