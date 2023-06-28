public class LevelUpState : UIState
{
    public LevelUpState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.LevelUp.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.LevelUp.gameObject.SetActive(false);

    }
}