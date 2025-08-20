using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    TurnManager turnManager;
    InGameUIManager gameUIManager;

     public GameObject highlight;
    
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        gameUIManager = FindObjectOfType<InGameUIManager>();

        StartCoroutine(buttonHighlight());
    }

    IEnumerator buttonHighlight()
    {
        int weaponNumber = -1;
        if (turnManager.turnCount == 0) { weaponNumber = 3; }
        if (turnManager.turnCount == 2) { weaponNumber = 0; }
        if (turnManager.turnCount == 4) { weaponNumber = 5; }

        if(weaponNumber != -1 && turnManager.state == TurnState.Select)
        {
            for(int i = 0;i< 3;i++)
            {
                GameObject prefab = Instantiate(highlight);
                prefab.transform.parent = gameUIManager.weaponSlotParent[weaponNumber];
                prefab.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // 정확한 위치

                yield return new WaitForSeconds(0.3f);
            }

        }

        if(turnManager.turnCount >= 5)
        {
            yield break;
        }

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(buttonHighlight());
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
