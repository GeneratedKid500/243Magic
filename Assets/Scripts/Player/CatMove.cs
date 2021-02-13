using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    float vel;

    Vector2 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed /= 100;
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (vel < 1)
                vel += moveSpeed;
            moveDir = Vector2.right;
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (vel < )
                vel += moveSpeed;
            moveDir = Vector2.left;
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            if (vel > 0)
            {
                vel -= moveSpeed * 10;
            }
            else if (vel < 0)
            {
                vel = 0;
            }
        }

        transform.Translate(moveDir * (Time.deltaTime * vel));

 
    }
}
