using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("For entities that require to know of the Game Manager Object")]
    private GameObject GameEntitiesSearchRoot;

    [Header("Player Generation")]

	[SerializeField]
	private Transform inputReceiversTransform;

	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	private GameObject playerPrefab;

	public GameObject PlayerPawn { get; private set; }

    [Header("Room Generation")]

    [SerializeField]
    private AllRulesObject allRules;

    [SerializeField]
    private GameObject[] defaultRoomsPrefabs;

    [SerializeField]
    [Tooltip("Location of the rule manager in the scene")]
    private GameObject ruleManagerLocation;

    [Header("Rule Management")]

    [SerializeField]
    [Range(1, 3)]
    [Tooltip("Determines the amount of rules generated in a stage")]
    private int minDifficulty = 1;

    [SerializeField]
    [Range(4, 8)]
    [Tooltip("Determines the amount of rules generated in a stage")]
    private int maxDifficulty = 4;

    [SerializeField]
    [Range(0.1f, 1)]
    [Tooltip("Determines how much the difficulty is increased inbetween stages")]
    private float difficultyIncreaseMod = 0.15f;

    private RuleManager ruleManager;

    private StreakFactory streakFactory;

    private Streak currentStreak;

    private GameObject currentArena;

    private const float roomUnderminingValue = 10.0f;

    private void Awake()
	{
		this.GeneratePlayerPawn();
        streakFactory = new StreakFactory(defaultRoomsPrefabs, new RuleFactory(allRules));
	}

	private void Start()
    {
        IGameEntity[] entities = this.GameEntitiesSearchRoot.GetComponentsInChildren<IGameEntity>();
        for (int i = 0; i < entities.Length; i++)
        {
            var entity = entities[i];
            entity.Init(this);
        }

        ruleManager = this.GameEntitiesSearchRoot.GetComponentInChildren<RuleManager>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            GenerateNewStreak();
            Stage currentStage = currentStreak.GetCurrentStage();
            if (currentStage != null)
            {
                GoToNextStage(currentStage);
            }
            else
            {
                StreakEnded();
            }
        }
    }

    private void GoToNextStage(Stage currentStage)
    {
        ruleManager.SetNewRuleset(currentStage.GetRules());
        currentArena = Instantiate(currentStage.GetRoom(), RoomPosition(), Quaternion.identity);
    }

    private Vector3 RoomPosition()
    {
        Vector3 playerPos = PlayerPawn.transform.position;
        playerPos.y -= roomUnderminingValue;
        return playerPos;
    }

    public void ActionEventTrigger(Actions action)
    {
        ruleManager.ApplyRule(action);
    }

    public void KillPlayer()
    {
        PlayerPawn.GetComponent<PlayerController>().DoDeath();
        GenerateNewStreak();
    }

    public void EndOfStage()
    {
        Stage nextStage = currentStreak.NextStage();
        Destroy(currentArena);
        GoToNextStage(nextStage);
    }

    private void GenerateNewStreak()
    {
        int difficulty = Random.Range(minDifficulty, maxDifficulty);
        currentStreak = streakFactory.GetRandomStreak(difficulty, difficultyIncreaseMod);
    }

    private void GeneratePlayerPawn()
	{
		this.PlayerPawn = Instantiate(this.playerPrefab, this.spawnPoint.position, Quaternion.identity);
		this.PlayerPawn.transform.SetParent(this.inputReceiversTransform);
	}

    private void StreakEnded() { Debug.Log("End Of Streak"); }
    
}