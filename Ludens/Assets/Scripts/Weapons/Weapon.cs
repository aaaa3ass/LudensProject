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

    private void Awake()
    {
        image.sprite = Resources.Load<Sprite>(imageName);
    }


}
