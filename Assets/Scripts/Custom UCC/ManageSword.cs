using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Inventory;
using Opsive.UltimateCharacterController.Events;
using Opsive.UltimateCharacterController.Items;

public class ManageSword : MonoBehaviour
{
    int itemNum;

    public GameObject sword;
    public GameObject katana;

    public void Awake() {
    
        //EventHandler.RegisterEvent<Item, int>(gameObject, "OnInventoryEquipItem", showSword);
    }

    public void OnUnEquipItem(Item item, int ind)
    {
        //print("item: " + item.ItemType.name);

        //if (item.ItemType.name == "Katana")
        //{
        //    katana.active = true;
        //}
        //else if (item.ItemType.name == "Sword")
        //{
        //    sword.active = true;
        //}
    }
    
    public void OnEquipItem(Item item, int ind)
    {
        //print("item: " + item.ItemType.name);

        //if(item.ItemType.name == "Katana")
        //{
        //    katana.active = false;
        //}
        //else if (item.ItemType.name == "Sword")
        //{
        //    sword.active = false;
        //}
    }
}