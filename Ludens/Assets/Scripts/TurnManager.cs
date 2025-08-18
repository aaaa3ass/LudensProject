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
    public static TurnManager Instance { get; private set; } // 싱글톤 패턴

    public TurnState state;

    public int playerCount = 1; // 플레이어 수
    public int turnPlayer;      // 턴 플레이어
    public int turnCount;       // 턴 수
    public Text turnCountText;  // 턴 텍스트

    public Character[] Players;
    public int moveDistance = 0;
    public int weaponType = 0;

    private InGameUIManager gameUIManager;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTurnState(TurnState newState)
    {
        state = newState;

        switch (state)
        {
            case TurnState.StartGame:
                //Debug.Log("게임시작 초기화");
                StartCoroutine(HandleStartGame());
                break;
            case TurnState.Select:
                //Debug.Log($"플레이어{turnPlayer + 1}무기 선택");
                StartCoroutine(HandleSelect());
                break;
            case TurnState.Attack:
                //Debug.Log("공격");
                StartCoroutine(HandleAttack());
                break;
            case TurnState.Move:
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
        gameUIManager = FindObjectOfType<InGameUIManager>();
        SetTurnState(TurnState.StartGame);
    }

    IEnumerator HandleStartGame()
    {
        yield return new WaitForSeconds(0.5f);
        SetTurnState(TurnState.Select);
    }
    IEnumerator HandleSelect()
    {
        gameUIManager.ActiveButton(); // 공격 버튼 활성화
        yield break;
    }
    IEnumerator HandleAttack()
    {
        gameUIManager.InactiveButton(weaponType); // 공격 버튼 비활성화

        if(weaponType == 0)
        {
            StartCoroutine(HandlePunch());
            yield break;
        }

        Players[turnPlayer].Attack(weaponType);        
        yield return new WaitForSeconds(1.0f);
        

        //Debug.Log(weaponType.ToString());

        if (Players[turnPlayer].Fixed == true)
        {
            SetTurnState(TurnState.EndTurn);
        }
        else
        {
            SetTurnState(TurnState.Move);
        }

    }
    public IEnumerator HandleMove() 
    {
        for (int i = 0; i < moveDistance; i++)
        {
            Players[turnPlayer].move();
            yield return new WaitForSeconds(Players[turnPlayer].moveDuration + 0.1f);
        }

        SetTurnState(TurnState.EndTurn);
        yield break;
    }

    IEnumerator HandleTurnEnd()
    {
        turnCount++;                            // 턴 증가
        turnPlayer = turnCount % playerCount;// 턴 플레이어 변경
        turnCountText.text = $"{turnCount} 턴"; // 턴 표시
        SetTurnState(TurnState.Select);
        yield break;
    }

    IEnumerator HandlePunch()
    {
        //Debug.Log("펀치 공격");
        for(int i = 0; i < moveDistance; i++)
        {
            if (Players[turnPlayer].isEnemyInFrontOf())
            {
                Players[turnPlayer].Attack(0);
                yield return new WaitForSeconds(1.0f);
                break;
            }
            Players[turnPlayer].move();
            yield return new WaitForSeconds(Players[turnPlayer].moveDuration + 0.1f);
        }

        if (Players[turnPlayer].isEnemyInFrontOf())
        {
            Players[turnPlayer].Attack(0);
            yield return new WaitForSeconds(1.0f);
        }
            
        SetTurnState(TurnState.EndTurn);
        yield break;
    }

}
