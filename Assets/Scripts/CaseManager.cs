using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public Case[] cases; // Manually set how many we want in InitialiseCases()
    public int currentCaseNo; // savedata
    [SerializeField] private Case myCurrentCase;
    private TimeManager timeManager;

    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        InitialiseCases();
    }

    void InitialiseCases() 
    {
        cases = new Case[2];
        
    }

    public void CompleteCase(bool decidedGuilty)
    {
        myCurrentCase = cases[currentCaseNo];
        Debug.Log(myCurrentCase.caseName);
        //myCurrentCase.caseState = Case.CaseState.Completed;
        //myCurrentCase.UpdateCase(); // update state

        if (decidedGuilty && myCurrentCase.caseSuspectGuilty || !decidedGuilty && !myCurrentCase.caseSuspectGuilty)
        {
            Debug.Log("YEAHHH");
            //myCurrentCase.correctOutcome = true;
            myCurrentCase.UpdateCase(Case.CaseState.Completed, true);
        }
        else {
            Debug.Log("NAURRR"); myCurrentCase.UpdateCase(Case.CaseState.Completed, false);
        }

        myCurrentCase.DisableObjects();

        //foreach (GameObject obj in myCurrentCase.caseObjects) { obj.SetActive(false); }
        if (currentCaseNo == cases.Length)
        {
            Debug.Log("reached limit");
            // END THE GAME
        }
        else // ADVANCE CASE
        {
            currentCaseNo++;
            myCurrentCase = cases[currentCaseNo-1];
            //myCurrentCase.myCaseState = Case.CaseState.Active; //UpdateCase()
            myCurrentCase.UpdateCase(Case.CaseState.Active, false);
            myCurrentCase.EnableObjects();
            timeManager.maxTime = myCurrentCase.caseTime;
            timeManager.ResetTime();
        }
    }

    //[Serializable]
    public struct Case
    {
        public int caseNo;
        public string caseName;
        [SerializeField] private CaseState myCaseState; // savedata
        public CaseState MyCaseState
        {
            get { return myCaseState; }
            set { myCaseState = value; }
        }
        public bool caseSuspectGuilty; // if the correct decision is to find the suspect guilty
        [SerializeField] private bool correctOutcome; // if the player made the correct decision in this case
        public bool CorrectOutcome
        {
            get { return correctOutcome; }
            set { correctOutcome = value; }
        }
        public List<GameObject> caseObjects;
        public int caseTime; // How much time the player has to solve this case 

        public enum CaseState
        {
            Unstarted,
            Active,
            Completed
        }
        
        public void UpdateCase(CaseState _caseState, bool _correctOutcome) // Only need to update the variables that will change. e.g. caseNo is a predefined variable to indicate the order of the cases, and won't change
        {
            MyCaseState = _caseState;
            CorrectOutcome = _correctOutcome;
        }

        public void DisableObjects()
        {
            for (int i = 0; i < caseObjects.Count; i++)
            {
                caseObjects[i].SetActive(false);
            }
        }

        public void EnableObjects()
        {
            for (int i = 0; i < caseObjects.Count; i++)
            {
                caseObjects[i].SetActive(true);
            }
        }
    }
}
