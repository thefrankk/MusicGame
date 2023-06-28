using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteData 
{
    private float _currentTime;
    private int _spawnerIndex;
    public float CurrentTime => _currentTime;
    public int SpawnerIndex => _spawnerIndex;
    public NoteData(float currentTime, int spawnerIndex)
    {
        _currentTime = currentTime;
        _spawnerIndex = spawnerIndex;
    }
    
 
    
}
