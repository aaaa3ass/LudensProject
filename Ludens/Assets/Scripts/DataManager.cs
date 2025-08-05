using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤 패턴

    public List<Weapon> Inventory;
    public List<Weapon> Loadout;
    public GameObject weaponimage;
    public Transform viewport;
    public List<Image> slots;

    private void Awake()
    {
        if(instance == null) // 처음 생성될 때 싱글톤 인스턴스 할당
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀔 때 파괴되지 않게
        }
        else
        {
            Destroy(gameObject); // 새 오브젝트 파괴
        }
    }

    void Start()
    {
        for(int i = 0; i < 30;i++)
        {
            Inventory.Add(new Weapon()); // 인벤토리에 추가
            Inventory[i].moveDistance = i; // 임시 넘버링
            GameObject newObject = Instantiate(weaponimage, viewport); // 무기 생성
            newObject.name = "" + i; // 이름 변경
            InventoryButton button = newObject.GetComponent<InventoryButton>(); // 버튼 할당
            button.weapon = Inventory[i];
            button.dataManager = this; // DataManager 연결
            Text child = newObject.GetComponentInChildren<Text>(); // 텍스트 연결
            child.text = "" + i; // 텍스트 변경
        }
    }

    private void Update()
    {
        #region 장착 무기 업데이트
        for (int i = 0; i < 6; i++)
        {
            if (Loadout.Count <= i)
            {
                slots[i].GetComponentInChildren<Text>().text = "None";
            }
            else 
            {
                slots[i].GetComponentInChildren<Text>().text = Loadout[i].moveDistance.ToString();
            }
        }
        #endregion

    }

    public void WeaponSelect(Weapon weapon)
    {

        if (Loadout.Count == 0)
        {
            Loadout.Add(weapon);
            return;
        }
        int count = Loadout.Count;

        for(int i = 0; i < count; i++)
        {
            if (weapon.moveDistance == Loadout[i].moveDistance)
            {
                //Debug.Log($"{i}번째에 있는 무기 {Loadout[i].moveDistance} 해제");
                Loadout.RemoveAt(i);
                return;
            }
        }
        Loadout.Add(weapon);
    }


}
