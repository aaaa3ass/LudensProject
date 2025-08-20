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

    }
}
