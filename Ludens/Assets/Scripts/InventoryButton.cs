using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public DataManager dataManager;
    private Button buttonComponent;
    public Weapon weapon;
    void Start()
    {
        buttonComponent = GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (dataManager.Loadout.Count < 6)
        {
            Debug.Log($"{this.name} 무기 추가");
            dataManager.Loadout.Add(weapon);
            
        }
    }
}
