using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Automatically selects all child cheers as cheers")]
    private List<CheerController> cheers = new List<CheerController>();

    private void OnValidate()
    {
        if (cheers.Count == 0)
        {
            cheers.AddRange(GetComponentsInChildren<CheerController>());
        }
    }

    public void ExecuteCheer()
    {
        int randCheerController = Random.Range(0, cheers.Count);
        cheers[randCheerController].ExecuteCheer();
    }
}
