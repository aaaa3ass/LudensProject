using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    TurnManager turnManager;
    Weapon weapon;

    public int weaponNumber;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        weapon = FindObjectOfType<Weapon>();
    }

    public void OnButtonClick()
    {
        Debug.Log("버튼 누름");
        turnManager.moveDistance = weapon.MoveDistance(weaponNumber);
        turnManager.SetTurnState(TurnState.Move);
    }
}
