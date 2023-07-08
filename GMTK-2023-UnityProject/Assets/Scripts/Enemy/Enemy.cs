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
    private int baseHealth,healthScaleFactor, maxHealth;

    [SerializeField]
    private int health;

    public int Health { get { return health; } set { SetHealth(value); } }

    [SerializeField]
    private int attack,baseAttack,dmgScaleFactor,maxDmg;

    public int Attack { get { return attack; } set { attack = value; } }

    void Awake()
    {
        lvl = (int)(TileManager.Instance.PassedTiles * 1.5f / 10) + PlayerStats.Instance.Lvl + UnityEngine.Random.Range(-5, 5);

        lvl =  Math.Clamp(lvl, 3 , int.MaxValue);

        health = baseHealth + lvl * healthScaleFactor;

        Attack = baseAttack + lvl * dmgScaleFactor;
        maxDmg = Attack;
        maxHealth = health;
    }

    public void DamageEnemy(int dmgAmount) 
    {
        Health -= dmgAmount;

        Attack =  (int) (maxDmg *  (float) damageFalloff.Evaluate((float) health / maxHealth));

    }

    private void SetHealth(int healthAmount) 
    {
        health = healthAmount;

        if (health <= 0) 
        {
            health = 0;
            Debug.Log("EnemyDed!!!!!!!!");
        }
    }
}
