
using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteRecordingLogic : MonoBehaviour
{
    //Input handler
    RecordInputHandler _recordInputHandler;
    
    //Note prefab representation
    [SerializeField] private FoodLogic _notePrefab;
    [SerializeField] private Transform[] _spawners;
    
    //All note data
    private LinkedList<NoteData> _allNotes = new LinkedList<NoteData>();
    private LinkedListNode<NoteData> _currentNode;
    private int _notesAmount;


    //Object pooling
    private ObjectPooling<FoodLogic> _objectPooling;
    
    //Properties
    public LinkedList<NoteData> AllNotes => _allNotes;

    private int _diff;


    public static event Action<int> OnNoteCreated;
    
    
    private void Awake()
    {
        _recordInputHandler = FindObjectOfType<RecordInputHandler>();
    }

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        _recordInputHandler.OnButtonPressed += SetNote;
        _diff = GameManager.Instance.GetCurrentDiff();
        
        _objectPooling = new ObjectPooling<FoodLogic>("Candys POOL", _notePrefab, 40 * (1 + _diff));
        
    }

    private void OnDisable()
    {
        _recordInputHandler.OnButtonPressed -= SetNote;

    }

    private void SetNote(int spawnerIndex)
    {
        if (!SongRecorderController.IsRecording)
            return;
      

        _notesAmount++;
        
        _allNotes.AddLast(new NoteData(SongRecorderController.CurrentSongRunningTime, spawnerIndex));
        createNotePrefab(spawnerIndex);
        
        OnNoteCreated?.Invoke(_notesAmount);
        
    }

    public void DeleteRecordings()
    {
        _allNotes.Clear();
        _notesAmount = 0;
        _objectPooling.DestroyPool();
    }

    private void createNotePrefab(int spawnerIndex)
    {
        FoodLogic food = _objectPooling.GetObjectFromPool();
        FoodData foodData = new FoodData( (_spawners.Length / 2) <= spawnerIndex ? FoodLogic.Side.DOWN : FoodLogic.Side.UP, _spawners[spawnerIndex], _objectPooling, _diff);
        food.Configure(foodData);
    }

    
    
   
}
