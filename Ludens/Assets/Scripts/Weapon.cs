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
    public Sprite[] weaponImages = new Sprite[WEAPON_TYPE_COUNT];    // 이미지

    TurnManager turnManager;

    private void Start()
    {
        LoadWeaponData();
        LoadWeaponImage();
        turnManager = FindObjectOfType<TurnManager>();
    }
    private void LoadWeaponData()
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
        #region 오브
        weapons[1].Name = "Orb";
        weapons[1].ATK = 2;
        weapons[1].moveDistance = 1;
        weapons[1].AttackRange = new List<List<int>>
        {
            new List<int> { 1, 1, 1, },
            new List<int> { 1, 2, 1, },
            new List<int> { 1, 1, 1  }
        };
        #endregion
        #region 서
        weapons[2].Name = "Grimore";
        weapons[2].ATK = 3;
        weapons[2].moveDistance = 2;
        weapons[2].AttackRange = new List<List<int>>
        {
            new List<int> { 0, 2, 0, },
            new List<int> { 0, 1, 0, },
            new List<int> { 1, 1, 1  },
            new List<int> { 1, 1, 1  }
        };
        #endregion
        #region 환도
        weapons[3].Name = "Hwando";
        weapons[3].ATK = 4;
        weapons[3].moveDistance = 3;
        weapons[3].AttackRange = new List<List<int>>
        {
            new List<int> { 1, 1, 1 },
            new List<int> { 1, 1, 1 },
            new List<int> { 0, 2 }
        };
        #endregion
        #region 쌍검
        weapons[4].Name = "TwinSwords";
        weapons[4].ATK = 5;
        weapons[4].moveDistance = 4;
        weapons[4].AttackRange = new List<List<int>>
        {
            new List<int> { 1, 0, 0, 0, 1 },
            new List<int> { 0, 1, 0, 1 },
            new List<int> { 0, 0, 1 },
            new List<int> { 0, 0, 2 }
        };
        #endregion
        #region 대검
        weapons[5].Name = "GreatSword";
        weapons[5].ATK = 6;
        weapons[5].moveDistance = 5;
        weapons[5].AttackRange = new List<List<int>>
        {
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 2 }
        };
        #endregion
        #region 오래된 검
        weapons[6].Name = "OldSword";
        weapons[6].ATK = 0;
        weapons[6].moveDistance = 0;
        weapons[6].AttackRange = new List<List<int>>
        {
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 2 }
        };
        #endregion
    } // 무기 정보
    private void LoadWeaponImage()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weaponImages[i] = Resources.Load<Sprite>("Weapon/" + weapons[i].Name);
        }
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
