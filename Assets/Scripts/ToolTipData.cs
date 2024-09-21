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
        // Find the TMP_Text object for the tooltip
        tooltipTMP = GameObject.Find("TooltipText").GetComponent<TMP_Text>();
        tooltipTMP.gameObject.SetActive(false); // Ensure the tooltip is initially hidden
    }

    // When the mouse enters this UI object
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the tooltip with the correct text
        tooltipTMP.text = tooltipText;
        tooltipTMP.gameObject.SetActive(true);
    }

    // When the mouse exits this UI object
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip when no longer hovering
        tooltipTMP.gameObject.SetActive(false);
    }

}