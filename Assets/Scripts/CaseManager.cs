using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaseManager : MonoBehaviour
{
    public Case[] cases; // Manually set how many we want in InitialiseCases()
    static public bool startupPerformed;
    public int currentCaseNo; // savedata
    [HideInInspector] public Case myCurrentCase;
    private TimeManager timeManager;
    [SerializeField] private int startingCaseNo;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        timeManager = FindObjectOfType<TimeManager>();
        if (!startupPerformed)
        {
            currentCaseNo = startingCaseNo; // take out if savedata implemented
            InitialiseCases();
            startupPerformed = true;
        }
        timeManager.ResetTime();
    }

    void InitialiseCases() 
    {
        cases = new Case[2]; // As we finish cases we can manually update this
        for (int i = 0; i < cases.Length; i++)
        {
            cases[i].caseNo = i+1;
            switch (i)
            {
                case 0: // We will manually set the details of the case in the initaliser
                    {   // I'm sure there is a prettier method but my head is fried rn
                        // We could make arrays for handling the data that will be initialised
                        cases[i].caseName = "Burnt Cupcakes </3";
                        cases[i].caseSuspectGuilty = true;
                        cases[i].caseTime = 240;
                        break;
                    }
                case 1:
                    {
                        cases[i].caseName = "AN Untimely Death";
                        cases[i].caseSuspectGuilty = false;
                        cases[i].caseTime = 270;
                        break;
                    }
                default:
                    {
                        Debug.Log("HEEEEEEEEEEELP");
                        break;
                    }
            }

            if (startingCaseNo > i+1)
            {
                cases[i].UpdateCase(Case.CaseState.Completed, true);
            }
            else if (startingCaseNo == i+1)
            {
                cases[i].UpdateCase(Case.CaseState.Active, false);
            }
        }
        cases[startingCaseNo-1].MyCaseState = Case.CaseState.Active;
        myCurrentCase = cases[startingCaseNo-1];
        timeManager.maxTime = cases[startingCaseNo - 1].caseTime;
        timeManager.ResetTime();
    }

    public void CompleteCase(bool decidedGuilty)
    {
        //Debug.Log(myCurrentCase.caseName);
        if (myCurrentCase.MyCaseState != Case.CaseState.Completed)
        {
            if (decidedGuilty && myCurrentCase.caseSuspectGuilty || !decidedGuilty && !myCurrentCase.caseSuspectGuilty)
            {
                Debug.Log("YEAHHH");
                myCurrentCase.UpdateCase(Case.CaseState.Completed, true);
            }
            else
            {
                Debug.Log("NAURRR"); myCurrentCase.UpdateCase(Case.CaseState.Completed, false);
            }

            myCurrentCase.DisableObjects();

            cases[currentCaseNo - 1] = myCurrentCase;

            if (myCurrentCase.caseNo < cases.Length)
            {
                currentCaseNo++;
                myCurrentCase = cases[currentCaseNo-1];
                myCurrentCase.UpdateCase(Case.CaseState.Active, false);
                myCurrentCase.EnableObjects();
                timeManager.maxTime = myCurrentCase.caseTime;
                timeManager.ResetTime();
            }
        }
    }

    [Serializable]
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
            if (caseObjects != null)
            {
                for (int i = 0; i < caseObjects.Count; i++)
                {
                    caseObjects[i].SetActive(false);
                }
            }
        }

        public void EnableObjects()
        {
            if (caseObjects != null)
            {
                for (int i = 0; i < caseObjects.Count; i++)
                {
                    caseObjects[i].SetActive(true);
                }
            }
        }
    }

    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
