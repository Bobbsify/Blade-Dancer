using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float delay = 0.5f;

    private int currentObj = 1;

    private void Awake()
    {
        if (objects.Length > 0)
        {
            StartCoroutine(DoSwitch());
        }
    }

    private IEnumerator DoSwitch()
    {
        yield return new WaitForSeconds(delay);
        objects[currentObj].SetActive(false);
        currentObj++;
        if (currentObj == objects.Length) { currentObj = 0; }
        objects[currentObj].SetActive(true);
        StartCoroutine(DoSwitch());
    }
}
