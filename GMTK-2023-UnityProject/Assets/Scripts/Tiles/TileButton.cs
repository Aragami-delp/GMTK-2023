using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileButton : MonoBehaviour
{
    private MapTileSO m_so;
    private int m_chosenInteraction;
    [SerializeField] private Image m_foreground;
    [SerializeField] private Image m_background;
    [SerializeField] private TMP_Text m_title;
    [SerializeField] private TMP_Text m_description;

    public TileButton Init(MapTileSO _so)
    {
        m_so = _so;
        if (_so.Event == EVENTTYPE.INTERACTION && _so.interactions.Length != 0)
        {
            m_chosenInteraction = UnityEngine.Random.Range(0, _so.interactions.Length);
        }
        else
        {
            m_chosenInteraction = -1;
        }
        
        m_foreground.sprite = m_so.Foreground;
        m_background.sprite = m_so.Background;
        m_title.SetText(m_so.getTitle(m_chosenInteraction));
        m_description.SetText(m_so.getDescription(m_chosenInteraction));

        return this;
    }

    public void OnButtonPressed()
    {
        TileManager.Instance.OnButtonNewTilePressed(this.m_so, m_chosenInteraction);
        this.gameObject.SetActive(false);
        PlayerMovement.Instance.MovePlayer();
    }
}
