using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    public int PassedTiles { get; private set; } = 0;


    [SerializeField, InspectorName("Map Tile SOs")] private List<MapTileSO> m_mapTileScriptables;

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

        AwakeWorldTile();
        CreateNewButtons();
    }

    [Header("Pooling"), SerializeField] private TileButton m_tileButtonPrefab;
    private List<TileButton> m_poolTileButton = new List<TileButton>();
    private List<TileButton> m_currentlyActiveButtons = new List<TileButton>();
    [SerializeField] private TileWorld m_tileWorldPrefab;
    private List<TileWorld> m_poolTileWorld = new List<TileWorld>();

    #region TileButton
    [Header("Button"), SerializeField] private int m_buttonCount = 3;
    [SerializeField] private int m_minTilesInSameBiom = 4;
    [SerializeField] private RectTransform m_buttonParent;

    private TileButton GetNewTileButton()
    {
        for (int i = 0; i < m_poolTileButton.Count; i++)
        {
            if (!m_poolTileButton[i].gameObject.activeInHierarchy)
            {
                return m_poolTileButton[i];
            }
        }

        TileButton newTileButton = Instantiate(m_tileButtonPrefab);
        newTileButton.transform.SetParent(this.transform, false);
        newTileButton.gameObject.SetActive(false);
        m_poolTileButton.Add(newTileButton);
        return newTileButton;
    }

    public void OnButtonNewTilePressed(MapTileSO _so)
    {
        PassedTiles++;
        PlaceNextFullTile(_so);
        RemoveCurrentButtons();
    }

    [ContextMenu("GenerateButtons")]
    public void CreateNewButtons()
    {
        BIOM prevBiom = GetCurrentTile().NextBiom;
        List<MapTileSO> newPossibleTiles = m_mapTileScriptables.Where(o => o.PrevBiom == prevBiom && (m_minTilesInSameBiom > 0 ? o.NextBiom == prevBiom : true)).ToList();
        m_minTilesInSameBiom--;
        for (int i = 0; i < m_buttonCount; i++)
        {
            MapTileSO newSO = newPossibleTiles[Random.Range(0, newPossibleTiles.Count)];
            newPossibleTiles.Remove(newSO);
            TileButton newButton = GetNewTileButton();
            if (newButton != null)
            {
                newButton.gameObject.SetActive(true);
                newButton.Init(newSO);
                newButton.transform.SetParent(m_buttonParent, false);
                m_currentlyActiveButtons.Add(newButton);
            }
        }
    }

    private void RemoveCurrentButtons()
    {
        foreach (TileButton oldButton in m_currentlyActiveButtons)
        {
            oldButton.gameObject.SetActive(false);
            oldButton.transform.SetParent(this.transform, false);
        }
    }
    #endregion

    #region PlaceTiles

    [Header("TileWorld"), SerializeField] private int m_visiblePrevTilesCount = 5;
    [SerializeField] private int m_visibleNextTilesCount = 5;
    [SerializeField] private Sprite m_emptyTileForeground;
    [SerializeField] private Sprite m_emptyTileBackground;
    [SerializeField] private Transform m_tileWorldParent;
    [SerializeField] private float m_newTileDistance = 3;
    private int GetActiveTilePosition => m_visiblePrevTilesCount;
    private int GetActiveTotalTiles => GetActiveTilePosition + m_visibleNextTilesCount;

    private LinkedList<TileWorld> m_activeTiles = new LinkedList<TileWorld>();

    private TileWorld GetNewTileWorld()
    {
        for (int i = 0; i < m_poolTileWorld.Count; i++)
        {
            if (!m_poolTileWorld[i].gameObject.activeInHierarchy)
            {
                return m_poolTileWorld[i];
            }
        }

        TileWorld newTileWorld = Instantiate(m_tileWorldPrefab);
        newTileWorld.transform.SetParent(this.transform, false);
        newTileWorld.gameObject.SetActive(false);
        m_poolTileWorld.Add(newTileWorld);
        return newTileWorld;
    }

    private int m_drawnTiles;

    private void PlaceNewEmptyTile()
    {
        TileWorld newEmptyTile = GetNewTileWorld();
        foreach (TileWorld tileWorld in m_activeTiles)
        {
            tileWorld.transform.position = new Vector3(tileWorld.transform.position.x, tileWorld.transform.position.y, tileWorld.transform.position.z - 0.01f); // Player moves, not the world
            tileWorld.SetOldTile();
        }
        if (newEmptyTile != null)
        {
            newEmptyTile.gameObject.SetActive(true);
            newEmptyTile.InitEmptyTile(m_emptyTileForeground, m_emptyTileBackground);
            newEmptyTile.transform.SetParent(m_tileWorldParent, false);
            newEmptyTile.transform.position = m_tileWorldParent.position - new Vector3(m_newTileDistance * m_drawnTiles++ - (m_visiblePrevTilesCount * m_newTileDistance), 0, 0);
            m_activeTiles.AddFirst(newEmptyTile);
        }

        if (m_activeTiles.Count > GetActiveTotalTiles)
        {
            TileWorld x = m_activeTiles.Last();
            x.gameObject.SetActive(false);
            m_activeTiles.RemoveLast();
        }
    }

    private void PlaceNextFullTile(MapTileSO _so)
    {
        PlaceNewEmptyTile();
        m_activeTiles.ElementAt(GetActiveTilePosition).SetTile(_so);
        //TODO: Start
    }

    private void AwakeWorldTile()
    {
        for (int i = 0; i < GetActiveTotalTiles; i++)
        {
            PlaceNewEmptyTile();
        }
    }

    public TileWorld GetCurrentTile()
    {
        return m_activeTiles.ElementAt(GetActiveTilePosition);
    }
    #endregion

    #region Debug
#if UNITY_EDITOR
    [ContextMenu("Print current Biom"), System.Obsolete("Only use for inspector calls", false)]
    public void PrintCurrentBiom()
    {
        Debug.Log(GetCurrentTile().CurrentBiom.ToString());
    }
#endif
    #endregion

    public void StartNextTileTurn()
    {
        CreateNewButtons();
    }
}
