using System;

public class WinState : UIState
{
    public WinState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
       _uiManager.WinUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.WinUI.gameObject.SetActive(false);

    }
}