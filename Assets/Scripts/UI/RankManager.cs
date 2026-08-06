using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI TimesHit;
    public TextMeshProUGUI GoldText;
    // Start is called before the first frame update
    void Start()
    {
        TimesHit.text = "HP LEFT: " + PlayerPrefs.GetFloat("HP", 0);
        GoldText.text = "Gold: " + PlayerPrefs.GetFloat("Gold", 0);
        GoldText.text = "Time: " + PlayerPrefs.GetFloat("Seconds", 0);
        RankText.text = "Rank: ";
    }
}
