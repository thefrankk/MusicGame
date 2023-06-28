using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> where T : Component
{
    private Queue<T> _pool; 
    private GameObject _poolObject;

    
    public ObjectPooling(string name, T prefab, int poolSize)
    {
        CreatePool(prefab, poolSize, name);
    }
   
    private void CreatePool(T prefab, int poolSize, string name)
    {
        
        _pool = new Queue<T>();
        _poolObject = new GameObject(name);
        
        for (int i = 0; i < poolSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, _poolObject.transform);
            _pool.Enqueue(obj);
            
            obj.gameObject.SetActive(false);
        }
    }
    
    public T GetObjectFromPool()
    {
        T obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObjectToPool(T obj, float delay = 0)
    {
        if (delay == 0)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            return;
        }
        
        Timer.CreateTimer(delay, () =>
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        });

    }
    public void DestroyPool()
    {
        _pool.Clear();
        GameObject.Destroy(_poolObject.gameObject);
        
    }
    
    ~ObjectPooling()
    {
        DestroyPool();
    }
}
