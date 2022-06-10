using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    public gameController gameController;
    public enemyController enemyController;

    public int amount;
    public GameObject goblin;
    public GameObject go;
    public GameObject Parent;


    private List<GameObject> list = new List<GameObject>();

    public void EnemyGeneration()
    {
        amount = Random.Range(1, 6);
        {
            for(int i = 0; i < amount; i++)
            {
                go = Instantiate(goblin, new Vector2((1920/(amount + 1)) * (i+1),10), Quaternion.identity);
                go.transform.SetParent(Parent.transform, false);
                list.Add(go);
                EnemyStats();

            }
            
        }
    }



    public void EnemyStats()
    {
        //Calculates states
        go.GetComponent<enemyController>().eMaxHealth = Random.Range(50, 100);
        go.GetComponent<enemyController>().eMaxDamage = Random.Range(10, 15);
        go.GetComponent<enemyController>().eMinDamage = Random.Range(5, 10);
        go.GetComponent<enemyController>().eHealth = go.GetComponent<enemyController>().eMaxHealth;

    }

    public void Aggression()
    {
        if (gameController.turnCount > 5)
        {
            go.GetComponent<enemyController>().eMaxHealth += 5;
            go.GetComponent<enemyController>().eMaxDamage += 5;
            go.GetComponent<enemyController>().eMinDamage += 5;
            go.GetComponent<enemyController>().eHealth += 5;
        }
    }

}
