
using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private void OnEnable()
    {
        GameManager.Instance.OnDamageReceived += checkForLoose;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDamageReceived -= checkForLoose;
    }
    private void checkForLoose()
    {
        if(Consumables.HeartsManager.Hearts <= 0)
        {
            GameManager.Instance.StopGame();
           
            GameManager.Instance.OnGameLoose();
          
        }
    }


    public void Revive()
    {
        GameManager.Instance.ResumeGame();
    }

}
