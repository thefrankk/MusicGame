using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SpineController : MonoBehaviour
{
    public List<string> prize;
    public List<AnimationCurve> animationCurves;

    [Header("ListObjects")]
    public GameObject[] ListObj;
    public TextMeshProUGUI Reming;
    public Button SpineBtn;

    [Header("Time Controller")]
    private const string TimeKey = "PlayerTime";
    private TimeSpan remainingTime;
    private readonly TimeSpan countdownDuration = TimeSpan.FromHours(24);

    [Header("Internal Data")]
    private bool spinning;
    private float anglePerItem;
    private int randomTime;
    private int itemNumber;

    public Action<bool> OnSpinAvailable;

    void Start()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;
        LoadRemainingTime();
    }
    void Update()
    {
        if(PlayerPrefs.GetString("StartSpine") == "")
        {

        }
        else
        {
            // Decrease remaining time by deltaTime
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
            // Check if the countdown has finished
            if (remainingTime.TotalSeconds <= 0)
            {
                remainingTime = TimeSpan.Zero;
                Debug.Log("Countdown finished!");
                //SpineBtn.interactable = true;
                OnSpinAvailable?.Invoke(true);
                Reming.text = "";
                PlayerPrefs.SetString("StartSpine", "");
            }
            else
            {
                // Display formatted time
                string formattedTime = FormatTime(remainingTime);
                Reming.text = "FREE IN " + formattedTime;
                //SpineBtn.interactable = false;
            }

            // Save remaining time to PlayerPrefs
            SaveRemainingTime();
        }
    }
    public void Spin()
    {
        if(PlayerPrefs.GetString("StartSpine") == "")
        {
            foreach (GameObject obj in ListObj)
            {
                obj.SetActive(false);
            }
            randomTime = UnityEngine.Random.Range(4, 9);
            itemNumber = UnityEngine.Random.Range(0, prize.Count);
            float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
            StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
            LoadRemainingTime();
            PlayerPrefs.SetString("StartSpine", "Done");
        }
    }
    public void RewaredSpin()
    {
        //Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
        void CompleteMethod(bool completed, string advertiser)
        {
            Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
            if (completed == true)
            {
                foreach (GameObject obj in ListObj)
                {
                    obj.SetActive(false);
                }
                randomTime = UnityEngine.Random.Range(4, 9);
                itemNumber = UnityEngine.Random.Range(0, prize.Count);
                float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
                StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
                LoadRemainingTime();
            }
            else
            {
                //no reward
            }
        }
    }
    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;

        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = UnityEngine.Random.Range(0, animationCurves.Count);
        Debug.Log("Animation Curve No. : " + animationCurveNumber);
        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime * 5;
            yield return 0;
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;
        foreach (GameObject obj in ListObj)
        {
            if (obj.name == prize[itemNumber])
            {
                obj.SetActive(true);
                if(prize[itemNumber] == "200")
                {
                    PlayerPrefs.SetInt("COINS", PlayerPrefs.GetInt("COINS") + 200);
                }
                if (prize[itemNumber] == "5")
                {
                    PlayerPrefs.SetInt("HEART", PlayerPrefs.GetInt("HEART") + 5);
                }
                if (prize[itemNumber] == "No Ads")
                {

                }
                if (prize[itemNumber] == "1000")
                {
                    PlayerPrefs.SetInt("COINS", PlayerPrefs.GetInt("COINS") + 1000);
                }
                if (prize[itemNumber] == "600")
                {
                    PlayerPrefs.SetInt("COINS", PlayerPrefs.GetInt("COINS") + 600);
                }
                if (prize[itemNumber] == "New Skin")
                {
                    /*int CurrentSkin = UnityEngine.Random.Range(0, ShopController.ListCharacters.Length);
                    int RandomChar = UnityEngine.Random.Range(0, 3);
                    if(RandomChar == 0)
                    {
                        SongItem ItemOne = ShopController.ListCharacters[CurrentSkin].ItemOne;
                        PlayerPrefs.SetString("CurrentChar" + ItemOne.Character.skeletonDataAsset.name, "Done");
                    }
                    if(RandomChar == 1)
                    {
                        SongItem ItemTwo = ShopController.ListCharacters[CurrentSkin].ItemTwo;
                        PlayerPrefs.SetString("CurrentChar" + ItemTwo.Character.skeletonDataAsset.name, "Done");
                    }
                    if(RandomChar == 2)
                    {
                        SongItem ItemThree = ShopController.ListCharacters[CurrentSkin].ItemThree;
                        PlayerPrefs.SetString("CurrentChar" + ItemThree.Character.skeletonDataAsset.name, "Done");
                    }*/
                }
            }
        }

    }

    private string FormatTime(TimeSpan time)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

    private void LoadRemainingTime()
    {
        if (PlayerPrefs.HasKey(TimeKey))
        {
            string savedTime = PlayerPrefs.GetString(TimeKey);
            remainingTime = TimeSpan.Parse(savedTime);
        }
        else
        {
            remainingTime = countdownDuration;
        }
    }

    private void SaveRemainingTime()
    {
        string currentTime = remainingTime.ToString();
        PlayerPrefs.SetString(TimeKey, currentTime);
        PlayerPrefs.Save();
    }
}
