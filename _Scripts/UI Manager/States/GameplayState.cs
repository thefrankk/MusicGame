using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : UIState
{
    public GameplayState(UIManagers uiManager) 
    {
        this._uiManager = uiManager;
    }

    public override void EnterState()
    {
       _uiManager.GameplayUI.gameObject.SetActive(true);
       GameManager.Instance.ContainerGameplay.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.GameplayUI.gameObject.SetActive(false);
        GameManager.Instance.ContainerGameplay.SetActive(false);

        GameManager.Instance.ResetGameToStart();
        
        Time.timeScale = 1.0f;

    }
}