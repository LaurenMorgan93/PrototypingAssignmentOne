using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
        //myCurrentCase.caseState = Case.CaseState.Completed;
        //myCurrentCase.UpdateCase(); // update state

        if (decidedGuilty && myCurrentCase.caseSuspectGuilty || !decidedGuilty && !myCurrentCase.caseSuspectGuilty)
        {
            Debug.Log("YEAHHH");
            //myCurrentCase.correctOutcome = true;
            myCurrentCase.UpdateCase(Case.CaseState.Completed, true);
        }
        else {
            Debug.Log("NAURRR)"); myCurrentCase.UpdateCase(Case.CaseState.Completed, false);
        }

        myCurrentCase.DisableObjects();

        //foreach (GameObject obj in myCurrentCase.caseObjects) { obj.SetActive(false); }
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
            myCurrentCase.EnableObjects();
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
        
        public void UpdateCase(/*int _caseNo, string _caseName, */CaseState _caseState, /*bool _caseSuspectGuilty,*/ bool _correctOutcome /*List<GameObject> _caseObjects*/)
        {
            //caseNo = _caseNo;
            //caseName = _caseName;;
            caseState = _caseState;
            //caseSuspectGuilty = _caseSuspectGuilty;
            correctOutcome = _correctOutcome;
            /*for (int i = 0; i < _caseObjects.Count; i++)
            {
                caseObjects[i] = _caseObjects[i];
            }*/
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

    /*Case UpdateCase(Case caseToUpdate)
    {
        return new Case { caseNo = caseToUpdate.caseNo, caseName = caseToUpdate.caseName, caseState = caseToUpdate.caseState, caseSuspectGuilty = caseToUpdate.caseSuspectGuilty, correctOutcome = caseToUpdate.correctOutcome, caseObjects = caseToUpdate.caseObjects };
    }*/
}
