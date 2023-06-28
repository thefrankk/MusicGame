
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerLogic : MonoBehaviour
{

    [Header("Spawners")]
    [SerializeField] private Transform[] _spawners;

    [FormerlySerializedAs("_bullets")]
    [Header("Bullets")]
    [SerializeField] private FoodLogic[] _food;

    //Notes of the song
    private LinkedList<NoteData> _allNotes;
    private LinkedListNode<NoteData> _currentNote;
    
    //Object pooling
    private ObjectPooling<FoodLogic> _objectPooling;



    private int _diff;
    //Variables
    private bool _isSpawnerRunning;
    private float _currentTime = 0;
    private float _prev;
    private int _cont;

    public void StartSpawner(LinkedList<NoteData> notes, int noteDiff)
    {
        _allNotes = notes;
        _isSpawnerRunning = true;
        _diff = noteDiff;
        _currentNote = _allNotes.First;
        _objectPooling = new ObjectPooling<FoodLogic>("Candys POOL", _food[0], (int)(_allNotes.Count * 0.10));
        
    }


    private void Update()
    {
        if (!_isSpawnerRunning)
            return;

        if (_currentNote.Next != null)
        {

            CreateEnemys();

        }

    }
    
    private void CreateEnemys()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _currentNote.Value.CurrentTime && _currentNote.Next.Value.CurrentTime > _prev)
        {
            SpawnNote(_currentNote);

            _prev = _currentTime;

            _cont++;

            if (_cont >= _allNotes.Count)
            {
                _cont = 0;
                _currentTime = 0;
                _prev = 0;
                _isSpawnerRunning = false;

            }
           
            _currentNote = _currentNote.Next;
        }

    }
    
    private void SpawnNote(LinkedListNode<NoteData> noteData)
    {
       FoodLogic food = _objectPooling.GetObjectFromPool();
       FoodData foodData = new FoodData( (_spawners.Length / 2) <= noteData.Value.SpawnerIndex ? FoodLogic.Side.DOWN : FoodLogic.Side.UP, _spawners[noteData.Value.SpawnerIndex], _objectPooling, _diff);
       food.Configure(foodData);
    }

    public void StopSpawner()
    {
        _isSpawnerRunning = false;
    }

    public void ReanudeSpawner()
    {
        _isSpawnerRunning = true;
    }
    public void ResetSpawner()
    {
        Debug.Log("reset spawner");
        _objectPooling.DestroyPool();
        _isSpawnerRunning = false;
        _currentTime = 0;
        _prev = 0;
        _cont = 0;
    }
}  
