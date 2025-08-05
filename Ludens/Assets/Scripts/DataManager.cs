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
        for(int i = 0;i < Loadout.Count; i++)
        {
            slots[i].GetComponentInChildren<Text>().text = Loadout[i].moveDistance.ToString();
        }
    }


}
