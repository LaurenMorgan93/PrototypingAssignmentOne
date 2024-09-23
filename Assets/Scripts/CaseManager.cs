using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    [SerializeField] public List<Case> cases = new();
    public int currentCaseNo; // savedata
    private Case myCurrentCase;

    /*public void AdvanceCase(bool decidedGuilty)
    {
        myCurrentCase = cases[currentCaseNo];

        switch (myCurrentCase.caseState)
        {
            case (Case.CaseState.Unstarted):
                {
                    myCurrentCase.caseState = Case.CaseState.Active;
                    break;
                }
            case (Case.CaseState.Active):
                {
                    CompleteCase(decidedGuilty);
                    break;
                }
            case (Case.CaseState.Completed):
                {
                    Debug.Log("Trying to advance on an already completed case : (");
                    break;
                }
            default:
                {
                    // panic
                    break;
                }
        }
    }*/

    public void CompleteCase(bool decidedGuilty)
    {
        myCurrentCase = cases[currentCaseNo];
        Debug.Log(myCurrentCase.caseName);
        myCurrentCase.caseState = Case.CaseState.Completed;
        //if (decidedGuilty == myCurrentCase.caseSuspectGuilty) { myCurrentCase.correctOutcome = true; }
        if (decidedGuilty && myCurrentCase.caseSuspectGuilty || !decidedGuilty && !myCurrentCase.caseSuspectGuilty) { Debug.Log("YEAHHH"); myCurrentCase.correctOutcome = true; }
        else { Debug.Log("NAURRR)"); myCurrentCase.correctOutcome = false; }

        foreach (GameObject obj in myCurrentCase.caseObjects) { obj.SetActive(false); }
        if (currentCaseNo == cases.Count)
        {
            Debug.Log("reached limit");
            // END THE GAME
        }
        else
        {
            currentCaseNo++;
            myCurrentCase = cases[currentCaseNo-1];
            myCurrentCase.caseState = Case.CaseState.Active;
            foreach (GameObject obj in myCurrentCase.caseObjects) { obj.SetActive(true); }
        }
    }

    [Serializable]
    public struct Case
    {
        public int caseNo;
        public string caseName;
        public CaseState caseState; // savedata
        public bool caseSuspectGuilty; // if the correct decision is to find the suspect guilty
        public bool correctOutcome; // if the player made the correct decision in this case
        public List<GameObject> caseObjects;

        public enum CaseState
        {
            Unstarted,
            Active,
            Completed
        }
    }
}
