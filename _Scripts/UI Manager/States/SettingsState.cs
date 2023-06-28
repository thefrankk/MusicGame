using System;

public class SettingsState : UIState
{
    public SettingsState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.SettingsUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.SettingsUI.gameObject.SetActive(false);

    }
}