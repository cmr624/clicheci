using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScene : MonoBehaviour
{
   public void LoadNextScene()
   {
      GameFlowManager.Instance.LoadNextScene();
   }

   public void LoadScene(string name)
   {
      GameFlowManager.Instance.LoadScene(name);
   }
}
