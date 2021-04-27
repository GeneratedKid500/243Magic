using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    float timeBtwAttack;
    public float startTimeBtwAttack;

    Animator anim;

    public SpriteRenderer slashSprite;
    float timer = 0;

    public Transform attackPos;
    public LayerMask enemyMask;
    public float attackRange;
    public int damage;

    bool keydown = false;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            Attack();
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (slashSprite.enabled)
        {
            anim.Play("Attack");
            timer += Time.deltaTime;
            if (timer > 0.05f)
            {
                slashSprite.enabled = false;
                timer = 0f;
            }
        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.X) && !keydown)
        {
            keydown = true;
            slashSprite.enabled = true;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyMask);
            foreach (Collider2D enemy in enemiesToDamage)
            {
                enemy.SendMessageUpwards("TakeDamage", 1);
            }
        }

        if (!Input.GetKey(KeyCode.X))
        {
            keydown = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
