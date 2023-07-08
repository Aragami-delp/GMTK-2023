using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
