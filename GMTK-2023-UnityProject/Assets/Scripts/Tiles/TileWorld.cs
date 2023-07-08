using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileWorld : MonoBehaviour
{
    private MapTileSO m_so;
    [SerializeField] private SpriteRenderer m_foreground;
    [SerializeField] private SpriteRenderer m_background;

    public TileWorld InitEmptyTile(Sprite _foreground, Sprite _background)
    {
        m_foreground.sprite = _foreground;
        m_background.sprite = _background;
        return this;
    }

    public TileWorld SetTile(MapTileSO _so)
    {
        m_so = _so;
        m_foreground.sprite = _so.Foreground;
        m_background.sprite = _so.Background;
        return this;
    }

    public BIOM NextBiom
    {
        get
        {
            if (m_so != null && m_so.NextBiom != BIOM.NONE)
                return m_so.NextBiom;
            return BIOM.WOODS;
        }
    }

    public BIOM CurrentBiom
    {
        get
        {
            if (m_so != null)
                return m_so.PrevBiom;
            return BIOM.NONE;
        }
    }
}
