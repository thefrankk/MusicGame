
using UnityEngine;

public class HeartsManager : ConsumableManager
{
    private int _maxHearts;

    public int MaxHearts => _maxHearts;
    public HeartsManager()
    {
        Instance = this;
        _asset = PlayerPrefs.GetInt("hearts", 10);
        _maxHearts = PlayerPrefs.GetInt("maxhearts", 10);
    }
    
    //Properties
    public int Hearts => _asset;

    public override void Add(int amount)
    {
       _asset += amount;
       if(_asset > _maxHearts)
           _asset = _maxHearts;
       
       SaveHearts();
    }

    public void SetHeartsToMax()
    {
        _asset = _maxHearts;
    SaveHearts();
    }
    
    public void AddMaxHearts(int amount)
    {
        _maxHearts += amount;
        PlayerPrefs.SetInt("maxhearts", _maxHearts);
    }
    public override void Substract(int amount)
    {
        if (_asset <= 0)
            return;
        
       _asset -= amount;
       SaveHearts();
    }
    
    public void SaveHearts()
    {
        PlayerPrefs.SetInt("hearts", _asset);
    }
}
