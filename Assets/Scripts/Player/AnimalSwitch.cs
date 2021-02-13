using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSwitch : MonoBehaviour
{
    public GameObject[] animals;

    BatMove batMove;
    CatMove catMove;
    RatMove ratMove;
    int pos = 1;

    // Start is called before the first frame update
    void Start()
    {
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

        if (pos == 0)
        {
            batMove.Move();
        }
        if (pos == 1)
        {
            catMove.Move();
        }
        if (pos == 2)
        {
            ratMove.Move();
        }
    }

    void SwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && pos != 0)
        {
            SwitchAnimal(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && pos != 1)
        {
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
}
