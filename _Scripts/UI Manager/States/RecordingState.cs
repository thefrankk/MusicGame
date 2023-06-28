using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingState : UIState, IGameplaySettings
{

    public RecordingState(UIManagers uiManager) 
    {
        this._uiManager = uiManager;
    }

    public override void EnterState()
    {
        _uiManager.RecordingUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.RecordingUI.gameObject.SetActive(false);

    }

   

    public GameData GetGameplaySettings()
    {
        throw new System.NotImplementedException();
    }
}
