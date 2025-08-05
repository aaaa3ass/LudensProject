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
            Inventory.Add(new Weapon());
            Inventory[i].moveDistance = i + 1;
            GameObject newObject = Instantiate(weaponimage, viewport);
            newObject.name = "" + i;
            Text child = newObject.GetComponentInChildren<Text>();
            child.text = "" + (i + 1);
        }
    }



}
