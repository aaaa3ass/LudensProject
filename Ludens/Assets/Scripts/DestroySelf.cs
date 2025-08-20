using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destroyTime = 0.6f;
    private void Start()
    {
        StartCoroutine(destroyRoutine());
    }

    IEnumerator destroyRoutine()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }


}
