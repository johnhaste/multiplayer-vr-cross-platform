using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_OpenWorldsManager : MonoBehaviour
{
    public Button buttonEnter;

    void start()
    {
        buttonEnter.enabled = false;
    }

    public void displayButtons()
    {
        buttonEnter.gameObject.SetActive(true);
    }
}
