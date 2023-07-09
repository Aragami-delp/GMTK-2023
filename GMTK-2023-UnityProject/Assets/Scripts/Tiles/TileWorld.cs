using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileWorld : MonoBehaviour
{
    private MapTileSO m_so;
    private int m_chosenInteraction;
    [SerializeField] private SpriteRenderer m_foreground;
    [SerializeField] private SpriteRenderer m_background;
    [SerializeField] private Collider2D m_tileCollider;

    public TileWorld InitEmptyTile(Sprite _foreground, Sprite _background)
    {
        m_foreground.sprite = _foreground;
        m_background.sprite = _background;
        m_tileCollider.enabled = false;
        return this;
    }

    public TileWorld SetTile(MapTileSO _so, int _chosenInteraction)
    {
        m_so = _so;
        m_foreground.sprite = _so.Foreground;
        m_background.sprite = _so.Background;
        m_tileCollider.enabled = true;
        m_chosenInteraction = _chosenInteraction;
        
        return this;
    }

    public TileWorld SetOldTile()
    {
        m_tileCollider.enabled = false;
        return this;
    }

    public EVENTTYPE GetEvent => m_so.Event;

    public BIOM NextBiom
    {
        get
        {
            if (m_so != null && m_so.NextBiom != BIOM.NONE)
                return m_so.NextBiom;
            return BIOM.TAVERN;
        }
    }

    public BIOM CurrentBiom
    {
        get
        {
            if (m_so != null)
                return m_so.PrevBiom;
            return BIOM.TAVERN;
        }
    }

    public MapTileSO GetTileSo()
    {
        return m_so;
    }

    public int getChosenInteraction()
    {
        return m_chosenInteraction;
    }
}
