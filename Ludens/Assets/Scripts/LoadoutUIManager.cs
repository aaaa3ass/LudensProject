using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutUIManager : MonoBehaviour
{
    public List<Button> buttonList;
    public List<Text> textList;
    public DataManager dataManager;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }
    void Start()
    {
        Debug.Log("ui MANAGER 가동");
        if(dataManager.Inventory.Count > 0)
        {
            Debug.Log("카운트 > 0");
            if (dataManager.Inventory[0] == null)
            {
                Debug.Log("데이터 로드 실패");
            }
        }
    }

    void Update()
    {

    }

}
