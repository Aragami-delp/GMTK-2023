using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInWorld : MonoBehaviour
{
    private MapTileSO m_so;
    [SerializeField] private Image m_foreground;
    [SerializeField] private Image m_background;

    public TileInWorld Init(MapTileSO _so)
    {
        m_so = _so;
        m_foreground.sprite = m_so.Foreground;
        m_background.sprite = m_so.Background;
        return this;
    }
}
