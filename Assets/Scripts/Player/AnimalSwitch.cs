﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSwitch : MonoBehaviour
{
    [Header ("Components")]
    public Rigidbody2D rb;
    public GameObject[] animals;
    public LayerMask ground;

    [Header("Collision")]
    public bool isGrounded = false;
    [HideInInspector] public float groundLength;
    public float hangTime = 0.1f;
    [HideInInspector] public float hangCounter;

    ////Animal transformations
    BatMove batMove;
    CatMove catMove;
    RatMove ratMove;
    [HideInInspector] public int pos = 1;

    ////inspector values
    bool facingLeft = false;
    [HideInInspector] public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        batMove = GetComponent<BatMove>();
        catMove = GetComponent<CatMove>();
        ratMove = GetComponent<RatMove>();

        animals[0].SetActive(false);
        animals[1].SetActive(true);
        animals[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchInput();
        FlipDir();

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Debug.Log(direction.y);

        if (pos == 0)
        {
            batMove.Move(direction);
        }
        else if (pos == 1)
        {
            catMove.Move(direction);
        }
        else if (pos == 2)
        {

            ratMove.Move(direction);
        }
    }

    void FixedUpdate()
    {
        IsGrounded();

        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (pos == 0)
        {
            batMove.FixedMove(direction);
        }
        else if (pos == 1)
        {
            catMove.FixedMove(direction);
        }
        else if (pos == 2)
        {
            ratMove.FixedMove(direction);
        }
    }

    void SwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && pos != 0)
        {
            SwitchAnimal(0);
            rb.gravityScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && pos != 1)
        {
            if (pos == 2) transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
            SwitchAnimal(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && pos != 2)
        {
            SwitchAnimal(2);
        }
    }
    void SwitchAnimal(int i)
    {
        rb.gravityScale = 1;
        animals[i].SetActive(true);
        animals[i].transform.position = animals[pos].transform.position;
        animals[pos].SetActive(false);
        pos = i;
    }

    void FlipDir()
    {
        if (direction.x > 0 && facingLeft)
        {
            facingLeft = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0 && !facingLeft)
        {
            facingLeft = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void IsGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundLength, ground);
        
        //manage hangtime
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

}
