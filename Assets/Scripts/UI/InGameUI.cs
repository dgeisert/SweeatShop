using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText, rateText, clickRate;

    public void UpdateScore(float val)
    {
        scoreText.text = "Score: " + val.ToString("#,#");
    }

    public void UpdateRate(float val)
    {
        rateText.text = "Production Rate: " + val.ToString("#,#");
    }

    public void UpdateClickRate(float val)
    {
        clickRate.text = "Click Rate: " + val.ToString("#,#");
    }
    public void EndGame(bool victory)
    {
        gameObject.SetActive(false);
    }
}