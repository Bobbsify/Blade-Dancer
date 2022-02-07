using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerManager : MonoBehaviour, IGameEntity
{
    [SerializeField]
    [Tooltip("Automatically selects all child cheers as cheers")]
    private CheerController cheerController;

    [SerializeField]
    private List<string> possibleCheers = new List<string>();

    [SerializeField]
    private List<SoundPacket> cheerSounds = new List<SoundPacket>();

    private GameManager gameManager;

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
        gameManager.PlaySound(cheerSounds[Random.Range(0, cheerSounds.Count)]);
        cheerController.ExecuteCheer(possibleCheers[Random.Range(0, possibleCheers.Count)]);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
