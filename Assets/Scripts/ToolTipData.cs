using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ToolTipData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText; // The text for the tooltip

    private TMP_Text tooltipTMP; // Reference to the tooltip TMP_Text object

    private void Start()
    {
        // Find the TMP_Text object for the tooltip in the current object's children
        tooltipTMP = transform.Find("TooltipText").GetComponent<TMP_Text>();
        if (tooltipTMP != null)
        {
            tooltipTMP.gameObject.SetActive(false); // Ensure the tooltip is initially hidden
        }
        else
        {
            Debug.LogError("TooltipText object not found in children of " + gameObject.name);
        }
    }

    // When the mouse enters this UI object
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipTMP != null)
        {
            tooltipTMP.text = tooltipText;
            tooltipTMP.gameObject.SetActive(true);
        }
    }

    // When the mouse exits this UI object
    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipTMP != null)
        {
            tooltipTMP.gameObject.SetActive(false);
        }
    }
}