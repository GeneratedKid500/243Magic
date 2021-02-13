using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    public int moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(Vector2.left * (Time.deltaTime * moveSpeed));
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * (Time.deltaTime * moveSpeed));
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }
}
