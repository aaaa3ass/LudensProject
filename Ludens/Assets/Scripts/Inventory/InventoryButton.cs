using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public Button button;
    public bool isPressed = false;
    WeaponInventory inventory;
    public int weaponNumber;

    private void Start()
    {
        button = GetComponent<Button>();
        inventory = FindObjectOfType<WeaponInventory>();
    }

    public void OnButtonClick()
    {
        if (inventory.isFull()) return;

        #region 버튼 토글
        //Image color = GetComponent<Image>();
        //if(isPressed)
        //{
        //    color.color = Color.white;
        //    isPressed = false;
        //}
        //else
        //{
        //    color.color = Color.grey;
        //    isPressed = true;
        //}
        #endregion

        inventory.EquipWeapon(weaponNumber);
    }

}
