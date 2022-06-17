using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerDefended : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int towerLives = 5;

    // Start is called before the first frame update
    void Start()
    {
        towerLives = 5;
    }

    public void LoseLive()
    {
        if(towerLives > 0)
        {
            towerLives--;
            UpdateLivesGUI();
        }
        else
        {
            Die();
        }
    }

    void UpdateLivesGUI()
    {
        healthText.text = towerLives+"";
    }

    void Die()
    {
       healthText.text = "You Lost!"; 
    }
}
