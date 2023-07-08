using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUIManager : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;
    PlayerStats playerstats;

    private void Start()
    {
        playerstats = PlayerStats.Instance;

        UpdateHealthBar(this, EventArgs.Empty);

        playerstats.OnchangingHp += UpdateHealthBar;
        playerstats.OnAttackChange += UpdateDamageText;
    }

    private void UpdateHealthBar(object sender, EventArgs e)
    {
        hpSlider.value = playerstats.HP / (float) playerstats.MaxHp;
    }

    private void UpdateDamageText(object sender, EventArgs e)
    {
        
    }
}
