
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    //Notes for the song
    private LinkedList<NoteData> _allNotes;
    //Difficulty of the song
    private SongView.SongDiff _difficulty;
    //Delay
    private float _delayOfSong;

    private Sprite _foodSprite;
    //Properties
    public LinkedList<NoteData> AllNotes => _allNotes;
    public SongView.SongDiff Difficulty => _difficulty;
    public float DelayOfSong => _delayOfSong;
    
    //Food Movement data
    private float _speed;
    
    
    
    
    //Constructor
    public GameData(LinkedList<NoteData> allNotes,  SongView.SongDiff difficulty, float delayOfSong)
    {
        _allNotes = allNotes;
        _difficulty = difficulty;
        _delayOfSong = delayOfSong;
    }


}
