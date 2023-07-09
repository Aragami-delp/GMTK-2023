using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;
    public ItemData ItemData { get { return itemData; } set { InitPrefab(value); } }

    public void InitPrefab(ItemData data)
    {
        itemData = data;

        itemID = data.ItemID;
        itemType = data.ItemType;
        itemValue = data.ItemValue;
        changeAmount = data.ChangeAmount;
    }

    [SerializeField]
    private ItemList itemID;
    public ItemList ItemID { get { return itemID; } set { itemID = value; } }

    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } set { itemType = value; } }


    [SerializeField]
    private int itemValue;
    public int ItemValue { get { return itemValue; } set { itemValue = value; } }

    [SerializeField]
    private int changeAmount;
    public int ChangeAmount { get { return changeAmount; } set { changeAmount = value; } }

    public void LevelItem(int level) 
    {
        int levelScaled = 2 * level;
        changeAmount += levelScaled + (int) UnityEngine.Random.Range(-levelScaled * 0.25f , levelScaled * 0.25f);

        itemValue = changeAmount;

    }
}
