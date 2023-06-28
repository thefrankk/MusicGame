
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerLevelManager : MonoBehaviour
{

    public static PlayerLevelManager Instance;
    private int _level;
    private float _currentExp;
    private float _expNedeed;

    private int _diamondsReward;
    
    //Properties
    public int Level => _level;
    public float CurrentExp => _currentExp;
    public float ExpNedeed => _expNedeed;


    public Action OnLevelUp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
            Destroy(this.gameObject);
        
        Initialize();
    }


    public void Initialize()
    {
        loadData();
    }
    
    public bool AddExp(int amount)
    {
        _currentExp += amount;
        if (_currentExp >= _expNedeed)
        {
            _level++;
            var difference = _currentExp - _expNedeed;
            _currentExp = 0;
            _expNedeed += 150 + (_expNedeed * 50 / 100);
            AddExp((int)difference);
            OnLevelUp?.Invoke();

            rewards();

            return true;
        }

        saveData();
        return false;
        
    }

    private void rewards()
    {
        Consumables.HeartsManager.AddMaxHearts(1);
        Consumables.HeartsManager.SetHeartsToMax();

        var minDiamonds = 10 + _level;
        var maxDiamonds = 20 + _level + ((_expNedeed) / 100);

        _diamondsReward = (int)Random.Range(minDiamonds, maxDiamonds);
        Consumables.DiamondManager.Add(_diamondsReward);
    }
    private void loadData()
    {
        _level = PlayerPrefs.GetInt("LEVEL", 0);
        _currentExp = PlayerPrefs.GetFloat("CURRENTEXP", 0);
        _expNedeed = PlayerPrefs.GetFloat("EXPNEEDED", 350);;
    }

    private void saveData()
    {
        PlayerPrefs.SetInt("LEVEL", _level);
        PlayerPrefs.SetFloat("CURRENTEXP", _currentExp);
        PlayerPrefs.SetFloat("EXPNEEDED", _expNedeed);;
    }
}
