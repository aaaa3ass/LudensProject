using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Tilemaps;

public class TileManager : MonoBehaviour
{
    public string fileName = "tiles_data";
    public List<List<int>> loadedTiles = new List<List<int>>();

    public GameObject tileObject;

    // Start is called before the first frame update
    void Start()
    {
        LoadTilesFromTextFile();
        #region 로드된 타일 배열 확인 (디버깅용)
        /*Debug.Log("Loaded 2D Integer Array:");
        for (int i = 0; i < loadedTiles.Count; i++)
        {
            string rowString = $"Row {i}: [";
            for (int j = 0; j < loadedTiles[i].Count; j++)
            {
                rowString += loadedTiles[i][j];
                if (j < loadedTiles[i].Count - 1)
                {
                    rowString += ", ";
                }
            }
            rowString += "]";
            Debug.Log(rowString);
        }*/
        #endregion

        SetTiles();
    }

    void LoadTilesFromTextFile()
    {
        // Resources 폴더에서 TextAsset 로드
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        if (textAsset == null)
        {
            Debug.LogError($"파일을 찾을 수 없습니다: {fileName}.txt 또는 {fileName}.csv. Assets/Resources 폴더에 있는지 확인하세요.");
            return;
        }

        // 텍스트 파일 내용을 한 줄씩 읽기
        string[] lines = textAsset.text.Split('\n');

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim(); // 공백 제거
            if (string.IsNullOrEmpty(trimmedLine)) continue; // 빈 줄 건너뛰기

            // 쉼표로 분리
            string[] stringValues = trimmedLine.Split(',');
            List<int> currentRow = new List<int>();

            foreach (string strVal in stringValues)
            {
                int intVal;
                // int.TryParse를 사용하여 안전하게 변환
                if (int.TryParse(strVal.Trim(), out intVal))
                {
                    currentRow.Add(intVal);
                }
                else
                {
                    Debug.LogWarning($"유효하지 않은 숫자 형식입니다: '{strVal.Trim()}' in line: {trimmedLine}");
                }
            }

            if (currentRow.Count > 0) // 유효한 데이터가 있는 행만 추가
            {
                loadedTiles.Add(currentRow);
            }
        }
        Debug.Log($"총 {loadedTiles.Count}개의 행을 로드했습니다.");
    }

    void SetTiles()
    {
        int width = 1; // 타일 너비
        int height = 1;// 타일 길이

        int row = 0, column = 0;
        foreach (List<int> tiles in loadedTiles)
        {
            column = 0;
            foreach (int tile in tiles)
            {
                if (tile == 1)
                {
                    GameObject go = Instantiate(tileObject);
                    go.transform.position = new Vector2(column * width, -row * height);
                }
                column++;
            }
            row++;
        }
    }

}
