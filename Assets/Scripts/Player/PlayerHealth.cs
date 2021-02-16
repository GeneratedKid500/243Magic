using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public Sprite[] animalSprites;
    public Material hurtMat;
    Material startMat;

    [SerializeField] int currentHealth;
    AnimalSwitch aS;

    bool beenHit = false;
    float beenHitTimer = 0;

    void Start()
    {
        currentHealth = maxHealth;
        aS = GetComponent<AnimalSwitch>();
    }

    void Update()
    {
        if (beenHit)
        {
            GetComponentInChildren<Renderer>().material = hurtMat;
            beenHitTimer += Time.deltaTime;
            if (beenHitTimer >= 0.2f)
            {
                beenHit = false;
                beenHitTimer = 0;
                GetComponentInChildren<Renderer>().material = startMat;
            }
        }
    }

    public void HealthChange(int fx)
    {
        currentHealth += fx;

        switch (currentHealth) 
        {
            case 2:
                for (int i=0; i < 3; i++)
                {
                    aS.animals[i].SetActive(true);
                    aS.animals[i].GetComponent<SpriteRenderer>().sprite = animalSprites[i];
                    if (i != aS.pos)
                        aS.animals[i].SetActive(false);
                }
                break;

            case 1:
                for (int i = 0; i < 3; i++)
                {
                    aS.animals[i].SetActive(true);
                    aS.animals[i].GetComponent<SpriteRenderer>().sprite = animalSprites[i+3];
                    if (i != aS.pos)
                        aS.animals[i].SetActive(false);
                }
                break;

            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            default:
                currentHealth = 2;
                break;
        }

        if (fx < 0)
        {
            beenHit = true;
            startMat = GetComponentInChildren<Renderer>().material;
        }
    }
}
