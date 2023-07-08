using System;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPair
    {
        [SerializeField] public BIOM EnemyType;
        [SerializeField] public Enemy EnemyPrefab;
    }

    private List<Enemy> GetPossibleEnemyPrefabs(BIOM _biom)
    {
        List<Enemy> retPrefabs = new List<Enemy>();
        foreach (EnemyPair pair in m_enemyPairList)
        {
            if (pair.EnemyType == _biom)
                retPrefabs.Add(pair.EnemyPrefab);
        }
        return retPrefabs;
    }

    [SerializeField]
    private List<ItemData> RewardItems = new List<ItemData>();
    [SerializeField] private List<EnemyPair> m_enemyPairList = new List<EnemyPair>();

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


    public void StartFight(BIOM _enemyBiom)
    {
        List<Enemy> enemyPrefabs = GetPossibleEnemyPrefabs(_enemyBiom);
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogError("No enemy to spawn, ending fight");
            EndFight();
        }

        Enemy enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)]);
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
        else
        {
            GameManager.Instance.EndGame();
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

        EndFight();
    }

    private void EndFight()
    {
        TileEventManager.Instance.EndEvent();
    }
}
