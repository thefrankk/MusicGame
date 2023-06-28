
using TMPro;
using UnityEngine;

public class NotEnoughDiamondsPopUp : UIPopUp, IInformativePopUp
{

    [SerializeField] private TextMeshProUGUI _currentDiamonds;
    [SerializeField] private TextMeshProUGUI _diamondsNeeded;
    protected override void addListeners()
    {
        base.addListeners();
    }


    public void SetText(params string[] data)
    {
        _currentDiamonds.text = $"You have: <color=red>{data[0]}</color> Diamonds";
        _diamondsNeeded.text = $"You need: <color=yellow>{data[1]}</color> Diamonds to unlock it";
    }
}
