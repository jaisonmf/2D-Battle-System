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

    //Enemy GUI
    public GameObject enemy;
    public GameObject enemyBars;
    public GameObject selection;
    public propertyMeter ehealthMeter;
    public propertyMeter edefenceMeter;
    public Text damageText;
    public GameObject damageOutput;
    public Animator damageAnim;

    //Delay
    private bool isCoroutineOn;

    //Stats
    public int eMaxHealth;
    public int eHealth;
    public int eMaxDamage;
    public int eMinDamage;
    public int eDefence;
    public int eMaxDefence;
    public int count;
    private int Action;
    public int eDamage;


    //Certain stats cannot go over a certain threshold
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

    //Damage calculation
    private void Damage()
    {
        eDamage = Random.Range(eMaxDamage, eMinDamage);
    }

    //Enemy delay on turn
    public void EnemyStart()
    {
        StartCoroutine(Delay(3));
    }

    //Enemy behaviour
    public void EnemyGo()
    {
        Action = Random.Range(1, 7);
        //Above 75%
        if (enemy.GetComponent<enemyController>().eHealth > enemy.GetComponent<enemyController>().eMaxHealth * 0.75)
        {
            if (Action <= 3)
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
        else if (enemy.GetComponent<enemyController>().eHealth > enemy.GetComponent<enemyController>().eMaxHealth * 0.25)
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
                Attack();
            }
        }

    }

    //Enemy attack against player
    public void Attack()
    {
        Damage();
        Debug.Log(eDamage);
        //not enough to break defend
        if (enemy.GetComponent<enemyController>().eDamage - playerController.pDefence <= playerController.pDefence)
        {
            playerController.pDefence -= enemy.GetComponent<enemyController>().eDamage;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
        }
        //breaks defence + goes into health. Also if player has no defence
        else
        {
            playerController.pHealth = playerController.pHealth - enemy.GetComponent<enemyController>().eDamage + playerController.pDefence;
            playerController.pDefence = 0;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
            playerController.healthMeter.UpdateMeter(playerController.pHealth, playerController.pMaxHealth);
        }

    }
    public void Special()
    {
        Attack();
        enemy.GetComponent<enemyController>().eHealth += 10;
        ehealthMeter.UpdateMeter(eHealth, eMaxHealth);
    }

    public void Defend()
    {
        enemy.GetComponent<enemyController>().eDefence += 10;
        edefenceMeter.UpdateMeter(eDefence, eMaxDefence);
    }


    IEnumerator Delay(float time)
    {
        if (isCoroutineOn)
            yield break;
        
        isCoroutineOn = true;
        
        yield return new WaitForSeconds(time);
        EnemyGo();

        gameController.PlayerTurn();
        playerController.energy = 5;
        playerController.healthNum.text = playerController.pHealth.ToString() + "/" + playerController.pMaxHealth.ToString();
        playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
        isCoroutineOn = false;
    }



}
