using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScene : MonoBehaviour
{
   public void PlayAgain()
   {
      GameFlowManager.Instance.PlayButtonPressed();
   }

   public void StartChaos()
   {
      GameFlowManager.Instance.StartChaosMode();
   }
   
   
   public void NextGame()
   {
      GameFlowManager.Instance.LoadNextMinigame();
   }

   public void LoadScene(string name)
   {
      GameFlowManager.Instance.LoadScene(name);
   }
}
