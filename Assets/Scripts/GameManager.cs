using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("For entities that require to know of the Game Manager Object")]
    private GameObject[] GameEntitiesRoots;

    [Header("Player Generation")]

	[SerializeField]
	private Transform inputReceiversTransform;

    [SerializeField]
    private Transform projectilesRoot;

	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	private GameObject playerPrefab;

	public GameObject PlayerPawn { get; private set; }

    [Header("Room Generation")]

    [SerializeField]
    private GameObject startingRoom;

    [SerializeField]
    private RuleSettingsContainer allRules;

    [SerializeField]
    private GameObject[] defaultRoomsPrefabs;

    [SerializeField]
    [Tooltip("Location of the rule manager in the scene")]
    private GameObject ruleManagerLocation;

    [SerializeField]
    [Tooltip("The amount of units the next room is generated below the character")]
    private const float roomUnderminingValue = 10.0f;

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

    [Header("Break Rooms")]

    [SerializeField]
    private List<GameObject> breakRooms;

    [SerializeField]
    private int currentBreakroom = 0;

    //--------------------------------------------------

    private RuleManager ruleManager;

    private StreakFactory streakFactory;

    private Streak currentStreak;

    private TimerManager timer;

    private GameObject currentArena;

    private bool firstRun = true;

    private void Awake()
	{
        TryGetComponent(out timer);
		this.GeneratePlayerPawn();
        PlayerPawn.GetComponent<Shoot>().SetProjectilesRoot(this.projectilesRoot);
        streakFactory = new StreakFactory(defaultRoomsPrefabs, new RuleFactory(allRules.getAll()), this);
	}

	private void Start()
    {
        foreach (GameObject root in this.GameEntitiesRoots) {
            IGameEntity[] entitiesToCall = root.GetComponentsInChildren<IGameEntity>();
            foreach (IGameEntity entity in entitiesToCall)
            {
                entity.Init(this);
            }
        }
        ruleManager = this.ruleManagerLocation.GetComponentInChildren<RuleManager>();
    }

    public List<RuleSetting> GetRuleSettings()
    {
        return this.allRules.getAll();
    }

    public void ActionEventTrigger(Actions action)
    {
        Debug.Log(action);
        if (currentStreak != null)
        {
            ruleManager.ApplyRule(action);
        }
    }

    #region StreakManagement

    public void StartStreak()
    {
        Destroy(startingRoom);
        GenerateNewStreak();
        Stage currentStage = currentStreak.GetCurrentStage();
        GoToNextStage(currentStage);
    }

    public void EndOfStage()
    {
        Stage nextStage = currentStreak.NextStage();
        if (nextStage != null)
        {
            Destroy(currentArena);
            GoToNextStage(nextStage);
            PlayerPawn.GetComponent<PlayerController>().DisableAllAbilities();
        }
        else
        {
            timer.StopTimer();
            StreakEnded();
        }
    }

    private void GoToNextStage(Stage currentStage)
    {
        ruleManager.SetNewRuleset(currentStage.GetRules());
        timer.SetTimer(currentStage.GetRulesTime());
        currentArena = Instantiate(currentStage.GetRoom(), RoomPosition(), Quaternion.identity);
        RoomController room = currentArena.GetComponent<RoomController>();
        foreach (RuleObject objToSpawn in currentStage.GetRuleRelatedObjectsToSpawn()) 
        {
            Instantiate(objToSpawn.GetRuleObj(), room.GetPos(objToSpawn.GetPositionType()), Quaternion.identity, currentArena.transform);
        }
    }

    private Vector3 RoomPosition()
    {
        Vector3 playerPos = PlayerPawn.transform.position;
        playerPos.y -= roomUnderminingValue;
        return playerPos;
    }

    private void GenerateNewStreak(int fixedDifficulty = 0)
    {
        if (firstRun)
        {
            currentStreak = streakFactory.GetTutorialStreak();
        }
        else { 
            int difficulty = fixedDifficulty == 0 ? Random.Range(minDifficulty, maxDifficulty) : Mathf.Max(Mathf.Min(fixedDifficulty, maxDifficulty), minDifficulty); //If difficulty is preset make sure it is between the two max values
            currentStreak = streakFactory.GetRandomStreak(difficulty, difficultyIncreaseMod);
        }
    }

    private void StreakEnded()
    {
        Debug.Log("End Of Streak");
        currentBreakroom++;
        firstRun = false;
        Instantiate(defaultRoomsPrefabs[0], RoomPosition(), Quaternion.identity); // temporary
        //Instantiate(breakRooms[currentBreakroom], RoomPosition(), Quaternion.identity); actual
    }

    #endregion

    public void KillPlayer()
    {
        PlayerPawn.GetComponent<PlayerController>().TakeDamage(500);
        GenerateNewStreak();
    }


    private void GeneratePlayerPawn()
	{
		this.PlayerPawn = Instantiate(this.playerPrefab, this.spawnPoint.position, Quaternion.identity);
		this.PlayerPawn.transform.SetParent(this.inputReceiversTransform);
	}
    
}