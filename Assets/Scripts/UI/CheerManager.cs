using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Automatically selects all child cheers as cheers")]
    private CheerController cheerController;

    [SerializeField]
    private List<string> possibleCheers = new List<string>();

    private void OnValidate()
    {
        if (possibleCheers.Count == 0)
        {
            possibleCheers.Add("WOW");
            possibleCheers.Add("FANTASTICO");
            possibleCheers.Add("STRAVAGANTE");
            possibleCheers.Add("INCREDIBILE");
            possibleCheers.Add("AAAAAAAAAAAA");
        }

        if (cheerController == null)
        {
            cheerController = GetComponentInChildren<CheerController>();
        }
    }

    public void ExecuteCheer()
    {
        cheerController.ExecuteCheer(possibleCheers[Random.Range(0, possibleCheers.Count)]);
    }
}
