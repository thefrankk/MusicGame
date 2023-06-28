
using UnityEngine;

public class ContinuesManager : ConsumableManager
{
    private int _maxContinues;
    public ContinuesManager()
    {
        Instance = this;
        _maxContinues = PlayerPrefs.GetInt("continues", 1);
        _asset = _maxContinues;
    }

    //properties
    public int Continues => _asset;
    public int MaxContinues => _maxContinues;
    public override void Add(int amount)
    {
        _asset += amount;

    }

    public override void Substract(int amount)
    {
        _asset -= amount;
    }

    public void SetContinuesToMax()
    {
        _asset = _maxContinues;
    }
    
    public void SetMaxContinues()
    {
        _maxContinues++;
        saveContinues();
    }
    private void saveContinues()
    {
        PlayerPrefs.SetInt("continues", _asset);
    }
}
