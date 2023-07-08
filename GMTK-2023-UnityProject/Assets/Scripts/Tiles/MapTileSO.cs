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
    
    private int chosenInteraction = -1;

    public int getChosenInteraction()
    {
        if (chosenInteraction == -1 && Event == EVENTTYPE.INTERACTION && interactions.Length != 0)
        {
            chosenInteraction = UnityEngine.Random.Range(0, interactions.Length);
        }

        return chosenInteraction;
    }

    public String getTitle()
    {
        return getChosenInteraction() == -1 ? Title : interactions[chosenInteraction].Title;
    }

    public String getDescription()
    {
        return getChosenInteraction() == -1 ? Description : interactions[chosenInteraction].Description;
    }
}
