using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public Sprite[] animalSprites;
    Animator anim;
    SpriteRenderer spr;
    public RuntimeAnimatorController[] animalSets;
    public Material hurtMat;
    Material startMat;

    [SerializeField] int currentHealth;
    AnimalSwitch aS;

    bool beenHit = false;
    float beenHitTimer = 0;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        aS = GetComponent<AnimalSwitch>();
        SetAnim();
    }

    void Update()
    {
        if (beenHit)
        {
            anim.SetBool("Hurt", true);
            beenHitTimer += Time.deltaTime;
            if (beenHitTimer >= 0.2f)
            {
                beenHit = false;
                beenHitTimer = 0;
                anim.SetBool("Hurt", false);
            }
        }
    }

    public void HealthChange(int fx)
    {
        currentHealth += fx;

        SetAnim();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (fx < 0)
        {
            beenHit = true;
            startMat = GetComponentInChildren<Renderer>().material;
        }
    }

    void SetAnim()
    {
        switch (aS.pos) 
        {
            case 0:
                if (currentHealth > 1)
                    anim.runtimeAnimatorController = animalSets[0];
                else
                    anim.runtimeAnimatorController = animalSets[1];
                spr.flipX = true;
                break;

            case 1:
                if (currentHealth > 1)
                    anim.runtimeAnimatorController = animalSets[2];
                else
                    anim.runtimeAnimatorController = animalSets[3];
                spr.flipX = false;
                break;

            case 2:
                if (currentHealth > 1)
                    anim.runtimeAnimatorController = animalSets[4];
                else
                    anim.runtimeAnimatorController = animalSets[5];
                spr.flipX = false;
                break;


        }

    }
}
