using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectEnemy : MonoBehaviour
{
    public enemyController enemyController;
    public playerController playerController;
    public Button selection;
    public GameObject selectButton;
    public GameObject selectedEnemy;

    public void Select(int Select)
    {
        if(Select == 0)
        {
            Selected();
        }
    }




    void Selected()
    {
        playerController.Attack();
    }
}
