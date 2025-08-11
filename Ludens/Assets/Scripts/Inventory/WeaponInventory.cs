using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    const int SIZE = 10;
    public int[] weapons = new int[SIZE];
    public Sprite[] weaponImages = new Sprite[SIZE];
    public bool[] equipments = new bool[SIZE];
    public int count = 0;
    

    public Transform slotParent;
    public GameObject slotPrefab;
    public Transform[] slotEquipments;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 6 ; i++)
        {
            foreach (Transform child in slotEquipments[i])
                Destroy(child.gameObject);
        }

        int equipIndex = 0;
        for (int i = 0;i < SIZE; i++)
        {
            if (weapons[i] > 0) 
            {
                GameObject slot = Instantiate(slotPrefab);
                if (equipments[i] == true)
                {
                    slot.transform.parent = slotEquipments[equipIndex++];
                    slot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    slot.transform.parent = slotParent;   
                }
                slot.GetComponent<Image>().sprite = weaponImages[i];    // 이미지 설정 
                slot.GetComponent<InventoryButton>().weaponNumber = i;  // 무기 종류
                slot.transform.Find("Type").GetComponent<Text>().text = i.ToString();// 임시 넘버
                //slot.transform.Find("Quantity").GetComponent<Text>().text = weapons[i].ToString(); // 무기 수  

            }
        }

    }

    public void EquipWeapon(int num)
    {
        if (isFull()) return;
        if (equipments[num] == true) 
        {
            equipments[num] = false;
            count--;
        }
        else
        {
            equipments[num] = true;
            count++;
        }
            
        RefreshUI();
    }

    public bool isFull()
    {
        if(count >= 6)
        {
            return true;
        }
        return false;
    }
}
