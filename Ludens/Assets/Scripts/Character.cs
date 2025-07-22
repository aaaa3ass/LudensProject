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

    public float moveDuration = 0.3f; // �� ĭ �̵��ϴ� �� �ɸ��� �ð�
    public Vector2Int currentPositon = new Vector2Int(1,1); // ���� ��ġ
    public Vector2Int previousPositon = new Vector2Int(1,0);// ���� ��ġ (���� ����)

    public float ATK;
    public float HP;
    public float CRT;

    public List<Weapon> weaponList;



    void Start()
    {
        currentState = CharacterState.Idle; // ��� ����

        transform.position = new Vector3(currentPositon.x, currentPositon.y * -1, 0); // ���� ��ġ

        //weaponList[0] = new TestWeapon();
    }

    #region 4���� �̵� ���� directions
    private static readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(-1,0), // up
        new Vector2Int(1,0),  // down
        new Vector2Int(0,-1), // left
        new Vector2Int(0,1)   // right
    };
    #endregion
    #region �̵� ����
    public void move()
    {
        Vector2Int nextPositon = GetNextPosition(currentPositon, previousPositon);

        Vector3 newTargetPos = new Vector3(nextPositon.x, nextPositon.y * -1, 0);

        StartCoroutine(SmoothMove(newTargetPos));   // �̵�

        // ��ġ ����
        previousPositon = currentPositon;
        currentPositon = nextPositon;
    }

    private Vector2Int GetNextPosition(Vector2Int current, Vector2Int previous)
    {
        if(!IsValidPositon(current))
        {
            Debug.LogError($"���� ��ġ {current}�� ������ ���");
            return current;
        }
        if (tileManager.loadedTiles[current.y][current.x] == 0)
        {
            Debug.LogError($"���� ��ġ {current}�� ������ ���");
            return current;
        }

        foreach (Vector2Int dir in directions) 
        { 
            Vector2Int neighborPos = current + dir;    // �̿� ��ġ ���

            if (IsValidPositon(neighborPos))    // ���� ���� �ִ���
            {
                if (tileManager.loadedTiles[neighborPos.y][neighborPos.x] == 1) // �� �� �ִ���
                { 
                    if(neighborPos != previous) // ���� ��ġ�� �ƴ���
                    {
                        return neighborPos;     // ���� ��ġ
                    }
                }
            }
        }
        Debug.LogWarning($"���� : {current} ���� ����");
        return current;
    }

    bool IsValidPositon(Vector2Int positon)
    {
        if (positon.y < 0 || positon.x >= tileManager.loadedTiles.Count) return false;  // �� �ε��� �˻�
        if (positon.x < 0 || positon.y >= tileManager.loadedTiles.Count) return false;  // �� �ε��� �˻�

        return true;
    }

    public IEnumerator SmoothMove(Vector3 newTarget)
    {
        currentState = CharacterState.Moving;       // �̵� ���·� ����
        Vector3 startPos = transform.position;      // �̵� ���� ��ġ
        float elapsedTime = 0f;                     // ��� �ð�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����� (0.0���� 1.0 ����)
            float t = elapsedTime / moveDuration;

            transform.position = Vector3.Lerp(startPos, newTarget,t);

            elapsedTime += Time.deltaTime;          // �ð� ������Ʈ
            yield return null;                      // ���� �����ӱ��� ���
        }

        transform.position = newTarget;             // ��Ȯ�� ��ġ�� ����
        currentState = CharacterState.Idle;         // ���� ���·� ����
    }
    #endregion

    void Update()
    {

    }
}