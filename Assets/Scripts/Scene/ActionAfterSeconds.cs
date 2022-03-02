using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float timeToExecute;

    [SerializeField]
    private UnityEvent actions;

    private void Awake()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute() 
    {
        yield return new WaitForSeconds(timeToExecute);
        actions.Invoke();
    }
}
