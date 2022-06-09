using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public bool enemyTurn;
    public bool gameStart = false;

    public enemyController enemyController;
    public playerController playerController;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameStart = true;
            enemyTurn = false;
        }

        if (enemyTurn == true && gameStart == true)
        {
            enemyController.EnemyStart();
        }

        if (enemyTurn == false && gameStart == true)
        {
            playerController.PlayerStart();
            instructions.text = ("Select your move");

        }
    }
}
