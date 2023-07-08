using System.Collections;
using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

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

    private void OnEnable()
    {
        Inventory = new GameObject[inventorySpace];

        for (int i = 0; i < Inventory.Length; i++)
        {
            Inventory[i] = ItemManager.Instance.CreateItem(ItemList.Nothing);
        }

        Attack = baseAttack;
    }

    #region Helath stuff
    [SerializeField]
    private int hp = 100;
    public int HP { get { return hp; } set { OnChangingHP(value); } }

    [SerializeField]
    private int maxHp = 100;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    public EventHandler OnchangingHp;
    public EventHandler OnDying;

    private void OnChangingHP(int newHP)
    {
        if (newHP <= 0)
        {
            hp = 0;
            Debug.Log("ded");
            OnDying?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            hp = newHP;
            OnchangingHp?.Invoke(this, EventArgs.Empty);
        }

    }

    public void HealPlayer(int healAmount)
    {
        HP += healAmount;

        HP = Math.Clamp(hp, 0, MaxHp);
    }

    public void DamagePlayer(int damageAmount)
    {
        HP -= damageAmount;
        HP = Math.Clamp(hp, 0, MaxHp);
    }

    #endregion

    #region level
    [SerializeField]
    private int lvl = 1;
    public int Lvl { get; set; }
    #endregion

    #region Xp stuff
    [SerializeField]
    private int xp;
    public int XP { get { return xp; } set { OnGainingXp(value); } }

    private void OnGainingXp(int newXp)
    {
        XP = newXp;

        if (XP >= XPForLevel)
        {
            XP -= XPForLevel;
            lvl++;
            Debug.Log("Wow Level up :D");

            XPForLevel += 200 * Lvl; 
        }
    }

    public void GainXP(int xpAmount)
    {
        xp += xpAmount;
    }


    [SerializeField]
    private int xpForLevel = 100;
    public int XPForLevel { get { return xpForLevel; } set { xpForLevel = value; } }

    #endregion

    #region attack
    [SerializeField]
    private int baseAttack = 5;
    public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }

    [SerializeField]
    private int attack;
    public int Attack { get { return attack; } set { OnChangingAttack(value); } }

    private void OnChangingAttack(int newAttack)
    {
        attack = newAttack;
        OnAttackChange?.Invoke(this,EventArgs.Empty);

    }

    public EventHandler OnAttackChange;
    #endregion

    #region inventoryStuff
    [SerializeField]
    private int inventorySpace = 4;
    public int InventorySpace { get { return inventorySpace; } set { inventorySpace = value; } }

    [SerializeField]
    private GameObject weaponHand;
    public GameObject WeaponHand { get { return WeaponHand; } set { OnWeaponHandChange(value); } }

    private void OnWeaponHandChange(GameObject newItem)
    {
        if (WeaponHand != null)
        {
            attack -= weaponHand.GetComponent<Item>().ChangeAmount;
            TryReplaceInventoryItem(weaponHand);
        }
        weaponHand = newItem;

        attack += weaponHand.GetComponent<Item>().ChangeAmount;
    }

    [SerializeField]
    private GameObject bodyItem;
    public GameObject BodyItem { get { return bodyItem; } set { OnBodyItemChange(value); } }

    private void OnBodyItemChange(GameObject newItem)
    {
        if (BodyItem != null)
        {
            if (BodyItem.GetComponent<Item>().ItemType == ItemType.Armor)
            {
                maxHp -= GetComponent<Item>().ChangeAmount;
            }

            TryReplaceInventoryItem(BodyItem);
        }
        BodyItem = newItem;

        if (BodyItem.GetComponent<Item>().ItemType == ItemType.Armor) 
        {
            maxHp += BodyItem.GetComponent<Item>().ChangeAmount;
        }
    }

    public GameObject[] Inventory { get; set; }

    #region Inventory Management
    public bool HasItem(ItemList item) 
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            Item inventoryItem = Inventory[i].GetComponent<Item>();
            if (inventoryItem.ItemID == item)
            {
                return true;
            }
        }

        return false;
    }

    public Item GetItem(ItemList item) 
    {

        for (int i = 0; i < Inventory.Length; i++)
        {
            Item inventoryItem = Inventory[i].GetComponent<Item>();
            if (inventoryItem.ItemID == item)
            {
                return inventoryItem;
            }
        }

        return null;
    }

    /// <summary>
    ///  Get the first index of the param item
    /// </summary>
    /// <param name="item"></param>
    /// <returns> the index, -1 if their is none </returns>
    public int GetItemIndex(ItemList item) 
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            Item inventoryItem = Inventory[i].GetComponent<Item>();

            if (inventoryItem.ItemID == item)
            {
                return i;
            }
        }

        return -1;
    }

    public void GiveItem(GameObject GameObjectNewItem) 
    {
        Item item = GameObjectNewItem.GetComponent<Item>();
        if (item.ItemType == ItemType.Weapon)
        {
            if (weaponHand.GetComponent<Item>().ItemValue <= item.ItemValue)
            {
                weaponHand = GameObjectNewItem;
            }
        }
        else if (item.ItemType == ItemType.Armor)
        {
            if (BodyItem.GetComponent<Item>().ItemValue <= item.ItemValue)
            {
                BodyItem = GameObjectNewItem;
            }
        }
        else 
        {
            TryReplaceInventoryItem(GameObjectNewItem);
        }
    }

    private bool TryReplaceInventoryItem(GameObject GameObjectNewItem) 
    {

        int lowestItemValue = int.MaxValue;
        int lowestItemIndex = 0;
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i].GetComponent<Item>().ItemValue > lowestItemValue)
            {
                lowestItemValue = Inventory[i].GetComponent<Item>().ItemValue;
                lowestItemIndex = i;
            }
        }

        if (GameObjectNewItem.GetComponent<Item>().ItemValue > lowestItemValue)
        {
            Inventory[lowestItemIndex] = GameObjectNewItem;
            return true;
        }

        return false;
    }


    #endregion

    #endregion

}
