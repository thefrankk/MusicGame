using System;

public class GameOverState : UIState
{
    public GameOverState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.GameOverUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.GameOverUI.gameObject.SetActive(false);

    }
}