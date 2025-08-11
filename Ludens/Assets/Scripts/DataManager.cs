using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤 패턴

    public List<Weapon> Inventory;
    public List<Weapon> Loadout;
    public List<int> Weapons;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null) // 처음 생성될 때 싱글톤 인스턴스 할당
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀔 때 파괴되지 않게
        }
        else
        {
            Destroy(gameObject); // 새 오브젝트 파괴
        }
        #endregion
        //SampleWeaponsAdd();
    }

    void Start()
    {
        Debug.Log("DataManager 시작");
        LoadInventory();
        SaveInventory(Inventory);
    }

    private void Update()
    {

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
            if (weapon.weaponType == Loadout[i].weaponType)
            {
                //Debug.Log($"{i}번째에 있는 무기 {Loadout[i].moveDistance} 해제");
                Loadout.RemoveAt(i);
                return;
            }
        }
        Loadout.Add(weapon);
    }

    private void SampleWeaponsAdd()
    {
        for (int i = 0; i < 30; i++)
        {
            Inventory.Add(new Weapon()); // 인벤토리에 추가
            Inventory[i].weaponType = i; // 임시 넘버링

        }
    }

    [System.Serializable]
    public class InventoryDataContainer
    {
        public List<Weapon> Weapons;
    }

    public void SaveInventory(List<Weapon> weapons)
    {
        InventoryDataContainer container = new InventoryDataContainer();
        container.Weapons = weapons;

        string json = JsonUtility.ToJson(container);
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        File.WriteAllText(path, json);
        Debug.Log("인벤토리 저장 완료: " + path);
    }

    public List<Weapon> LoadInventory()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryDataContainer container = JsonUtility.FromJson<InventoryDataContainer>(json);
            Debug.Log("인벤토리 로드 완료.");
            return container.Weapons;
        }
        Debug.Log("저장된 인벤토리 파일 없음.");
        return new List<Weapon>();
    }
}
