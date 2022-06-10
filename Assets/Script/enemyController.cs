using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
    //Call scripts
    public gameController gameController;
    public enemyGenerator enemyGenerator;
    public playerController playerController;
    public winScreen winScreen;

    public GameObject enemy;
    public GameObject enemyBars;

    public propertyMeter ehealthMeter;
    public propertyMeter edefenceMeter;

    private int Action;

    public int eDamage;

    private bool isCoroutineOn;

    public int eMaxHealth;
    public int eHealth;
    public int eMaxDamage;
    public int eMinDamage;
    public int eDefence;
    public int eMaxDefence = 50;

    private void Update()
    {
        if (eDefence >= eMaxDefence)
        {
            eDefence = eMaxDefence;
        }
        if(eHealth >= eMaxHealth)
        {
            eHealth = eMaxHealth;
        }
       /* if(eHealth <= 0)
        {
            enemy.SetActive(false);
            enemyBars.SetActive(false);
            //winScreen.Victory(true);
        }
       */
    }

    private void Damage()
    {
        eDamage = Random.Range(eMaxDamage, eMinDamage);
    }

    public void EnemyStart()
    {
        StartCoroutine(Delay(3));
    }

    public void EnemyGo()
    {
        //Above 75%
        if(eHealth > eMaxHealth * 0.75)
        {
            Action = Random.Range(1, 7);
            
            if(Action <= 3)
            {
                Attack();
            }
            else if (Action <= 5)
            {
                Special();
            }
            else if(Action == 6)
            {
                Defend();
            }
        }
        //Above 25%
        else if (eHealth > eMaxHealth * 0.25)
        {
            Action = Random.Range(1, 4);
            if (Action == 1)
            {
                Attack();
            }
            else if (Action == 2)
            {
                Defend();
            }

            else Special();
        }
        //Lower than 25%
        else
        {
            Action = Random.Range(1, 7);
            if (Action <= 4)
            {
                Defend();
            }
            else if (Action == 5)
            {
                Special();
            }
            else if (Action == 6)
            {
                Attack();
            }
        }

    }


    public void Attack()
    {
        Damage();
        Debug.Log(eDamage);
        //not enough to break defend
        if (eDamage - playerController.pDefence <= playerController.pDefence)
        {
            playerController.pDefence -= eDamage;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
        }
        //breaks defence + goes into health. Also if player has no defence
        else
        {
            playerController.pHealth = playerController.pHealth - eDamage + playerController.pDefence;
            playerController.pDefence = 0;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
            playerController.healthMeter.UpdateMeter(playerController.pHealth, playerController.pMaxHealth);
        }

    }
    public void Special()
    {
        Attack();
        Debug.Log(eDamage);
        eHealth += 10;
        ehealthMeter.UpdateMeter(eHealth, eMaxHealth);
    }

    public void Defend()
    {
        eDefence += 10;
        edefenceMeter.UpdateMeter(eDefence, eMaxDefence);
    }


    IEnumerator Delay(float time)
    {
        if (isCoroutineOn)
            yield break;

        isCoroutineOn = true;

        yield return new WaitForSeconds(time);
        EnemyGo();

        gameController.enemyTurn = false;
        playerController.energy = 5;
        gameController.turnCount += 1;
        gameController.turnCounter.text = "Turn: " + gameController.turnCount;
        playerController.healthNum.text = playerController.pHealth.ToString() + "/" + playerController.pMaxHealth.ToString();
        playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
        enemyGenerator.Aggression();
        isCoroutineOn = false;
    }



}
