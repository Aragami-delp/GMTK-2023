using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileButton : MonoBehaviour
{
    private MapTileSO m_so;
    [SerializeField] private Image m_foreground;
    [SerializeField] private Image m_background;
    [SerializeField] private TMP_Text m_title;
    [SerializeField] private TMP_Text m_description;

    public TileButton Init(MapTileSO _so)
    {
        m_so = _so;
        m_foreground.sprite = m_so.Foreground;
        m_background.sprite = m_so.Background;
        m_title.SetText(m_so.Title);
        m_description.SetText(m_so.Description);
        return this;
    }

    public void OnButtonPressed()
    {
        TileManager.Instance.OnButtonNewTilePressed(this.m_so);
        this.gameObject.SetActive(false);
    }
}
