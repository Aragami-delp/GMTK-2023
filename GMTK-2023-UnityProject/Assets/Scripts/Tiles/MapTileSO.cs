using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewTileData", menuName = "Tiles/New Tile")]
public class MapTileSO : ScriptableObject
{
    [SerializeField] public BIOM PrevBiom;
    [SerializeField] public BIOM NextBiom;
    [SerializeField] public string Title;
    [SerializeField, TextArea] public string Description;
    [SerializeField] public Sprite Foreground;
    [SerializeField] public Sprite Background;

    [SerializeField] public EVENTTYPE Event;
    [SerializeField] public InteractionSO[] interactions;
    
    public String getTitle(int chosenInteraction)
    {
        string txt = Title;
        if (chosenInteraction != -1)
        {
            txt = interactions[chosenInteraction].Title;
        }
        return txt;
    }

    public String getDescription(int chosenInteraction)
    {
        string txt = Description;
        if (chosenInteraction != -1)
        {
            txt = interactions[chosenInteraction].Description;
        }
        return txt;
    }
}
