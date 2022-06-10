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
    public GameObject selection;

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

        if (gameController.enemyTurn == false)
        {
            selection.SetActive(true);
        }
        else if (gameController.enemyTurn == true)
        {
            selection.SetActive(false);
        }
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
        Action = Random.Range(1, 7);
        Debug.Log(Action);
        //Above 75%
        if (eHealth > eMaxHealth * 0.75)
        {
            if (Action <= 3)
            {
                Debug.Log("dam,n");
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
            if (Action == 1 || Action == 2 || Action == 3)
            {
                Attack();
            }
            else if (Action == 4 || Action == 5)
            {
                Defend();
            }

            else Special();
        }
        //Lower than 25%
        else
        {
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
                Debug.Log("why are you here");
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
