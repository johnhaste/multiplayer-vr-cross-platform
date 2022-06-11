using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_OpenWorldsManager : MonoBehaviour
{
    public Button buttonEnter;

    public void displayButtons()
    {
        buttonEnter.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
    }
}
