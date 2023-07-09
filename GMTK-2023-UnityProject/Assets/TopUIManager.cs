using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopUIManager : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider,xpSlider;
    PlayerStats playerstats;

    [SerializeField]
    private TMP_Text levelNumber;
    [SerializeField]
    private ParticleSystem bleedParticels,HealParticels,XpGainParticels;

    int oldHP;
    int oldXp;
    int toXp;
    bool updateHealth;
    bool gainedHP;
    bool updateXP;
    bool updatePercentageXP;

    private void Start()
    {
        playerstats = PlayerStats.Instance;

        // 0 to play Heal particel stuff
        UpdateHealthBar(this, 0);
        playerstats.OnLevelUp += ClearXpBar;
        playerstats.OnchangingXP += UpdateXpBar;
        playerstats.OnchangingHp += UpdateHealthBar;
        playerstats.OnAttackChange += UpdateDamageText;
    }

    private void ClearXpBar(object sender, EventArgs e)
    {
        //xp to max
        UpdateXpBar(this,oldXp);
        
        UpdateXpPercent(playerstats.XP / playerstats.XPForLevel ,1);

        toXp = playerstats.XPForLevel;

        if (updateXP) 
        {
            timeXp = 0;
        }

        StartCoroutine(XPLevelup());


    }

    private void UpdateXpBar(object sender, int oldXP)
    {
        oldXp = oldXP;
        toXp = playerstats.XP;
        updateXP = true;
        StartCoroutine(XpParticels());
    }
    private void UpdateXpPercent(int fromP,int toP) 
    {
        if (toP == fromP) 
        {
            Debug.Log("Update xp panic !!!!");
        }
        updateXP = true;
        oldXp = fromP;
        toXp = toP;

        timeXp =0;

        StartCoroutine(XpParticels());

        updatePercentageXP = true;

    }

    private void UpdateHealthBar(object sender, int oldHP)
    {
        this.oldHP = oldHP;
        if (oldHP < playerstats.HP)
        {
            gainedHP = true;
        }
        else 
        {
            gainedHP = false;
        }

        updateHealth = true;
        StartCoroutine(BloodParticels());
    }

    private void UpdateDamageText(object sender, EventArgs e)
    {
        
    }

    float timeHP = 0;
    float timeXp = 0;
    private void Update()
    {
        if (updateXP) 
        {
            timeXp += Time.deltaTime;
            
            if (updatePercentageXP)
            {
                xpSlider.value = Mathf.Lerp(oldXp, toXp, timeXp);
            }
            else 
            {
                xpSlider.value = Mathf.Lerp(oldXp, toXp, timeXp) / playerstats.XPForLevel;
            }
        }
        if (updateHealth)
        {
            timeHP += Time.deltaTime;
            if (gainedHP)
            {
                hpSlider.value = Mathf.Lerp(oldHP, playerstats.HP , timeHP) / playerstats.MaxHp;
            }
            else 
            {
                hpSlider.value = Mathf.Lerp(oldHP, playerstats.HP, timeHP) / playerstats.MaxHp;
            }
            
        }
    }

    IEnumerator BloodParticels() 
    {
        if (gainedHP)
        {
            HealParticels.Play();
        }
        else 
        {
            bleedParticels.Play();
        }
        
        yield return new WaitForSeconds(1);

        HealParticels.Stop();
        bleedParticels.Stop();


        updateHealth = false;
        timeHP = 0;
    }

    IEnumerator XpParticels() 
    {
        XpGainParticels.Play();
        yield return new WaitForSeconds(1);
        XpGainParticels.Stop();
        updateXP = false;
        updatePercentageXP = false;
        timeXp = 0;
        oldXp = playerstats.XP;
    }

    IEnumerator XPLevelup() 
    {
        yield return new WaitForSeconds(1.3f);
        // baar to 0
        UpdateXpPercent(1,0);
        timeXp = 0;
        yield return new WaitForSeconds(1.3f);
        //Xp to target
        UpdateXpBar(this,0);

        levelNumber.text = playerstats.Lvl.ToString();
    }
}
