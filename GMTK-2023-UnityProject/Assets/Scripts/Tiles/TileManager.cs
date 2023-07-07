using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using System.Linq;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance = null;
    [SerializeField] private TileButton m_mapTilePrefab;
    private List<TileInWorld> m_prevTileInWorldList = new List<TileInWorld>();
    private TileInWorld m_currentTile;
    [SerializeField] private int m_prevTilesDisplayed = 3;

    [SerializeField, InspectorName("SO Map Tiles")] private List<MapTileSO> m_mapTileSOs = new List<MapTileSO>();

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

    public TileButton GetNewRandomMapTile(BIOM _prevBiom)
    {
        List<MapTileSO> matchingTiles = m_mapTileSOs.Where(o => o.PrevBiom == _prevBiom).ToList();
        return Instantiate(m_mapTilePrefab).Init(matchingTiles[Random.Range(0, matchingTiles.Count)]);
    }

    public void AddTileToWorld(MapTileSO m_so)
    {
        CycleTiles(m_currentTile);

    }

    private void CycleTiles(TileInWorld _newOldTile)
    {
        List<TileInWorld> newOrder = new List<TileInWorld> { _newOldTile };
        for (int i = 1; i < m_prevTilesDisplayed; i++)
        {
            newOrder.Add(m_prevTileInWorldList[i]);
        }
        //TODO: Update visible images
    }
}
