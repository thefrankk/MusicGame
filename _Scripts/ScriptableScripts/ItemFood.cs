using UnityEngine;

[CreateAssetMenu(fileName = "ItemFood", menuName = "ScriptableObjects/ItemFood", order = 1)]
public class ItemFood : ItemForBuy
{
    [SerializeField] private string _name;
    
    
    //Properties
    public string Name => _name;
    public override void LoadItemData()
    {
        if (_isUnlocked == true)
        {
            _isUnlocked = PlayerPrefs.GetInt(_name + "unlocked", 1) == 1;
        }
        else
        {
            _isUnlocked = PlayerPrefs.GetInt(_name + "unlocked", 0) == 1;
        }
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(_name + "unlocked", 1);

    }
}