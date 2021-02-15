using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public Sprite[] animalSprites;

    [SerializeField] int currentHealth;
    AnimalSwitch aS;

    void Start()
    {
        currentHealth = maxHealth;
        aS = GetComponent<AnimalSwitch>();
    }

    void Update()
    {
        HealthChange(0);
    }
    void HealthChange(int fx)
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
                //die
                break;
        }

    }
}
