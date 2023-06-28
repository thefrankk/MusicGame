using System;
using UnityEngine;

public class PauseState : UIState
{
    public PauseState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.PauseUI.gameObject.SetActive(true);
        MusicManager.Instance.Pause();
        Time.timeScale = 0.0f;
    }

    public override void ExitState()
    {
        _uiManager.PauseUI.gameObject.SetActive(false);
        MusicManager.Instance.UnPause();

        Time.timeScale = 1.0f;
    }
}