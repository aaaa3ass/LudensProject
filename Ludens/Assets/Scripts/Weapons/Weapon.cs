using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public List<List<int>> AttackRange;
    public int moveDistance;
    public float ATK;
    public string imageName;
    public Image image;
    public int weaponType;

    private void Awake()
    {
        //Debug.Log("公扁 积己");
        //image.sprite = Resources.Load<Sprite>(imageName);
    }

    private void Start()
    {
        imageName = "WeaponSample";
    }

    private void OnDestroy()
    {
        Debug.Log($"{moveDistance} 公扁 颇鲍!");
    }

}
