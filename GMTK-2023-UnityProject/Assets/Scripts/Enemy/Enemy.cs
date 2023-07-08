using System.Collections;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int lvl;
    public int Lvl { get { return lvl; } set { lvl = value; } }
    [SerializeField]
    private int baseHealth,healthScaleFactor;

    [SerializeField]
    private int health;

    public int Health { get { return health; } set { SetHealth(value); } }

    [SerializeField]
    private int attack,baseAttack,dmgScaleFactor;

    public int Attack { get { return attack; } set { attack = value; } }

    void Start()
    {
        lvl = (int)(TileManager.Instance.PassedTiles * 1.5f / 10) + PlayerStats.Instance.Lvl + UnityEngine.Random.Range(-5, 5);

        health = baseHealth + lvl * healthScaleFactor;

        Attack = baseAttack + lvl * dmgScaleFactor;

    }


    public void DamageEnemy(int dmgAmount) 
    {
        health -= dmgAmount;
    }

    private void SetHealth(int healthAmount) 
    {
        health = healthAmount;
    }
}
