using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : ConsumableManager
{

    
    public DiamondManager()
    {
        Instance = this;
        _asset = PlayerPrefs.GetInt("diamonds", 1750);
    }
    
    //Properties
    public int Diamonds => _asset;

    public void TryToBuy(int amount, Action<bool> callback)
    {
        if (_asset >= amount)
        {
            Substract(amount);
            callback(true);
        }
        else
        {
            callback(false);
        }
    }
   
 
    public override void Add(int amount)
    {
        _asset += amount;
        OnAssetChanged?.Invoke();
        saveDiamonds();

    }

    public override void Substract(int amount)
    {
       _asset -= amount;
       OnAssetChanged?.Invoke();
       saveDiamonds();

    }
    
    private void saveDiamonds()
    {
        PlayerPrefs.SetInt("diamonds", _asset);
    }
}
