using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Material hurtMat;
    Material startMat;

    PlayerHealth pHP;
    Animator playerAnim;
    Animator anim;

    public Transform[] waypoints;
    int p = 0;

    public int maxHP = 2;
    public float speed = 5;
    public int damage = -1;
    float tempSpeed;

    float stopageTime = 1f;
    float startStoppageTime = 0;
    bool canHit = true;

    bool beenHit = false;
    bool facingLeft = false;
    float beenHitTimer = 0;

    int curHP;

    // Start is called before the first frame update
    void Start()
    {
        pHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerAnim = pHP.GetComponent<Animator>();

        anim = GetComponent<Animator>();

        curHP = maxHP;
        tempSpeed = speed;

        startMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (waypoints.Length > 0)
        {
            transform.position = (Vector2.MoveTowards(transform.position, waypoints[p].position, speed * Time.fixedDeltaTime));

            Debug.Log(p);
            Flip();

            if (Vector2.Distance(transform.position, waypoints[p].position) < 1f)
            {
                if (p != waypoints.Length-1)
                {
                    p++;
                }
                else
                {
                    p = 0;
                }
            }
        }

        if (!canHit)
        {
            speed = 0;
            startStoppageTime += Time.deltaTime;
            if (startStoppageTime >= stopageTime)
            {
                speed = tempSpeed;
                startStoppageTime = 0;
                canHit = true;
            }
        }

        if (beenHit)
        {
            beenHitTimer += Time.deltaTime;
            if (beenHitTimer >= 0.2f)
            {
                beenHit = false;
                beenHitTimer = 0;
            }
        }
    }

    void Flip()
    {
        if (transform.position.x - waypoints[p].position.x > 0 && !facingLeft)
        {
            facingLeft = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x - waypoints[p].position.x < 0 && facingLeft)
        {
            facingLeft = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log("messageRecieved");
        curHP -= damage;
        anim.Play("hurt");

        if (curHP == 0)
        {
            Destroy(this.gameObject);
        }
        beenHit = true;
        canHit = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && canHit)
        {
            Vector2 knockbackVector = collision.transform.position - transform.position;
            pHP.HealthChange(damage);
            playerAnim.Play("Hurt");
            switch (this.gameObject.layer)
            {
                case 11:
                    Destroy(this.gameObject);
                    break;

                case 12:
                    Destroy(this.gameObject);
                    Application.Quit();
                    break;
                    
            }
            canHit = false;
        }

        if (collision.transform.tag == "Colliders" && (collision.gameObject.layer == 11 || collision.gameObject.layer == 12))
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }
}
