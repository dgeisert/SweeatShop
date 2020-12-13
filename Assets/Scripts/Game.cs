using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public PauseMenu pauseMenu;
    public ScoreScreen scoreScreen;
    public InGameUI inGameUI;
    public ParticleSystem ps;
    public bool active = true;

    public RateChanger[] rateChangers;

    float goodScore, badScore;

    public static int ClickRate
    {
        get
        {
            if (Instance)
            {
                return Instance.clickRate;
            }
            return -1;
        }
        set
        {
            if (Instance)
            {
                Instance.clickRate = value;
                Instance.inGameUI.UpdateClickRate(value);
            }
        }
    }
    private int clickRate = 1;
    public static float Rate
    {
        get
        {
            if (Instance)
            {
                return Instance.rate;
            }
            return -1f;
        }
        set
        {
            if (Instance)
            {
                Instance.rate = value;
                Instance.inGameUI.UpdateRate(value);
            }
        }
    }
    private float rate;
    public static float Score
    {
        get
        {
            if (Instance)
            {
                return Instance.score;
            }
            return -1f;
        }
        set
        {
            if (Instance)
            {
                Instance.score = value;
                Instance.inGameUI.UpdateScore(value);
            }
        }
    }
    private float score;
    int step = 3;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        pauseMenu.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);

        Score = 0;
        Rate = 0;
        ClickRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (score > 1000000)
            {
                active = false;
                pauseMenu.gameObject.SetActive(false);
                inGameUI.gameObject.SetActive(false);
                scoreScreen.EndGame(true);

                ps.loop = true;
                var em = ps.emission;
                em.rateOverTime = Mathf.Pow(clickRate, 0.5f);
                em.SetBursts(
                    new ParticleSystem.Burst[]
                    { });
                ps.Play();
            }
            foreach (RateChanger rc in rateChangers)
            {
                rc.Allow(score, CheckCost(rc.currentCost));
            }
            Score += rate * Time.deltaTime;
            badScore += rate * Time.deltaTime;
        }
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void Click()
    {
        Score += clickRate;
        goodScore += clickRate;
        var em = ps.emission;
        em.SetBursts(
            new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0, Mathf.Pow(clickRate, 0.5f))
            });
        ps.Play();
    }

    public bool CheckCost(float val)
    {
        return (score >= val);
    }

    public string Ethics()
    {
        return Mathf.RoundToInt((badScore / (goodScore + badScore) * 100)) + "%";
    }
}