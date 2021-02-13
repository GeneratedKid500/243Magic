using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    public int moveSpeed = 8;
    public int upForce = 5;
    bool moveUp = false;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed), Space.World);
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(Vector2.left * (Time.deltaTime * moveSpeed), Space.World);
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    moveUp = true;
        //}
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed), Space.World);
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * (Time.deltaTime * moveSpeed), Space.World);
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveUp = true;
        }
    }

    void FixedUpdate()
    {
        if (moveUp)
        {
            rb.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
            moveUp = false;
        }
    }
}
