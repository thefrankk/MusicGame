using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance;
    
    //Audio and clips
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _songClips;


    public enum SongClips
    {
        SCREENPLAY_SONG,
        PARASITE_SONG,
        BOPEBOO_SONG,
        
    }
   
  
    private SongClips _currentSongClip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public void SetClip(SongClips clip)
    {
        _currentSongClip = clip;
    }
    
    public void StartSong(float delay = 0, bool loop = false)
    {
        _audioSource.clip = _songClips[(int)_currentSongClip];


        if (delay > 0)
        {
            Timer.CreateTimer(delay, () =>
            {
                    _audioSource.Play();
            });
        }
        else
            _audioSource.Play();

        _audioSource.loop = loop;

    }

    public void StopSong()
    {
        _audioSource.Stop();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void UnPause()
    {
        _audioSource.UnPause();
    }
    
    public float GetCurrentSongLenght()
    {
        return _audioSource.clip.length;
    }

    public string GetCurrentSongName()
    {
        return ProcessInputString(_currentSongClip.ToString());
    }
    
    public string ProcessInputString(string input)
    {
        return input.Replace("_SONG", "");
    }
}