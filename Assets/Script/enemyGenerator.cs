using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGenerator : MonoBehaviour
{
    public gameController gameController;
    public enemyController enemyController;

    public int amount;
    public GameObject goblin;
    public GameObject go;
    public GameObject Parent;


    public List<GameObject> list = new List<GameObject>();

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
                go.GetComponent<enemyController>().count = i;

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
        go.GetComponent<enemyController>().eMaxDefence = 50;

    }

    public void Aggression()
    {
        if (gameController.turnCount > 5 && gameController.aggrovated == true)
        {
            go.GetComponent<enemyController>().eMaxHealth += 5;
            go.GetComponent<enemyController>().eMaxDamage += 5;
            go.GetComponent<enemyController>().eMinDamage += 5;
            go.GetComponent<enemyController>().eHealth += 5;
            gameController.aggrovated = false;
        }
    }

}
