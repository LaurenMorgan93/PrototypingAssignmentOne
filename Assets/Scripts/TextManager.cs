using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text textObj;  
    public float typingSpeed = 0.05f;  // Delay between each character
    private Coroutine typingCoroutine;

    void Start()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        string message = textObj.text;
        
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);  // Stop any currently running typing
        }

        typingCoroutine = StartCoroutine(TypeSentence(message));
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        
        textObj.text = "";  // Clear the text initially
        
        yield return new WaitForSeconds(0.05f);

        foreach (char letter in sentence.ToCharArray())
        {
            textObj.text += letter;  // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed);  // Wait for the typing speed delay
        }

        typingCoroutine = null;  // Reset coroutine reference when finished
    }

 
}
