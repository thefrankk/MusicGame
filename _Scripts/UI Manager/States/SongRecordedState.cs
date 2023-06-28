public class SongRecordedState : UIState
{
    public SongRecordedState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.SongRecordedUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.SongRecordedUI.gameObject.SetActive(false);

    }
}