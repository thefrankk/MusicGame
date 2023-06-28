
using System;
using UnityEngine;

public abstract class FoodLogic : MonoBehaviour 
{

    //Movement data
    protected float _speedMovement;
    protected int[] _notesSpeed = new int[] { 3, 5, 8 };
    protected bool _isCandyRunning;
    protected int _currentDiff;

    private Sprite _currentSprite;
    protected SpriteRenderer _spriteRenderer;
    
    private ObjectPooling<FoodLogic> _currentPool;
    
    
    [Header("Particles")] 
    [SerializeField] protected ParticleSystem[] _effectEat;

    public enum Side
    {
        UP = -1,
        DOWN = +1,
    }

    protected Side _currentSide;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        handleMovement();
        handleDestroy();
    }

    private void handleMovement()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + ((int)_currentSide * Time.deltaTime * _speedMovement), transform.localPosition.z);
    }

    protected virtual void handleDestroy(float delay = 1.5f)
    {
        switch (_currentSide)
        {
            case Side.UP:
                if (transform.localPosition.y < -7) 
                {
                    if (_isCandyRunning)
                    {
                        DestroyObject(delay);
                    }
                }

                break;

            case Side.DOWN:
                if (transform.localPosition.y > 7)
                {
                    if (_isCandyRunning)
                    {
                        DestroyObject(delay);
                    }
                }

                break;
        }
    }

    protected virtual void DestroyObject(float delay = 0)
    {
        _isCandyRunning = false;
        _currentPool.ReturnObjectToPool(this, delay);
    }
    
   
    public void Configure(FoodData data)
    {
        _currentSide = data.Side;
        _spriteRenderer.sprite = GameManager.Instance.FoodSprite;
        transform.position = data.Pos.position;
        _currentPool = data.Pool;
        _currentDiff = data.Diff;
        
        SetSpeedMovement(data.Diff);
    }
    
   
    public void SetSpeedMovement(int diff)
    {
        _currentDiff = diff;
        _isCandyRunning = true;
        _speedMovement = _notesSpeed[diff];
    }


}

public struct FoodData
{
    public FoodLogic.Side Side;
    public Transform Pos;
    public ObjectPooling<FoodLogic> Pool;
    public int Diff;
    
    public FoodData(FoodLogic.Side side,  Transform pos, ObjectPooling<FoodLogic> pool, int diff)
    {
        Side = side;
        Pos = pos;
        Pool = pool;
        Diff = diff;
    }
}