using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //Call scripts
    public gameController gameControl;
    public enemyGenerator enemyGenerator;
    public selectEnemy selectEnemy;


    //Sliders
    public propertyMeter healthMeter;
    public propertyMeter defenceMeter;

    //GUI
    public GameObject menu;
    public Text healthNum;
    public Text defenceNum;
    public bool selecting = false;
    public GameObject selectMenu;
    public GameObject damageOutput;

    //Stats
    public int pMaxHealth = 100;
    public int pHealth;
    
    private int pMaxDamage = 10;
    private int pMinDamage = 5;
    public int pDamage;

    public int pMaxDefence = 50;
    public int pDefence = 0;

    public int energy = 5;
    public Text energyCount;

    //Buttons
    public Button attack;
    public Button heal;
    public Button defend;

    //Other
    public Animator damageAnim;
    public Animator healAnim;
    public Animator defendAnim;
    public Text damageText;
    public Text healText;
    public Text defendText;

    private void Start()
    {
        pHealth = pMaxHealth;
    }

    //Damage calculation
    private void Damage()
    {
        pDamage = Random.Range(pMaxDamage, pMinDamage);
    }
    //Player death & defence/health doesnt pass a certain threshold
    private void Update()
    {
        if(pHealth > pMaxHealth)
        {
            pHealth = pMaxHealth;
        }

        if(pDefence > pMaxDefence)
        {
            pDefence=pMaxDefence;
        }

        if(pDefence <= 0)
        {
            pDefence = 0;
        }

        if(pHealth <= 0)
        {
            menu.SetActive(false);
            Destroy(this);
        }
        Buttons();
    }
    public void PlayerStart()
    {
        menu.SetActive(true);
        energyCount.text = energy.ToString();
        healthNum.text = pHealth.ToString() + "/" + pMaxHealth.ToString();
        defenceNum.text = pDefence.ToString() + "/" + pMaxDefence.ToString();
        
        heal.interactable = true;
        attack.interactable = true;
        defend.interactable = true;
    }

    //Player turn
    public void PlayerGo(int ButtonPress)
    {
        if (ButtonPress == 1 && energy >= 1)
        {
            selecting = true;
            energy -= 1;
        }
        if (ButtonPress == 2 && energy >= 2 && pHealth < 100)
        {
            pHealth += 10;
            healthMeter.UpdateMeter(pHealth, pMaxHealth);
            healText.text = 10.ToString();
            healAnim.Play("heal");
            energy -= 2;
        }
        if(ButtonPress == 3 && energy >= 2 && pDefence < 50)
        {
            pDefence += 10;
            defenceMeter.UpdateMeter(pDefence, pMaxDefence);
            defendText.text = 10.ToString();
            defendAnim.Play("defend");
            energy -= 2;
        }
        if (ButtonPress == 4)
        {
            gameControl.EnemyTurn();

            menu.SetActive(false);
            gameControl.turnCounter.text = "Turn: " + gameControl.turnCount;
            gameControl.turnCount += 1;

        }
    }

    //Attack Calculation againts enemy/enemies
    public void Attack(int listIndex)
    {
        GameObject enemy = enemyGenerator.list[listIndex];
        Damage();
        if (pDamage - enemy.GetComponent<enemyController>().eDefence <= enemy.GetComponent<enemyController>().eDefence)
        {
            enemy.GetComponent<enemyController>().eDefence -= pDamage;
            enemy.GetComponent<enemyController>().edefenceMeter.UpdateMeter(enemy.GetComponent<enemyController>().eDefence, enemy.GetComponent<enemyController>().eMaxDefence);
        }
        //breaks defence + goes into health. Also if player has no defence
        else
        {
            enemy.GetComponent<enemyController>().eHealth = enemy.GetComponent<enemyController>().eHealth - pDamage + enemy.GetComponent<enemyController>().eDefence;
            enemy.GetComponent<enemyController>().eDefence = 0;
            defenceMeter.UpdateMeter(enemy.GetComponent<enemyController>().eDefence, enemy.GetComponent<enemyController>().eMaxDefence);
            enemy.GetComponent<enemyController>().ehealthMeter.UpdateMeter(enemy.GetComponent<enemyController>().eHealth, enemy.GetComponent<enemyController>().eMaxHealth);
        }
        damageOutput.SetActive(true);
        damageText.text = pDamage.ToString();
        damageAnim.Play("damage");
        selecting = false;

    }


    //Button Avaliability
    private void Buttons()
    {
        if (energy == 0 || selecting == true)
        {
            attack.interactable = false;
        }
        if (energy < 2 || pHealth == 100 || selecting == true)
        {
            heal.interactable = false;
        }
        if (energy < 2 || pDefence == 50 || selecting == true)
        {
            defend.interactable=false;
        }
    }


}
