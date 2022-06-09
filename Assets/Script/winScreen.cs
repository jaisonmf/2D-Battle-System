using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winScreen : MonoBehaviour
{
    public GameObject win;
    public Text winText;
    public void Victory()
    {
        win.SetActive(true);
        winText.text = ("You have survived the battle!");
    }
}
