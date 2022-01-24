using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Input Manager of the game")]
    private InputManager inputManager;

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

    private PlayerController playerCtrl;

    [Header("Room Generation")]

    [SerializeField]
    private GameObject stagesRoot;

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
    private bool tookDamage = false;

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
            InitEntities(root);
        }
        ruleManager = this.ruleManagerLocation.GetComponentInChildren<RuleManager>();
    }

    public List<RuleSetting> GetRuleSettings()
    {
        return this.allRules.getAll();
    }

    public void ActionEventTrigger(Actions action)
    {
        if (currentStreak != null)
        {
            if (action.Equals(Actions.TakeDamage) && ruleManager.IsCurrentlyRule(AllRules.TakeDamage) && !tookDamage)
            {
                tookDamage = true;
                playerCtrl.TakeDamage(-1); //Heal player on first instance of damage
            }
            ruleManager.ApplyRule(action);
        }
    }
    public void KillPlayer()
    {
        playerCtrl.TakeDamage(playerCtrl.GetMaxHealth());
        GenerateNewStreak();
    }

    #region StreakManagement

    public void StartStreak()
    {
        this.inputManager.DisableInput<InputSystemPause>();
        Destroy(startingRoom);
        GenerateNewStreak();
        Stage currentStage = currentStreak.GetCurrentStage();
        GoToNextStage(currentStage);
    }

    public void StartStage()
    {
        tookDamage = false;
        timer.StartTimer();
    }

    public void EndOfStage()
    {
        playerCtrl.GetComponent<Dance>().Charge(GetDanceCharge());
        timer.StopTimer();
        Stage nextStage = currentStreak.NextStage();
        if (nextStage != null)
        {
            Destroy(currentArena);
            GoToNextStage(nextStage);
            playerCtrl.DisableAllAbilities();
        }
        else
        {
            timer.ResetTimer();
            StreakEnded();
        }
    }

    private int GetDanceCharge()
    {
        return timer.GetCheer();
    }

    public void doReset()
    {
        timer.ResetTimer();
        bool fr = firstRun; //If first run repeat first run instead of randomizing (In order to complete tutorial)
        currentBreakroom--; //Don't proceed to next brakeroom
        StreakEnded();
        firstRun = fr;
        playerCtrl.EnableAllAbilities();
        ruleManager.SetNewRuleset(new List<Rule>()); //Empty rules
    }

    private void GoToNextStage(Stage currentStage)
    {
        ruleManager.SetNewRuleset(currentStage.GetRules());
        timer.SetTimer(currentStage.GetRulesTime());
        currentArena = Instantiate(currentStage.GetRoom(), RoomPosition(), Quaternion.identity, stagesRoot.transform);
        RoomController room = currentArena.GetComponent<RoomController>();
        foreach (RuleObject objToSpawn in currentStage.GetRuleRelatedObjectsToSpawn()) 
        {
            Instantiate(objToSpawn.GetRuleObj(), room.GetPos(objToSpawn.GetPositionType()), Quaternion.identity, currentArena.transform);
        }
        InitEntities(currentArena);
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
            int difficulty = fixedDifficulty == 0 ? UnityEngine.Random.Range(minDifficulty, maxDifficulty) : Mathf.Max(Mathf.Min(fixedDifficulty, maxDifficulty), minDifficulty); //If difficulty is preset make sure it is between the two max values
            currentStreak = streakFactory.GetRandomStreak(difficulty, difficultyIncreaseMod);
        }
    }

    private void StreakEnded()
    {
        //End Streak
        Destroy(currentArena);
        firstRun = false;
        currentStreak = null;

        //Create Break room
        currentBreakroom++;
        GameObject breakoutRoom = Instantiate(breakRooms[currentBreakroom], RoomPosition(), Quaternion.identity, stagesRoot.transform);
        InitEntities(breakoutRoom);
        startingRoom = breakoutRoom;

        playerCtrl.TakeDamage(-playerCtrl.GetMaxHealth());
        this.inputManager.EnableInput<InputSystemPause>();
    }

    #endregion

    private void InitEntities(GameObject obj)
    {
        foreach (IGameEntity entity in obj.GetComponentsInChildren<IGameEntity>())
        {
            entity.Init(this);
        }
    }


    private void GeneratePlayerPawn()
	{
		this.PlayerPawn = Instantiate(this.playerPrefab, this.spawnPoint.position, Quaternion.identity);
		this.PlayerPawn.transform.SetParent(this.inputReceiversTransform);
        playerCtrl = PlayerPawn.GetComponent<PlayerController>();
	}
    
}