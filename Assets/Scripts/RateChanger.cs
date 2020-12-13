using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RateChanger : MonoBehaviour
{
    public TextMeshProUGUI name, cost;
    public string nameString;
    public float startCost = 10;
    public float increment = 0.2f;
    public float rate = 1;
    public int count = 0;
    public float currentCost;
    public Animator anim;
    public Button b;
    public bool clickRate = false;
    public ParticleSystem ps;
    bool isEnabled = false;

    void Start()
    {
        currentCost = startCost * Mathf.Pow(1 + increment, count);
        name.text = count + " " + nameString;
        cost.text = "Cost: " + currentCost.ToString("#,#");
        anim.SetTrigger("Disabled");
    }

    public void Allow(float score, bool allow)
    {
        if (startCost * 0.7f < score)
        {
            gameObject.SetActive(true);
        }
        if (!allow || (isEnabled != allow))
        {
            isEnabled = allow;
            anim.SetTrigger(allow ? "Normal" : "Disabled");
        }
        b.enabled = allow;
    }

    public void Click()
    {
        Game.Score -= currentCost;
        count++;
        if (clickRate)
        {
            Game.ClickRate += (int) rate;
        }
        else
        {
            Game.Rate += rate;
        }
        currentCost = startCost * Mathf.Pow(1 + increment, count);
        name.text = count + " " + nameString;
        cost.text = "Cost: " + currentCost.ToString("#,#");
        if (!!ps)
        {
            var em = ps.emission;
            em.enabled = true;
            em.rateOverTime = Mathf.Pow(count * rate, 0.5f);
        }
    }
}