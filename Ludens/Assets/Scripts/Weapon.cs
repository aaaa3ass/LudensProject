using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public const int WEAPON_TYPE_COUNT = 7; // 무기 종류 수
    struct weaponData
    {
        public List<List<int>> AttackRange;
        public int moveDistance;
        public int ATK;
        public string Name;
    }

    weaponData[] weapons = new weaponData[WEAPON_TYPE_COUNT];

    TurnManager turnManager;

    private void Start()
    {
        LoadWeaponData();
        turnManager = FindObjectOfType<TurnManager>();
    }
    public void LoadWeaponData()
    {
        #region 주먹
        weapons[0].Name = "Punch";
        weapons[0].ATK = 1;
        weapons[0].moveDistance = 6;
        weapons[0].AttackRange = new List<List<int>>
        {
            new List<int> { 1 },
            new List<int> { 2 }
        };
        #endregion
        #region 오래된 검
        weapons[1].Name = "OldSword";
        weapons[1].ATK = 2;
        weapons[1].moveDistance = 5;
        weapons[1].AttackRange = new List<List<int>>
        {
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 2 }
        };
        #endregion
        #region 쌍검
        weapons[2].Name = "TwinSwords";
        weapons[2].ATK = 3;
        weapons[2].moveDistance = 1;
        weapons[2].AttackRange = new List<List<int>>
        {
            new List<int> { 1, 0, 0, 0, 1 },
            new List<int> { 0, 1, 0, 1 },
            new List<int> { 0, 0, 1 },
            new List<int> { 0, 0, 2 }
        };
        #endregion
    }

    public void NormalAttack()
    {

    }

    public void SkillAttack()
    {

    }

    public int MoveDistance(int weaponNumber)
    {
        return weapons[weaponNumber].moveDistance;
    }

    public List<List<int>> AttackRange(int weaponNumber)
    {
        return weapons[weaponNumber].AttackRange;
    }

}
