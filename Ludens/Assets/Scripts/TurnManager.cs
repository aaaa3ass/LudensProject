using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum TurnState 
{ 
    StartGame,
    Select,
    Attack,
    Move,
    EndTurn,
    EndGame
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; } // �̱��� ����

    public TurnState state;
    public Button TestButton1;
    public Button TestButton2;

    public int playerCount = 1;
    public int turnPlayer;
    public int turnCount;
    public Text turnCountText;

    public Character testPlayer;
    public Character[] Players;
    public int moveDistance = 0;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTurnStateMove()
    {
        SetTurnState(TurnState.Move);
    }

    public void SetMoveDistance(int n)
    {
        moveDistance = n;
    }

    public void SetTurnState(TurnState newState)
    {
        state = newState;

        switch (state)
        {
            case TurnState.StartGame:
                Debug.Log("���ӽ��� �ʱ�ȭ");
                StartCoroutine(HandleStartGame());
                break;
            case TurnState.Select:
                Debug.Log($"�÷��̾�{turnPlayer + 1}���� ����");
                StartCoroutine(HandleSelect());
                break;
            case TurnState.Move:
                //Debug.Log("�̵�");
                StartCoroutine(HandleMove());
                break;
            case TurnState.EndTurn:
                StartCoroutine(HandleTurnEnd());
                break;

        }
    }

    void Start()
    {
        turnPlayer = 0;
        SetTurnState(TurnState.StartGame);
    }


    IEnumerator HandleStartGame()
    {
        yield return new WaitForSeconds(0.5f);
        SetTurnState(TurnState.Select);
    }
    IEnumerator HandleSelect()
    {
        if (TestButton1 != null)    // ��ư Ȱ��ȭ
        {
            TestButton1.interactable = true;
        }
        if (TestButton2 != null)    // ��ư Ȱ��ȭ
        {
            TestButton2.interactable = true;
        }
        yield break;
    }
    public IEnumerator HandleMove() 
    {
        TestButton1.interactable = false;
        TestButton2.interactable = false;
        for (int i = 0; i < moveDistance; i++)
        {
            testPlayer.move();
            //Players[turnPlayer].move();
            yield return new WaitForSeconds(testPlayer.moveDuration);
        }
        SetTurnState(TurnState.EndTurn);
    }

    IEnumerator HandleTurnEnd()
    {
        turnCount++;
        turnCountText.text = $"{turnCount} ��";
        SetTurnState(TurnState.Select);
        yield break;

    }

}
