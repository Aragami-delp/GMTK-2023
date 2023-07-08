using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using System.Linq;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance = null;
    [SerializeField] private TileButton m_mapTilePrefab;
    private List<Sprite> m_prevTileInWorldList = new List<Sprite>();
    [SerializeField] private TileInWorld m_currentTile;
    [SerializeField] private int m_prevTilesDisplayed = 3;
    [SerializeField] private RectTransform m_buttonsHolder;
    [SerializeField] private Image m_currentWorldTileImage;

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

    private void Start()
    {
        AddNewRandomTileButton(BIOM.WOODS);
    }

    public void AddNewRandomTileButton(BIOM _prevBiom)
    {
        List<MapTileSO> matchingTiles = m_mapTileSOs.Where(o => o.PrevBiom == _prevBiom).ToList();
        TileButton newTileButton = Instantiate(m_mapTilePrefab).Init(matchingTiles[Random.Range(0, matchingTiles.Count)]);
        newTileButton.transform.SetParent(m_buttonsHolder, false);
    }

    public void AddTileToWorld(MapTileSO m_so)
    {
        CycleTiles(m_currentTile);
        Instantiate(m_mapTilePrefab).Init(m_so);
    }

    public void GenerateNewTileChoices()
    {

    }

    private void CycleTiles(TileInWorld _newOldTile)
    {
        List<Sprite> newOrder = new List<Sprite> { _newOldTile.WorldSprite };
        for (int i = 1; i < m_prevTilesDisplayed; i++)
        {
            newOrder.Add(m_prevTileInWorldList[i]);
        }
        m_prevTileInWorldList = newOrder;
        //TODO: Update visible images
    }
}
