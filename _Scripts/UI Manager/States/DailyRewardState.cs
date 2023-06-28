public class DailyRewardState : UIState
{
    public DailyRewardState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.DailyRewardUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.DailyRewardUI.gameObject.SetActive(false);

    }
}