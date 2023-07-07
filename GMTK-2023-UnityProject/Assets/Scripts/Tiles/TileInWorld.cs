using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileInWorld : MonoBehaviour
{
    private MapTileSO m_so;
    [SerializeField] private Image m_previewImage;

    public TileInWorld Init(MapTileSO _so)
    {
        m_so = _so;
        m_previewImage.sprite = m_so.Preview;
        return this;
    }
}
