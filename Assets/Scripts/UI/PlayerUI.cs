using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HPTEXT;
    [SerializeField] private TextMeshProUGUI GOLDTEXT;
    private PlayerController plrController;

    private void Awake()
    {
        plrController = Object.FindAnyObjectByType<PlayerController>();
    }
    public void ChangeHP(float Value)
    {
        HPTEXT.text = Value.ToString();
    }

    public void ChangeGold(float Value)
    {
        plrController.Gold += Value;
        GOLDTEXT.text = plrController.Gold.ToString();
    }
}
