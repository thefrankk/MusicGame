using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class ItemForBuy : ScriptableObject
{

    
    [Header("Data")] 
    [SerializeField] private int _levelRequired;
    [SerializeField] private int _price;
    [SerializeField] protected bool _isUnlocked;
    [SerializeField] private bool _isSelected;
    [SerializeField] protected Sprite _itemImage;
    [SerializeField] private int _id;
    

    
    //Properties
    public int LevelRequired => _levelRequired;
    public int Price => _price;
    public bool IsUnlocked => _isUnlocked;
    public bool IsSelected => _isSelected;
    public Sprite ItemImage => _itemImage;
    public int Id => _id;


    public abstract void LoadItemData();
  

    public void Unlock()
    {
        _isUnlocked = true;
        Save();
    }

    public abstract void Save();

}