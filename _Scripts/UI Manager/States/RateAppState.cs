public class RateAppState : UIState
{
    public RateAppState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.RateAppUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.RateAppUI.gameObject.SetActive(false);
    }
}