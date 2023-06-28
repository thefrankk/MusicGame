using UnityEngine;

[CreateAssetMenu(fileName = "ItemCharacter", menuName = "ScriptableObjects/ItemCharacter", order = 1)]
public class ItemCharacter : ItemForBuy
{
    [SerializeField] private PlayerData _playerData;
    
    
    public PlayerData PlayerData => _playerData;


    public override void LoadItemData()
    {
        if (_isUnlocked == true)
        {
            _isUnlocked = PlayerPrefs.GetInt(_playerData.name + "unlocked", 1) == 1;
        }
        else
        {
            _isUnlocked = PlayerPrefs.GetInt(_playerData.name + "unlocked", 0) == 1;
        }
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(_playerData.name + "unlocked", 1);
    }
}