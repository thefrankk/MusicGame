
using System;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    private static DiamondManager _diamondManager;
    private static HeartsManager _heartsManager;
    private static ScoreManager _scoreManager;
    private static ContinuesManager _continuesManager;
    
    //Properties
    public static DiamondManager DiamondManager => _diamondManager;
    public static HeartsManager HeartsManager => _heartsManager;
    public static ScoreManager ScoreManager => _scoreManager;
    public static ContinuesManager ContinuesManager => _continuesManager;
    
   


    private void Awake()
    {        

        Initialize();
    }
    
    
    public void Initialize()
    {
        _diamondManager = new DiamondManager();
        _heartsManager = new HeartsManager();
        _scoreManager = new ScoreManager();
        _continuesManager = new ContinuesManager();
    }
}
