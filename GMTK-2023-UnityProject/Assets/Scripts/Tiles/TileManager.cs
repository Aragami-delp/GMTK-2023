using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using System.Linq;
using UnityEngine.UI;

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
    }

    #region TileButton
    [SerializeField] private int m_buttonCount = 3;
    [SerializeField] private int m_minTilesInSameBiom = 4;
    [SerializeField] private RectTransform m_buttonParent;

    [Header("Pooling"), SerializeField] private TileButton m_tileButtonPrefab;
    private List<TileButton> m_poolTileButton = new List<TileButton>();
    private List<TileButton> m_currentlyActiveButtons = new List<TileButton>();
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

    public void OnButtonNewTilePressed(MapTileSO _mapTileSO)
    {
        PassedTiles++;
        Debug.Log("Clicked on button: " + _mapTileSO.Title);
        RemoveCurrentButtons();
    }

    [ContextMenu("GenerateButtons"), System.Obsolete("Only use for inspector calls", false)]
    private void InspectorCreateNewButtons()
    {
        CreateNewButtons(BIOM.WOODS);
    }
    private void CreateNewButtons(BIOM _prevBiom, bool _biomChangeAllowed = true)
    {
        List<MapTileSO> newPossibleTiles = m_mapTileScriptables.Where(o => o.PrevBiom == _prevBiom && (m_minTilesInSameBiom > 0 ? o.NextBiom == _prevBiom : true)).ToList();
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

    [SerializeField] private int m_visiblePrevTilesCount = 10;
    //private List

    //private void 

    #endregion
}
