using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI TimesHit;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI GoldText;

    public float MaxGold = 74;
    private string Rank;

    private void CalculateScore()
    {
        float goldAmount = PlayerPrefs.GetFloat("Gold", 0);
        float seconds = PlayerPrefs.GetFloat("Seconds", 0);
        float minuets = PlayerPrefs.GetFloat("Minuets", 0);

        float percent = (goldAmount / MaxGold) * 100;

        if (minuets <= 0)
        {
            if (goldAmount >= 100 && seconds < 30 && PlayerPrefs.GetFloat("HP", 0) == 100)
                Rank = "P";
            else if (goldAmount >= 85 && seconds < 30)
                Rank = "S";
            else if (goldAmount >= 70 && seconds <= 35)
                Rank = "A";
            else if (goldAmount >= 55 && seconds <= 40)
                Rank = "B";
            else if (goldAmount >= 41 && seconds <= 45)
                Rank = "C";
            else if (goldAmount >= 40 && seconds <= 46)
                Rank = "D";
            else
                Rank = "F";
        }
        else
            Rank = "F";

    }

    // Start is called before the first frame update
    void Start()
    {
        CalculateScore();
        TimesHit.text = "HP LEFT: " + PlayerPrefs.GetFloat("HP", 0);
        print(PlayerPrefs.GetFloat("Gold", 0));
        GoldText.text = "Gold: " + PlayerPrefs.GetFloat("Gold", 0);
        TimerText.text = "Time: " + PlayerPrefs.GetString("Time", "");
        RankText.text = "Rank: " + Rank;
    }
}
