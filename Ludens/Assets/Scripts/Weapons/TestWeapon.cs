using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{

    private void Awake()
    {
        Debug.Log("TestWeapon »ý¼º");
    }
    void Start()
    {

        imageName = "TestWeapon";
        AttackRange = new List<List<int>>
        {
            new List<int> {1, 0, 0, 0, 1},
            new List<int> {0, 1, 0, 1, 0},
            new List<int> {0, 0, 1, 0, 0},
            new List<int> {0, 0, 2, 0, 0},
        };
        moveDistance = 3;
        ATK = 40;
    }

}
