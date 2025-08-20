using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    TurnManager turnManager;
    Weapon weapon;
    ButtonHoverAction buttonHover;

    public int weaponNumber;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        weapon = FindObjectOfType<Weapon>();
        buttonHover = GetComponent<ButtonHoverAction>();
    }

    public void OnButtonClick()
    {
        //Debug.Log("무기 버튼 누름");
        turnManager.moveDistance = weapon.MoveDistance(weaponNumber);
        turnManager.weaponType = weaponNumber;
        turnManager.SetTurnState(TurnState.Attack);
    }

    private void Update()
    {
        if (buttonHover.OnPointer && turnManager.state == TurnState.Select) // 마우스를 올려 놓으면
        {
            turnManager.Players[turnManager.turnPlayer].DisplayAttackRange(weaponNumber); // 공격 범위 Display
        }
    }
}
