using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    WeaponInventory inventory;
    LoadoutUIManager loadoutUIManager;
    
    public int weaponNumber;

    private void Start()
    {
        inventory = FindObjectOfType<WeaponInventory>();
        loadoutUIManager = FindObjectOfType<LoadoutUIManager>();
    }

    public void OnButtonClick()
    {
        inventory.EquipWeapon(weaponNumber);
        loadoutUIManager.RefreshUI();
    }

}
