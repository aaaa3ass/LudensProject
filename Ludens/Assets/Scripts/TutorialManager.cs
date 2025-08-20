using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    TurnManager turnManager;
    InGameUIManager gameUIManager;
    
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        gameUIManager = FindObjectOfType<InGameUIManager>();
    }

    public void TutorialRoutine(int turnCount)
    {
        int weaponNumber = 0;
        if ( turnCount == 0 ) { weaponNumber = 3; }
        if ( turnCount == 2 ) { weaponNumber = 0; }
        if ( turnCount == 4 ) { weaponNumber = 5; }

        foreach (Transform t in gameUIManager.weaponSlotParent)
        {
            if (t.GetComponentInChildren<WeaponButton>().weaponNumber == weaponNumber)
            {
                t.GetComponentInChildren<Button>().interactable = true;
            }
        }
    }
}
