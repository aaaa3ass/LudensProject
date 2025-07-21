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
        #region �ε�� Ÿ�� �迭 Ȯ�� (������)
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
        // Resources �������� TextAsset �ε�
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        if (textAsset == null)
        {
            Debug.LogError($"������ ã�� �� �����ϴ�: {fileName}.txt �Ǵ� {fileName}.csv. Assets/Resources ������ �ִ��� Ȯ���ϼ���.");
            return;
        }

        // �ؽ�Ʈ ���� ������ �� �پ� �б�
        string[] lines = textAsset.text.Split('\n');

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim(); // ���� ����
            if (string.IsNullOrEmpty(trimmedLine)) continue; // �� �� �ǳʶٱ�

            // ��ǥ�� �и�
            string[] stringValues = trimmedLine.Split(',');
            List<int> currentRow = new List<int>();

            foreach (string strVal in stringValues)
            {
                int intVal;
                // int.TryParse�� ����Ͽ� �����ϰ� ��ȯ
                if (int.TryParse(strVal.Trim(), out intVal))
                {
                    currentRow.Add(intVal);
                }
                else
                {
                    Debug.LogWarning($"��ȿ���� ���� ���� �����Դϴ�: '{strVal.Trim()}' in line: {trimmedLine}");
                }
            }

            if (currentRow.Count > 0) // ��ȿ�� �����Ͱ� �ִ� �ุ �߰�
            {
                loadedTiles.Add(currentRow);
            }
        }
        Debug.Log($"�� {loadedTiles.Count}���� ���� �ε��߽��ϴ�.");
    }

    void SetTiles()
    {
        int width = 1; // Ÿ�� �ʺ�
        int height = 1;// Ÿ�� ����

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
