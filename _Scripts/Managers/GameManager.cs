using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    [FormerlySerializedAs("ContainerCharacter")]
    [Header("Character")]
    [SerializeField] private GameObject _containerCharacter;
    [FormerlySerializedAs("CharacterLeft")] [SerializeField] private CharacterControllers _characterLeft;
    [FormerlySerializedAs("CharacterRight")] [SerializeField] private CharacterControllers _characterRight;

    [FormerlySerializedAs("PositionLeft")]
    [Header("Position")]
    [SerializeField] private Transform _positionLeft;
    [FormerlySerializedAs("PositionRight")] [SerializeField] private Transform _positionRight;

   

    [Header("Gameplay  container setup")]
    [SerializeField] private GameObject _containerGameplay;
    
    [Header("Boolean Manager")]
    internal bool CheckStartingGame = false;

 
    private GameplayPopup _gameplayPopup; 
    private SpawnerLogic _spawnerLogic;
    private Sprite _foodSprite;
    

   
    //Game data
    private GameData _gameData;
    private int _currentDiff;
    private string _currentSongType;
    
    //Properties
    public GameObject ContainerGameplay => _containerGameplay; 
    public GameData GameData => _gameData;
    public Sprite FoodSprite => _foodSprite;
    
    
  
    //Actions
    public Action OnDamageReceived;
    
    
    
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this.gameObject);
        Application.targetFrameRate = 120;

        _spawnerLogic = FindObjectOfType<SpawnerLogic>(true);
        _gameplayPopup = FindObjectOfType<GameplayPopup>(true);
        
    }

    private void OnEnable()
    {
        _gameplayPopup.OnGameStarted += OnGameStarted;
    }

    
    private void OnDisable()
    {
        _gameplayPopup.OnGameStarted -= OnGameStarted;

    }
  

    //Start game with game data, this enables the Gameplay UI and waits until the player press two buttons to start the gameplay.
    public void StartGame(ref GameData gameData)
    {
        _gameData = gameData;
        
        _containerCharacter.transform.localPosition = new Vector3(0, 0 , 0);//-0.49
        
        UIManagers.Instance.OnLoadingButton(() =>
        {
            _characterLeft.transform.localPosition = _positionLeft.transform.localPosition;
            _characterRight.transform.localPosition = _positionRight.transform.localPosition;
            UIManagers.Instance.OnPlayButton();
        });
        
    }
    
    //Start the gameplay
    private void OnGameStarted()
    {
       _spawnerLogic.StartSpawner(_gameData.AllNotes, (int)_gameData.Difficulty);
       
       MusicManager.Instance.StartSong(_gameData.DelayOfSong);
       BarFiller.StartFiller(MusicManager.Instance.GetCurrentSongLenght());
       BarFiller.OnFillerEnded += OnGameWin;

    }

    public void OnGameWin()
    {
        
        //Save score
        var prevScore = PlayerPrefs.GetInt(MusicManager.Instance.GetCurrentSongName() + "score" + _gameData.Difficulty, 0);

        var alreadyPlayed = prevScore == 0 ? false : true;
        
        if (prevScore < Consumables.ScoreManager.Score)
        {
            PlayerPrefs.SetInt( MusicManager.Instance.GetCurrentSongName()+ "score" + _gameData.Difficulty, Consumables.ScoreManager.Score);
        }
        //Unlock next diff of the song
        var diff = (int)_gameData.Difficulty >= 2 ? 2 : (int)_gameData.Difficulty + 1;
        PlayerPrefs.SetInt(MusicManager.Instance.GetCurrentSongName()+diff+"lockStatus", 1);

        
        Reward allRewards = reward(alreadyPlayed);
        
        
        //Save exp
        if (PlayerLevelManager.Instance.AddExp(allRewards.Exp))
        {
            UIManagers.Instance.OnLevelUpButton(out Button button, PlayerLevelManager.Instance.Level.ToString(), Consumables.HeartsManager.Hearts.ToString(),allRewards.Diamonds.ToString(), $"{PlayerLevelManager.Instance.CurrentExp} / {PlayerLevelManager.Instance.ExpNedeed}", (PlayerLevelManager.Instance.CurrentExp/PlayerLevelManager.Instance.ExpNedeed).ToString() );
            button.onClick.AddListener(() =>
            {
                onWinButton(allRewards);
            });
        }
        else
        {
            onWinButton(allRewards);
        }
        
       
    }

    private void onWinButton(Reward allRewards)
    {
        UIManagers.Instance.OnWinButton(
            PlayerLevelManager.Instance.Level.ToString(),
            allRewards.Hearts.ToString(),
            allRewards.Diamonds.ToString(),
            PlayerLevelManager.Instance.CurrentExp.ToString(),
            (PlayerLevelManager.Instance.CurrentExp/PlayerLevelManager.Instance.ExpNedeed).ToString(),
            Consumables.ScoreManager.Score.ToString(),
            MusicManager.Instance.GetCurrentSongName(), 
            _gameData.Difficulty.ToString()
        );
    }

    public void OnGameLoose()
    {
      
        UIManagers.Instance.OnGameOverButton(Consumables.ContinuesManager.Continues.ToString());
        
    }

    public void ReceiveDamage()
    {
        Consumables.HeartsManager.Substract(1);
        OnDamageReceived?.Invoke();
    }

    public void AddScore()
    {
        Consumables.ScoreManager.Add(10);
    }


    private Reward reward(bool alreadyPlayed = false)
    {

        var diamondsReward = 0;
        var minDiamonds = 10 + PlayerLevelManager.Instance.Level;
        var maxDiamonds = 20 + PlayerLevelManager.Instance.Level + ((PlayerLevelManager.Instance.ExpNedeed) / 100);

        var diamonds = (int)UnityEngine.Random.Range(minDiamonds, maxDiamonds);
        diamondsReward =  alreadyPlayed == true ? diamonds / 2 : diamonds;
        Consumables.DiamondManager.Add(diamondsReward);

        var heartReward = 0;
        var minHearts = 3 + (Consumables.HeartsManager.MaxHearts * 10 / 100);
        var maxHearts = 5 + PlayerLevelManager.Instance.Level + ((PlayerLevelManager.Instance.ExpNedeed) / 100)  + (Consumables.HeartsManager.MaxHearts * 15 / 100);
        
        var hearts = (int)UnityEngine.Random.Range(minHearts, maxHearts);
        heartReward = alreadyPlayed == true ? hearts / 2 : hearts;
        Consumables.HeartsManager.Add(heartReward);

        var exp = Consumables.ScoreManager.Score / 8;
        var expEarned = alreadyPlayed == true ? exp / 2 : exp;
        
        return new Reward(diamondsReward, heartReward, expEarned);
    }


    public void StopGame()
    {
        MusicManager.Instance.Pause();
        BarFiller.PauseFiller();
        _spawnerLogic.StopSpawner();

        FoodLogic[] allFood = FindObjectsOfType<FoodLogic>();
        foreach (FoodLogic food in allFood)
        {
            Destroy(food.gameObject);
        }
    }


    public void ResumeGame()
    {
        GameStarter.Instance.StartGame(() =>
        {
            MusicManager.Instance.UnPause();
            BarFiller.UnPauseFiller();
            _spawnerLogic.ReanudeSpawner();
        });

    }

  
    public void ResetGameToStart()
    {
        MusicManager.Instance.StopSong();
        _spawnerLogic.ResetSpawner();
        BarFiller.StopFiller();
        
        Consumables.ContinuesManager.SetContinuesToMax();

      
    }

    public void ReplayGame()
    {
         _gameplayPopup.ResetUI();
        UIManagers.Instance.OnPlayButton();
    }

    public int GetCurrentDiff() => _currentDiff;
    public void SetCurrentDiff(int diff)
    {
        _currentDiff = diff ;
    }

    public void SetCurrentSongType(string type)
    {
        _currentSongType = type;
    }

    public string GetCurrentSongType() => _currentSongType;
    public void SetFoodSprite(Sprite sprite)
    {
        _foodSprite = sprite;
    }
}

public struct Reward
{
    public int Diamonds;
    public int Hearts;
    public int Exp;
    
    public Reward(int diamonds, int hearts, int exp)
    {
        Diamonds = diamonds;
        Hearts = hearts;
        Exp = exp;
    }
}
