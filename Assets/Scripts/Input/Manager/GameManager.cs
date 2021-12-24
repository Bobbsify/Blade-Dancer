using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject searchRoot;

	[SerializeField]
	private Transform inputReceiversTransform;

	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	private GameObject playerPrefab;

	public GameObject PlayerPawn { get; private set; }

	private void Awake()
	{
		this.GeneratePlayerPawn();
	}

	private void Start()
	{
		var entities = this.searchRoot.GetComponentsInChildren<IGameEntity>();
		for (int i = 0; i < entities.Length; i++)
		{
			var entity = entities[i];
			entity.Init(this);
		}
	}

	private void GeneratePlayerPawn()
	{
		this.PlayerPawn = GameObject.Instantiate(this.playerPrefab, this.spawnPoint.position, Quaternion.identity);
		this.PlayerPawn.transform.SetParent(this.inputReceiversTransform);
	}

    public void KillPlayer()
    {
        PlayerPawn.GetComponent<Animator>().SetTrigger("die");
    }
}