using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    WeaponInventory weaponinventory;
    Weapon weaponClass;

    public GameObject weaponSlotPrefab; // 무기 버튼 프리팹
    public Transform[] weaponSlotParent; // 무기 버튼 위치

    int usedWeapon = -1;


    private void Start()
    {
        weaponinventory = FindObjectOfType<WeaponInventory>();
        weaponClass = FindObjectOfType<Weapon>();
        InitiateUI();
    }

    void InitiateUI()
    {
        int equipIndex = 0;
        for (int i = 0; i < WeaponInventory.SIZE; i++)
        {
            if (weaponinventory.equipments[i] == true) // 장착 상태면
            {
                GameObject slot = Instantiate(weaponSlotPrefab); // UI 프리팹 생성
                slot.transform.parent = weaponSlotParent[equipIndex++]; // 빈 슬롯에 위치하고 인덱스 증가
                slot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // 정확한 위치
                slot.GetComponent<Image>().sprite = weaponClass.weaponImages[i]; // 이미지 설정
                slot.GetComponent<WeaponButton>().weaponNumber = i; // 무기 종류
                slot.GetComponentInChildren<Button>().interactable = false; // 버튼 비활성화
                slot.transform.Find("MoveDistance").GetComponent<Text>().text = weaponClass.MoveDistance(i).ToString();
            }
        }
    }

    public void InactiveButton(int weaponNumber)
    {
        usedWeapon = weaponNumber;
        foreach (Transform t in weaponSlotParent)
        {
            t.GetComponentInChildren<Button>().interactable = false;
        }
    }

    public void ActiveButton()
    {
        foreach(Transform t in weaponSlotParent)
        {
            if (t.GetComponentInChildren<WeaponButton>().weaponNumber == usedWeapon)
            {
                continue;
            }
            t.GetComponentInChildren<Button>().interactable = true;
        }
        usedWeapon = -1;
    }
}
