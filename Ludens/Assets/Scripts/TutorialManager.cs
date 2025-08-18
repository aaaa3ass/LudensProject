using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    TurnManager turnManager;
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }
    void Update()
    {
        //if (turnManager.turnCount % 2 == 1)
        //{
        //    turnManager.weaponType = 3; // È¯µµ
        //    turnManager.SetTurnState(TurnState.Attack);
        //}
    }
}
