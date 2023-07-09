using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUIManager : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider,xpSlider;
    PlayerStats playerstats;

    [SerializeField]
    private ParticleSystem bleedParticels,HealParticels,XpGainParticels;

    int oldHP;
    int oldXp;
    bool updateHealth;
    bool gainedHP;
    bool updateXP;

    private void Start()
    {
        playerstats = PlayerStats.Instance;

        // 0 to play Heal particel stuff
        UpdateHealthBar(this, 0);
        playerstats.OnchangingXP += UpdateXpBar;
        playerstats.OnchangingHp += UpdateHealthBar;
        playerstats.OnAttackChange += UpdateDamageText;
    }

    private void UpdateXpBar(object sender, int oldXP)
    {
        oldXp = oldXP;
        updateXP = true;
        StartCoroutine(XpParticels());
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

            xpSlider.value = Mathf.Lerp(oldXp, playerstats.XP, timeXp) / playerstats.XPForLevel;
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
        timeXp = 0;
    }
}
