using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecisionButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    private CaseManager caseManager;

    public void OnClick(bool guilty)
    {
        caseManager = FindObjectOfType<CaseManager>();
        caseManager.CompleteCase(guilty);
    }
}
