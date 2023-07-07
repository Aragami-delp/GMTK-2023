using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance = null;

    [SerializeField, InspectorName("SO Map Tiles")] private List<MapTileSO> m_mapTileSOs = new List<MapTileSO>();
    private List<MapTile> m_tiles = new List<MapTile>();

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

        LoadTileSOs();
    }

    public MapTile GetNewRandomMapTile(BIOM _selectedBiom)
    {
        return m_tiles[Random.Range(0, m_tiles.Count)];
    }

    private void LoadTileSOs()
    {
        foreach (MapTileSO so in m_mapTileSOs)
        {
            m_tiles.Add(new MapTile(so));
        }
    }
}
