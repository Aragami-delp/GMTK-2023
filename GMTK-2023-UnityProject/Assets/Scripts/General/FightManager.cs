using System;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> RewardItems = new List<ItemData>();

    public static FightManager Instance { get; private set; }
    private void Awake()
    {
        #region Singleton
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        #endregion
    }


    public void StartFight(Enemy enemy) 
    {
        PlayerStats player = PlayerStats.Instance;

        while(enemy.Health > 0 && player.HP > 0)
        {
            enemy.DamageEnemy(player.Attack);
            player.DamagePlayer(enemy.Attack);
        }

        if (player.HP > 0) 
        {
            RewardPlayer(enemy);
        }

    }

    public void RewardPlayer(Enemy enemy) 
    {
        ItemData rewardItem;

        int[] randomIndex = new int[3];

        for (int i = 0; i < randomIndex.Length; i++)
        {
            randomIndex[i] = UnityEngine.Random.Range(0, RewardItems.Count);
        }
        Array.Sort(randomIndex);
        rewardItem = RewardItems[randomIndex[0]];
        
        PlayerStats.Instance.GiveItem(ItemManager.Instance.CreateItem(rewardItem));
        PlayerStats.Instance.GainXP(100 + UnityEngine.Random.Range(0,25) * enemy.Lvl);

    }
}
