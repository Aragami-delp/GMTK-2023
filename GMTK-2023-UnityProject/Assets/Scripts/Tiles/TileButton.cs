using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileButton : MonoBehaviour
{
    private MapTileSO m_so;
    [SerializeField] private Image m_previewImage;
    [SerializeField] private TMP_Text m_title;
    [SerializeField] private TMP_Text m_description;

    public TileButton Init(MapTileSO _so)
    {
        m_so = _so;
        m_previewImage.sprite = m_so.Preview;
        m_title.SetText(m_so.Title);
        m_description.SetText(m_so.Description);
        return this;
    }

    public BIOM PrevBiom => m_so.PrevBiom;
    public BIOM NextBiom => m_so.NextBiom;
    public string Name => m_so.Title;
    public string Description => m_so.Description;
    public Sprite Image => m_so.Preview;

    private void OnButtonPressed()
    {
        TileManager.Instance.AddTileToWorld(this.m_so);
        Destroy(this.gameObject);
    }
}
