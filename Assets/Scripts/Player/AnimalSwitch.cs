using System.Collections;
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

    [Header("Physics")]
    float gravity = 1;
    float fallMultiplier = 5;

    ////Animal transformations
    BatMove batMove;
    CatMove catMove;
    RatMove ratMove;
    int pos = 1;

    ////inspector values
    bool facingLeft = false;
    [HideInInspector] public Vector2 direction;
    float linearDrag = 10f;

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
        
        Debug.Log(rb.velocity.x);

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
        modifyPhysics();
    }

    void SwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && pos != 0)
        {
            SwitchAnimal(0);
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
        animals[i].SetActive(true);
        animals[i].transform.position = animals[pos].transform.position;
        animals[pos].SetActive(false);
        pos = i;
    }

    void FlipDir()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && facingLeft)
        {
            facingLeft = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !facingLeft)
        {
            facingLeft = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void IsGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundLength, ground);
    }

    public void modifyPhysics()
    {
        if (isGrounded)
        {
            bool changingDirection = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

            if (Mathf.Abs(direction.x) < 0.4f || changingDirection)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
        }
        //else
        //{
        //    rb.gravityScale = gravity;
        //    rb.drag = linearDrag * 0.15f;
        //    if (rb.velocity.y < 0)
        //    {
        //        rb.gravityScale = gravity * fallMultiplier;
        //    }
        //    else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        //    {
        //        rb.gravityScale = gravity * (fallMultiplier / 2);
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

}
