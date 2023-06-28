
public class ScoreManager : ConsumableManager
{
    
    public ScoreManager()
    {
        Instance = this;
        //_asset = PlayerPrefs.GetInt("SCORE", 0);
    }
    
    //Properties
    public int Score => _asset;

    private int _scoreTemporal;
    
    public override void Add(int amount)
    {
        _asset += amount;
    }

    public override void Substract(int amount)
    {
       _asset -= amount;
    }

    public void Reset()
    {
        _asset = 0;
    }


   
}
