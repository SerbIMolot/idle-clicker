
using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ClickTimer : MonoBehaviour
{
    public float timeLeft = 300.0f;
    public float maxTime = 300.0f;
    private const float MIN_TIME = 0f;

    public bool stop = true;
    public bool pause = false;

    private float minutes = 0;
    private float seconds = 0;

    public TextMesh text;
    public GameObject Bar;
    public GameObject BarBack;
    public GameObject targetObject;
    private BaseClick targetClicker;

    private void Start()
    {
        targetClicker = targetObject.GetComponent<BaseClick>();
    }
    public void startTimer(float from)
    {
        stop = false;
        pause = false;
        maxTime = from;
        timeLeft = from;
        UpdateTime();
       // StartCoroutine(updateCoroutine());
    }
    private void Reset()
    {
        stop = false;
        pause = false;
        timeLeft = maxTime;

        UpdateTime();

        Bar.transform.localScale = new Vector3(normalizeTimeLeft(), 1f, 1f);
    }
    private void UpdateTime()
    {
        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;

        if (seconds > 59)
            seconds = 59;

        if (minutes < 0)
        {
            Reset();
            CallTargetEvent();
        }
    }
    void Update()
    {
        if (stop || targetClicker.numOfBases == 0)
            return;

        if (!pause)
        {
            timeLeft -= Time.deltaTime;

        }

        if (maxTime <= 0.1f)
        {
            Bar.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            Bar.transform.localScale = new Vector3(normalizeTimeLeft(), 1f, 1f);
        }

        UpdateTime();
    }

    float normalizeTimeLeft()
    {
        return (timeLeft - MIN_TIME) / (maxTime - MIN_TIME);
    }

    public string GetProductionPerTime()
    {
        string income = targetClicker.TotalBaseIncome.ToEngeneeringString();
        string result = "";

        if (targetClicker.TotalBaseIncome == LargeNumber.zero)
        {
            income = targetClicker.baseIncomePerProduce.ToEngeneeringString();
        }
        if (minutes == 0)
        {
            if (maxTime < 1f)
            {
                result = string.Format("{0}/{1}s", income, maxTime);
            }
            else
            {
                result = string.Format("{0}/{1}s", income, Mathf.RoundToInt(seconds));

            }
        }
        else
        {

            result = string.Format("{0}/{1}m {2}s", income, Mathf.RoundToInt(minutes), Mathf.RoundToInt(seconds));

        }

        return result;
    }
    private void FixedUpdate()
    {
        text.text = GetProductionPerTime();
    }
    public void CallTargetEvent()
    {
        targetObject.SendMessage("Produce");
    }
}

