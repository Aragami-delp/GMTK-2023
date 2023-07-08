using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> itemList = new List<ItemData>();

    [SerializeField]
    private GameObject blankItemPrefab;

    public static ItemManager Instance { get; private set; }
    private void Awake()
    {
        #region Singleton
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        #endregion
    }

    public GameObject CreateItem(ItemList item) 
    {
        for (int i = 0; i < itemList.Count; i++)
        {

            if (itemList[i].ItemID == item) 
            {
                GameObject newItem = GameObject.Instantiate(blankItemPrefab);
                newItem.GetComponent<Item>().ItemData = itemList[i];
                return newItem;
            }
        }
        Debug.LogWarning("Something went wrong on ItemCreation");
        return null;
    }

    public GameObject CreateItem(ItemData item) 
    {
        GameObject newItem = GameObject.Instantiate(blankItemPrefab);
        newItem.GetComponent<Item>().ItemData = item;
        return newItem;
    }

}
