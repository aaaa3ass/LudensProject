using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public DataManager dataManager;
    private Button buttonComponent;
    private Image imageComponent;
    public Weapon weapon;
    private bool selected;
    void Start()
    {
        selected = false;
        buttonComponent = GetComponent<Button>();
        imageComponent = GetComponent<Image>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (!selected)  // 선택되지 않은 무기
        {
            if (dataManager.Loadout.Count >= 6) // 6개 넘게 장착 불가능
            {
                return;
            }
            selected = true;    // 버튼 비활성화
            imageComponent.color = Color.black;
        }
        else if(selected)// 선택된 무기
        {
            selected=false;     // 버튼 활성화
            imageComponent.color = Color.white;
        }
        dataManager.WeaponSelect(weapon);

    }
}
