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

    [SerializeField]
    private ParticleSystem bleedParticels,HealParticels;

    int oldHP;
    bool updateHealth;
    bool gainedHP;
    private void Start()
    {
        playerstats = PlayerStats.Instance;

        // 0 to play Heal particel stuff
        UpdateHealthBar(this, 0);

        playerstats.OnchangingHp += UpdateHealthBar;
        playerstats.OnAttackChange += UpdateDamageText;
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
    float time = 0;
    private void Update()
    {
        if (updateHealth)
        {
            time += Time.deltaTime;
            if (gainedHP)
            {
                hpSlider.value = Mathf.Lerp(oldHP, playerstats.HP , time) / playerstats.MaxHp;
            }
            else 
            {
                hpSlider.value = Mathf.Lerp(oldHP, playerstats.HP, time) / playerstats.MaxHp;
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
        time = 0;
    }
}
