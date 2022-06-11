using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_OpenWorldsManager : MonoBehaviour
{
    public TextMeshProUGUI buttonEnterText;

    void start()
    {
        buttonEnterText.text = "Loading";
    }

    public void displayButtons()
    {
        buttonEnterText.text = "Enter";
    }
}
