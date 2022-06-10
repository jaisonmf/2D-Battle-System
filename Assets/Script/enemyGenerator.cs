using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    public gameController gameController;
    public enemyGenerator enemyController;

    public int amount;
    public GameObject goblin;
    public GameObject go;
    public GameObject canvas;

    //Health
    public int eHealth;
    public int eMaxHealth;
    
    //Damage
    public int eMaxDamage;
    public int eLowDamage;
     
    //Defence
    public int eDefence;
    public int eMaxDefence = 50;

    //Bars
    public propertyMeter ehealthMeter;
    public propertyMeter edefenceMeter;

    public void EnemyGeneration()
    {
        for (amount = Random.Range(0, 6); amount < 0; amount++)
        {
            go = Instantiate(goblin, new Vector3(amount * 10, 0, 1), Quaternion.identity);
            Debug.Log(amount);
            goblin.SetActive(true);
            go.transform.parent = canvas.transform;
            EnemyStats();
        }
    }
    
    
    
    public void EnemyStats()
    {
        //Calculates states
        eMaxHealth = Random.Range(50, 100);
        eMaxDamage = Random.Range(10, 15);
        eLowDamage = eMaxDamage - 5;
        eHealth = eMaxHealth;

    }

    public void Aggression()
    {
        if(gameController.turnCount > 5)
        {
            eMaxHealth += 5;
            eMaxDamage += 5;
            eLowDamage += 5;
            enemyController.eHealth += 5;
        }
    }

}
