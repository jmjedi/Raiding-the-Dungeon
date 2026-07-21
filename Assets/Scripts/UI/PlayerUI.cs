using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HPTEXT;
    [SerializeField] private TextMeshProUGUI GOLDTEXT;

    private void Awake()
    {
        
    }
    public void ChangeHP(float Value)
    {
        HPTEXT.text = Value.ToString();
    }

    public void ChangeGold(float Value)
    {
        GOLDTEXT.text = Value.ToString();
    }
}
