public class NotEnoughLevelState : UIState
{
    public NotEnoughLevelState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.NotEnoughLevel.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.NotEnoughLevel.gameObject.SetActive(false);

    }
}