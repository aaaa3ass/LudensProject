using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum CharacterState
{
    Idle,
    Moving,
    Attacking,
    Dead
}

public class Character : MonoBehaviour
{
    public TileManager tileManager;

    public CharacterState currentState;

    public float moveDuration = 0.3f; // 한 칸 이동하는 데 걸리는 시간
    public Vector2Int currentPositon = new Vector2Int(1,1); // 현재 위치
    public Vector2Int previousPositon = new Vector2Int(1,0);// 이전 위치 (방향 결정)

    public float ATK;
    public float HP;
    public float CRT;

    public List<Weapon> weaponList;



    void Start()
    {
        currentState = CharacterState.Idle; // 대기 상태

        transform.position = new Vector3(currentPositon.x, currentPositon.y * -1, 0); // 시작 위치

        //weaponList[0] = new TestWeapon();
    }

    #region 4방향 이동 벡터 directions
    private static readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(-1,0), // up
        new Vector2Int(1,0),  // down
        new Vector2Int(0,-1), // left
        new Vector2Int(0,1)   // right
    };
    #endregion
    #region 이동 관련
    public void move()
    {
        Vector2Int nextPositon = GetNextPosition(currentPositon, previousPositon);

        Vector3 newTargetPos = new Vector3(nextPositon.x, nextPositon.y * -1, 0);

        StartCoroutine(SmoothMove(newTargetPos));   // 이동

        // 위치 갱신
        previousPositon = currentPositon;
        currentPositon = nextPositon;
    }

    private Vector2Int GetNextPosition(Vector2Int current, Vector2Int previous)
    {
        if(!IsValidPositon(current))
        {
            Debug.LogError($"현재 위치 {current}가 범위를 벗어남");
            return current;
        }
        if (tileManager.loadedTiles[current.y][current.x] == 0)
        {
            Debug.LogError($"현재 위치 {current}가 범위를 벗어남");
            return current;
        }

        foreach (Vector2Int dir in directions) 
        { 
            Vector2Int neighborPos = current + dir;    // 이웃 위치 계산

            if (IsValidPositon(neighborPos))    // 범위 내에 있는지
            {
                if (tileManager.loadedTiles[neighborPos.y][neighborPos.x] == 1) // 갈 수 있는지
                { 
                    if(neighborPos != previous) // 이전 위치가 아닌지
                    {
                        return neighborPos;     // 다음 위치
                    }
                }
            }
        }
        Debug.LogWarning($"현재 : {current} 길이 없음");
        return current;
    }

    bool IsValidPositon(Vector2Int positon)
    {
        if (positon.y < 0 || positon.x >= tileManager.loadedTiles.Count) return false;  // 행 인덱스 검사
        if (positon.x < 0 || positon.y >= tileManager.loadedTiles.Count) return false;  // 열 인덱스 검사

        return true;
    }

    public IEnumerator SmoothMove(Vector3 newTarget)
    {
        currentState = CharacterState.Moving;       // 이동 상태로 설정
        Vector3 startPos = transform.position;      // 이동 시작 위치
        float elapsedTime = 0f;                     // 경과 시간

        while (elapsedTime < moveDuration)
        {
            // 이동 진행률 (0.0에서 1.0 사이)
            float t = elapsedTime / moveDuration;

            transform.position = Vector3.Lerp(startPos, newTarget,t);

            elapsedTime += Time.deltaTime;          // 시간 업데이트
            yield return null;                      // 다음 프레임까지 대기
        }

        transform.position = newTarget;             // 정확한 위치에 안착
        currentState = CharacterState.Idle;         // 정지 상태로 설정
    }
    #endregion

    void Update()
    {

    }
}