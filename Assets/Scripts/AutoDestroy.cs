using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(timeDestroy());
    }

    IEnumerator timeDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
