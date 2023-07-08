using System.Collections;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int lvl;

    [SerializeField]
    private AnimationCurve damageFalloff;
    public int Lvl { get { return lvl; } set { lvl = value; } }
    [SerializeField]
    private int baseHealth,healthScaleFactor,maxHelath;

    [SerializeField]
    private int health;

    public int Health { get { return health; } set { SetHealth(value); } }

    [SerializeField]
    private int attack,baseAttack,dmgScaleFactor,maxDmg;

    public int Attack { get { return attack; } set { attack = value; } }

    void Start()
    {
        lvl = (int)(TileManager.Instance.PassedTiles * 1.5f / 10) + PlayerStats.Instance.Lvl + UnityEngine.Random.Range(-5, 5);

        lvl =  Math.Clamp(lvl, 1, int.MaxValue);

        health = baseHealth + lvl * healthScaleFactor;

        Attack = baseAttack + lvl * dmgScaleFactor;
        maxDmg = Attack;
        maxHelath = health;
    }

public void DamageEnemy(int dmgAmount) 
    {
        health -= dmgAmount;

        Attack = (int) damageFalloff.Evaluate(health / maxHelath);
    }

    private void SetHealth(int healthAmount) 
    {
        health = healthAmount;
    }
}
