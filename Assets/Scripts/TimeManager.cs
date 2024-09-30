using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int maxTime, currentTime;
    public TextMeshProUGUI displayTimeText;
    private CaseManager caseManager;

    public void Awake()
    {
        //ResetTime(); // if we implement save data, remove this
    }

    private void OnEnable()
    {
        caseManager = FindObjectOfType<CaseManager>();
        maxTime = caseManager.myCurrentCase.caseTime;
        ResetTime();
    }

    public void ResetTime()
    {
        currentTime = maxTime;
        UpdateTimeDisplay();
    }

    public void UpdateTimeDisplay()
    {
        if (displayTimeText != null)
        {
            displayTimeText.text = "Time remaining : " + currentTime.ToString();
        }
    }

    public void SpendTime(int timeCost)
    {
        Debug.Log("Time before spending: " + currentTime);
        if (currentTime > maxTime) // Catch in case current time has increased unexpectedly
        {
            currentTime = maxTime;
        }
        if (currentTime < timeCost) // This shouldn't ever run as the evidence checks this first, but just in case
        {
            Debug.Log("Can't afford to spend time here.");
            return;
        }
        currentTime -= timeCost;
        if (currentTime < 0)
        {
            Debug.Log("Gone under time somehow");
            currentTime = 0;
            return;
        }
        if (currentTime == 0)
        {
            currentTime = 0;
        }
        Debug.Log("Time after spending: " + currentTime);
        UpdateTimeDisplay();
    }
}
