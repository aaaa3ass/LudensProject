using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{
    DataManager dataManager;
    public List<Image> slots;
    public GameObject weaponimage;
    public Transform viewport;

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        ButtonSetting();
    }

    // Update is called once per frame
    void Update()
    {
        #region 장착 무기 업데이트
        for (int i = 0; i < 6; i++)
        {
            if (dataManager.Loadout.Count <= i)
            {
                slots[i].GetComponentInChildren<Text>().text = "None";
            }
            else
            {
                slots[i].GetComponentInChildren<Text>().text = dataManager.Loadout[i].weaponType.ToString();
            }
        }
        #endregion
    }

    void ButtonSetting()
    {
        for(int i = 0;i < dataManager.Inventory.Count;i++)
        {
            GameObject newObject = Instantiate(weaponimage, viewport); // 무기 장착 버튼 생성
            newObject.name = "" + i; // 이름 변경
            InventoryButton button = newObject.GetComponent<InventoryButton>(); // 버튼 할당
            button.weapon = dataManager.Inventory[i];
            button.dataManager = dataManager; // DataManager 연결
            Text child = newObject.GetComponentInChildren<Text>(); // 텍스트 연결
            child.text = "" + dataManager.Inventory[i].weaponType; // 텍스트 변경
        }
    }

    public void SaveLoadout()
    {
        for(int i = 0; i < dataManager.Loadout.Count; i++)
        {

        }
    }
}
