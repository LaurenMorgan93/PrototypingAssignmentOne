using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  // Morality Variables
  public Slider moralityMeter;
  private int currentCopStanding; 

  void Start()
  {
    // initlialise the cop Standing meter 
    currentCopStanding = 50;
    moralityMeter.value = currentCopStanding;
  }

  public void ChangeCopStanding(int value) // call the function to change the current value of the morality meter by an amount
  {
    currentCopStanding += value;
    moralityMeter.value = currentCopStanding;
  }
    
}
