using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory Instance;             // 싱글톤

    public const int SIZE = 7;
    public int[] weapons = new int[Weapon.WEAPON_TYPE_COUNT];// 무기 종류
    public bool[] equipments = new bool[Weapon.WEAPON_TYPE_COUNT];          // 장착 여부
    public int count = 0;                               // 장착 무기 수

    public Sprite[] weaponImages = new Sprite[Weapon.WEAPON_TYPE_COUNT];    // 이미지
    
    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록
        }
        else
        {
            Destroy(gameObject);
        }
        LoadWeaponImages();
    }

    private void LoadWeaponImages()
    {
        weaponImages[0] = Resources.Load<Sprite>("Weapon/Punch");
        weaponImages[1] = Resources.Load<Sprite>("Weapon/OldSword");
        weaponImages[2] = Resources.Load<Sprite>("Weapon/TwinSwords");
        weaponImages[3] = Resources.Load<Sprite>("Weapon/Hwando");
        weaponImages[4] = Resources.Load<Sprite>("Weapon/Orb");
        weaponImages[5] = Resources.Load<Sprite>("Weapon/Grimore");
        weaponImages[6] = Resources.Load<Sprite>("Weapon/Knife");
    }   // 무기 이미지 로드

    public void EquipWeapon(int num)
    {

        if (equipments[num] == true) 
        {
            equipments[num] = false;
            count--;
        }
        else
        {
            if (isFull()) return;
            equipments[num] = true;
            count++;
        }
            
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
