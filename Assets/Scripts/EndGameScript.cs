using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    CaseManager caseManager;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI endText;

    private void OnEnable()
    {
        caseManager = FindObjectOfType<CaseManager>();
        int i = 0;
        foreach (CaseManager.Case _case in caseManager.cases)
        {
            if (_case.CorrectOutcome)
            {
                i++;
            }
        }

        if (i == 2)
        {
            headerText.text = "WELL DONE!";
            endText.text = "Looks like you cracked every case! That's why you're the best detective on the force. Well done, we can take it from here.\n\nCases solved: 2/2";
        }
        else if (i == 1)
        {
            headerText.text = "NICE JOB.";
            endText.text = "Well, that was something. Hey, could have been worse... At least you successfully deduced one of the perps. We can take it from here.\n\nCases solved: 1/2";
        }
        else if (i == 0)
        {
            headerText.text = "YOU'RE FIRED!";
            endText.text = "Good lord, what are you doing!? You've got to be the worst detective on Earth! Get out of here, and don't bother coming back.\n\nCases solved: 0/2";
        }
        else
        {
            endText.text = "Thanks for playing!";
        }
    }
}