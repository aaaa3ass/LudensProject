using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(destroyRoutine());
    }

    IEnumerator destroyRoutine()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {

    }

}
