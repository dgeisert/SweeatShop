using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Ethics;
    public void EndGame(bool victory = false)
    {
        gameObject.SetActive(true);
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(Time.time);
        scoreText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        Ethics.text = Game.Instance.Ethics();
    }

    public void Restart()
    {
        SceneChanger.LoadScene(Scenes.Game);
    }
    public void Menu()
    {
        SceneChanger.LoadScene(Scenes.MainMenu);
    }
}