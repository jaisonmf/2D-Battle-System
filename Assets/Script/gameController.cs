using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public bool enemyTurn = false;
    public bool gameStart = false;
    public bool generated = false;

    public enemyController enemyController;
    public playerController playerController;
    public enemyGenerator enemyGenerator;

    public Text turnCounter;
    public int turnCount = 0;

    public Text instructions;
    private void Start()
    {
        enemyTurn = false;
        playerController.menu.SetActive(false);
        turnCounter.text = "Turn: " + turnCount;
        instructions.text = ("You have been attacked!\nPress 'E' to continue");
        playerController.healthNum.text = playerController.pHealth.ToString() + "/" + playerController.pMaxHealth.ToString();
        playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && generated == false)
        {
            enemyGenerator.EnemyGeneration();
            gameStart = true;
            generated = true;
        }

        if (enemyTurn == true && gameStart == true)
        {
            enemyController.EnemyStart();
        }

        if (enemyTurn == false && gameStart == true)
        {
            gameStart = true;
            playerController.PlayerStart();
            instructions.text = ("Select your move");

        }
    }
}
