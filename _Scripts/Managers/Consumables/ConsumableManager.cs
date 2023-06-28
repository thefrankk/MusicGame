
using System;

public abstract class ConsumableManager
{

    public static ConsumableManager Instance;

    protected int _asset;
    public static Action OnAssetChanged;

    public abstract void Add(int amount);


    public abstract void Substract(int amount);
    
    
    
}
