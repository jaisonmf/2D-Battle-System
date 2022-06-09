using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    public gameController gameController;
    public enemyGenerator enemyController;

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

    void Start()
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
