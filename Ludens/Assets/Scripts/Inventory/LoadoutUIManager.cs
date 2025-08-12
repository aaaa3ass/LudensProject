using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutUIManager : MonoBehaviour
{
    WeaponInventory weaponInventory;

    public Transform slotParent;        // 인벤토리 위치
    public GameObject slotPrefab;       // 무기 버튼 프리팹
    public Transform[] slotEquipments;  // 장착 무기 UI 위치

    void Start()
    {
        weaponInventory = FindAnyObjectByType<WeaponInventory>();
        RefreshUI();
    }

    public void RefreshUI()
    {
        #region 초기화
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < 6; i++)
        {
            foreach(Transform child in slotEquipments[i])
                Destroy(child.gameObject);
        }
        #endregion
        #region UI생성
        int equipIndex = 0;
        for (int i = 0; i< WeaponInventory.SIZE; i++) // 무기 전체 종류만큼 반복
        {
            if (weaponInventory.weapons[i] > 0) // 해당 무기가 하나 이상 있으면
            {
                GameObject slot = Instantiate(slotPrefab); // UI 프리팹 생성
                if (weaponInventory.equipments[i] == true) // 장착 상태면
                {
                    slot.transform.parent = slotEquipments[equipIndex++]; // 빈 슬롯에 위치하고 인덱스 증가
                    slot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // 정확한 위치
                }
                else // 장착 상태가 아니면
                {
                    slot.transform.parent = slotParent; // 인벤토리 창에 위치
                }
                slot.GetComponent<Image>().sprite = weaponInventory.weaponImages[i]; // 이미지 설정
                slot.GetComponent<InventoryButton>().weaponNumber = i; // 무기 종류
                slot.transform.Find("Type").GetComponent<Text>().text = i.ToString();   // 넘버링     
            }
        }
        #endregion
    }

}
