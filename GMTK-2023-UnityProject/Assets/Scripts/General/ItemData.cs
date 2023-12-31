using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Item", menuName ="Create New Item")]
public class ItemData: ScriptableObject
{
    [SerializeField]
    private ItemList itemID;
    public ItemList ItemID { get { return itemID; } set { itemID = value; } }
    
    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } set { itemType = value; } }


    [SerializeField]
    private int itemValue;
    public int ItemValue { get; set; }

    [SerializeField]
    private int changeAmount;
    public int ChangeAmount { get; set; }

}



public enum ItemList 
{ 
    Nothing,
    HealingPotion,
    Sword,
    Speer,
    Armor
}

public enum ItemType 
{
    Nothing,
    Potion,
    Weapon,
    Armor
}