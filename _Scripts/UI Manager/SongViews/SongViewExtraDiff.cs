
    using TMPro;
    using UnityEngine;

    public class SongViewExtraDiff : SongView
    {

        [SerializeField] private TextMeshProUGUI _score;
        
       
        protected override void Start()
        {
            _songData = GetComponentInParent<SongViewSelect>().SongData;
            base.Start();
            
            _score.text = _songData.ScoresExtraDiff[(int)_currentSongDiff].ToString();
           
        }

        public override void CheckBackData()
        {
            _unlockedSong.SetActive(_songData.IsExtraDiffUnlocked);
            _lockedSong.SetActive(!_songData.IsExtraDiffUnlocked);

            if (!_songData.IsExtraDiffUnlocked)
            {
            
                if(PlayerLevelManager.Instance.Level < _songData.LevelRequieredExtraDiff)
                {
                    _levelRequired.text = _songData.LevelRequieredExtraDiff.ToString();
                    _levelRequired.color = Color.red;
                }
                else
                {
                
                }
            
            
                _unlockPrice.text = _songData.UnlockPriceExtraDiff.ToString();
            }
        }


        protected override void setDiff()
        {
            Color currentColor = Color.green;
            if(CurrentDifficultyMode == 0)
            {
                _currentSongDiff = SongDiff.Easy;
           
                Type.text = "EASY";
                currentColor = Color.magenta;
                Type.color = currentColor;
            }
            else if(CurrentDifficultyMode == 1)
            {

                if (!_songData.DifficultiesLocks[1])
                {
                    Type.text = "LOCKED";
                    currentColor = Color.yellow;
                    Type.color = Color.yellow;
                    return;
                }
            
                _currentSongDiff = SongDiff.Normal;

                Type.text = "NORMAL";
                currentColor = Color.blue;
                Type.color = Color.blue;
            }
            else if(CurrentDifficultyMode == 2)
            {
            
                if (!_songData.DifficultiesLocks[2])
                {
                    Type.text = "LOCKED";
                    currentColor = Color.red;
                    Type.color = Color.red;
                    return;
                }
                _currentSongDiff = SongDiff.Hard;

                Type.text = "HARD";
                currentColor = Color.red;
                Type.color = Color.red;
            }
        
            _score.text = _songData.ScoresExtraDiff[(int)_currentSongDiff].ToString();
            _score.color = currentColor;
        
            _songData.LoadSongData();
            CheckBackData();
        }

        protected override void buySong()
        {
            if(PlayerLevelManager.Instance.Level < _songData.LevelRequieredExtraDiff)
            {
                //show pop up cant buy
                UIManagers.Instance.OnNotEnoughLevelButton(PlayerLevelManager.Instance.Level.ToString(), _songData.LevelRequieredExtraDiff.ToString());
                return;
            }
        
            Consumables.DiamondManager.TryToBuy(_songData.UnlockPriceExtraDiff, (canBuy) =>
            {
                if (!canBuy)
                {
                    //show pop up cant buy
                    UIManagers.Instance.OnNotEnoughDiamondsButton(Consumables.DiamondManager.Diamonds.ToString(), _songData.UnlockPriceExtraDiff.ToString());
                    return;
                }
            
                _songData.UnlockExtraDiff();
            
            });
        
            base.buySong();
        }

       
    }
