using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Tiles/New Tile")]
public class MapTileSO : ScriptableObject
{
    [SerializeField] public BIOM PrevBiom;
    [SerializeField] public BIOM NextBiom;
    [SerializeField] public string Name;
    [SerializeField, TextArea] public string Description;
    [SerializeField] public Sprite Image;
}
