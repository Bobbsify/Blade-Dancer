using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTriggerArea : MonoBehaviour
{
    [SerializeField]
    private TriggerMode triggerMode;

    [SerializeField]
    private UnityEvent eventsToTriggerOnEnter;

    [SerializeField]
    private UnityEvent eventsToTriggerOnExit;

    private Collider col;
    private void Awake()
    {
        TryGetComponent(out col);
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController pc))
        {
            if (triggerMode == TriggerMode.TriggerOnce)
            {
                this.enabled = false;
            }
            eventsToTriggerOnEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController pc))
        {
            if (triggerMode == TriggerMode.TriggerOnce)
            {
                this.enabled = false;
            }
            eventsToTriggerOnExit.Invoke();
        }
    }
}

public enum TriggerMode 
{ 
    TriggerOnce,
    AlwaysTrigger
}
