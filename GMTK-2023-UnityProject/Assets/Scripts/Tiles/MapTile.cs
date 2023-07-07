using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTile
{
    private MapTileSO m_so;

    public MapTile(MapTileSO _so)
    {
        m_so = _so;
    }

    public BIOM PrevBiom => m_so.PrevBiom;
    public BIOM NextBiom => m_so.NextBiom;
    public string Name => m_so.Name;
    public string Description => m_so.Description;
    public Sprite Image => m_so.Image;
}
