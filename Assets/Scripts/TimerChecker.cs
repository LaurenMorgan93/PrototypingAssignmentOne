using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerChecker : MonoBehaviour
{
    // get the player's remaining time balance
    private int playerTimeRemaining;

    // the time cost for unlocking this piece of evidence
    public int leadTimeCost;

    // the button to unlock the evidence
    public Button spendTimeButton;

    private bool alreadyPurchased = false;

    //reference to the TimeManager script where the timer is handled / saved
    public TimeManager timeManagementScript;

    void OnEnable() // this checker is called every time the relevant panel is opened (Set Active) by the player. 
    {
        timeManagementScript = FindObjectOfType<TimeManager>(); // This is really slow and inoptimal but for a prototype it shouldnt make a difference, just didn't want to have to update the scene on git -Daragh
        // check the CaseManager script for the player's remaining time. 
        playerTimeRemaining = timeManagementScript.currentTime;
        
       
        // if the player does not have enough time remaining to unlock this evidence 
        if (leadTimeCost > playerTimeRemaining || alreadyPurchased)
        {
            // stop the button from being interactable
            spendTimeButton.interactable = false;
        }

        else
        {
            // let the button be interactable so that they can buy the evidence.
            spendTimeButton.interactable = true;
        }
    }

    public void flagEvidenceAsPurchased()
    {
        alreadyPurchased = true;
    }
}
