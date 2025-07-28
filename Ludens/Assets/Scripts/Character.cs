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
    SpriteRenderer spriteRenderer;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite sideSprite;

    public TileManager tileManager;

    public CharacterState currentState;

    public float moveDuration = 0.3f; // 한 칸 이동하는 데 걸리는 시간
    public Vector2Int currentPosition = new Vector2Int(1,1); // 현재 위치
    public Vector2Int previousPosition = new Vector2Int(1,0);// 이전 위치 (방향 결정)
    private CharacterDirection currectDirection;

    float ATK;
    float HP;
    float CRT;

    List<Weapon> weaponList;

    void Start()
    {
        currentState = CharacterState.Idle; // 대기 상태

        transform.position = new Vector3(currentPosition.x, currentPosition.y * -1, 0); // 시작 위치
        currectDirection = CharacterDirection.Down;

        spriteRenderer = GetComponent<SpriteRenderer>();    // 이미지
    }

    #region 4방향 이동 벡터 directions
    private enum CharacterDirection
    {
        Up, Down, Left, Right
    }
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
        Vector2Int nextPosition = GetNextPosition(currentPosition, previousPosition);

        Vector3 newTargetPos = new Vector3(nextPosition.x, nextPosition.y * -1, 0);

        StartCoroutine(SmoothMove(newTargetPos));   // 이동

        // 위치 갱신
        previousPosition = currentPosition;
        currentPosition = nextPosition;

    }

    private Vector2Int GetNextPosition(Vector2Int current, Vector2Int previous)
    {
        if(!IsValidPositon(current))
        {
            Debug.LogError($"현재 위치 {current}가 범위를 벗어남");
            return current;
        }
        if (tileManager.loadedTiles[current.y][current.x] != 1)
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
        if (positon.y < 0 || positon.x >= tileManager.loadedTiles.Count + 2) return false;  // 행 인덱스 검사
        if (positon.x < 0 || positon.y >= tileManager.loadedTiles.Count + 2) return false;  // 열 인덱스 검사
    
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

        SetDirection();
    }

    public void SetDirection()
    {
        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighborPos = currentPosition + dir;
            if (IsValidPositon(neighborPos) && neighborPos.y >= 0 && neighborPos.x >= 0)
            {
                if (tileManager.loadedTiles[neighborPos.y][neighborPos.x] == 1) // 갈 수 있는지
                {
                    if (neighborPos != previousPosition) // 이전 위치가 아닌지
                    {
                        if (dir == new Vector2Int(0, -1))
                        {
                            currectDirection = CharacterDirection.Up;
                        }
                        if (dir == new Vector2Int(0, 1)) 
                        {
                            currectDirection = CharacterDirection.Down;
                        }
                        if (dir == new Vector2Int(1, 0))
                        {
                            currectDirection = CharacterDirection.Right;
                        }
                        if (dir == new Vector2Int(-1, 0))
                        {
                            currectDirection = CharacterDirection.Left;
                        }
                    }
                }
            }

        }

    }
    #endregion

    void Update()
    {
        if(currectDirection == CharacterDirection.Up)
        {
            spriteRenderer.sprite = upSprite;
        }
        if(currectDirection == CharacterDirection.Down)
        {
            spriteRenderer.sprite = downSprite;
        }
        if(currectDirection == CharacterDirection.Left)
        {
            spriteRenderer.sprite = sideSprite;
            spriteRenderer.flipX = false;
        }
        if(currectDirection == CharacterDirection.Right)
        {
            spriteRenderer.sprite = sideSprite;
            spriteRenderer.flipX = true;
        }

    }
}