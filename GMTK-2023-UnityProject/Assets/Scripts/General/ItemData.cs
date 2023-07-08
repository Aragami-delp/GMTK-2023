using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Item", menuName ="Create New Item")]
public class ItemData: ScriptableObject
{
    [SerializeField]
    private ItemList itemID;
    public ItemList ItemID { get; set; }
    
    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType { get; set; }


    [SerializeField]
    private int itemValue;
    public int ItemValue { get; set; }

    [SerializeField]
    private int changeAmount;
    public int ChangeAmount { get; set; }

}



public enum ItemList 
{ 
    HealingPotion,
    Sword,
    Speer,
    Armor
}

public enum ItemType 
{ 
    Potion,
    Weapon,
    Armor
}