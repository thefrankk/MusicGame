public class MoreHeartsState : UIState
{
    public MoreHeartsState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.MoreHeartsUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.MoreHeartsUI.gameObject.SetActive(false);

    }
}