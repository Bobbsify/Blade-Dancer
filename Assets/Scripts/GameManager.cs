﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TimerManager))]
[RequireComponent(typeof(SoundQueueManager))]
[RequireComponent(typeof(StreakMusicSelector))]
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

    [SerializeField]
    private SoundPacket playerFall;

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

    [Header("Difficulty Settings")]

    [SerializeField]
    [Range(1, 3)]
    [Tooltip("Determines the amount of rules generated in a stage")]
    private int minDifficulty = 1;

    [SerializeField]
    [Range(4, 8)]
    [Tooltip("Determines the amount of rules generated in a stage")]
    private int maxDifficulty = 6;

    [SerializeField]
    [Range(0.1f, 1)]
    [Tooltip("Determines how much the difficulty is increased inbetween stages")]
    private float difficultyIncreaseMod = 0.15f;

    [Header("Break Rooms")]

    [SerializeField]
    private List<GameObject> breakRooms;

    [SerializeField]
    private int currentBreakroom = 0;

    [SerializeField]
    private SoundPacket breakRoomMusic;

    [Header("UI")]
    [SerializeField]
    private Image fadeToBlack;

    [SerializeField]
    [Range(0.01f,0.1f)]
    private float fadeAmount = 0.01f;

    [SerializeField]
    private Animator HUDAnimator;

    [SerializeField]
    private CheerManager cheerManager;

    [SerializeField]
    private HealthController healthController;

    [SerializeField]
    private GameObject pauseIcon;

    [Header("Camera")]

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private float zCameraOffset = 0.8f;

    [Header("Debug")]
    [SerializeField]
    [Tooltip("Never leaves the first break room")]
    private bool endless = false;

    [SerializeField]
    private bool firstRun = true;

    [SerializeField]
    private bool customRunEnabled = true;

    [SerializeField]
    private int streaksCompleted = 1;

    [SerializeField]
    [Tooltip("Specificare le regole che usciranno durante i prossimi 10 stage")]
    private AllRules[] customRun = new AllRules[6];

    //--------------------------------------------------

    //Streaking

    private RuleManager ruleManager;

    private StreakFactory streakFactory;

    private Streak currentStreak;

    private Stage nextStage;

    private TimerManager timer;

    private GameObject currentArena;

    //Sound

    private SoundQueueManager sqm;

    private SoundPacket streakMusic;

    private StreakMusicSelector streakMusicSelector;


    //Other

    private bool tookDamage = false;

    private bool playerIsBeingKilled = false;

    private void OnValidate()
    {
        if (cheerManager == null) 
        {
            cheerManager = GetUIComponent<CheerManager>();
        }
        if (healthController == null) 
        {
            healthController = GetUIComponent<HealthController>();
        }
        if(HUDAnimator == null) 
        {
            GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Animator>();
        }
    }

    private void Awake()
	{
        if (!firstRun) 
        {
            Debug.LogWarning("Game Manager has first run set to false upon start");
        }
        TryGetComponent(out streakMusicSelector);
        TryGetComponent(out sqm);
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
        currentArena = startingRoom;
        PlaySound(breakRoomMusic);
        HUDAnimator.SetBool("active", false);
    }

    public T GetUIComponent<T>()
    {
        return GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<T>(true);
    }

    public List<RuleSetting> GetRuleSettings()
    {
        return this.allRules.getAll();
    }

    public void PlayerDamageTrigger()
    {
        healthController.UpdateHearts();
    }

    public void ActionEventTrigger(Actions action)
    {
        if (currentStreak != null && (timer.IsGoing() || timer.IsDanceStopped()))
        {
            ruleManager.ApplyRule(action);
        }
    }

    public void PlaySound(SoundPacket packet, bool fade = false)
    {
        sqm.AddSound(packet, fade);
    }

    public void StopSound(SoundPacket packet, bool fade = false) 
    {
        sqm.RemoveSound(packet, fade);
    }

    public void KillPlayer()
    {
        if (!playerIsBeingKilled) {
            playerIsBeingKilled = true;
            playerCtrl.TakeDamage(playerCtrl.GetMaxHealth());
            ruleManager.SetNewRuleset(new List<Rule>());
            GenerateNewStreak();
        }
    }

    public void ShakeCamera()
    {
        cameraController.StartShake();
    }

    #region StreakManagement

    public void StartStreak()
    {
        PlaySound(playerFall);
        StopSound(breakRoomMusic);
        //Remove HUD
        HUDAnimator.SetBool("active", false);

        //Select music for streak
        streakMusic = streakMusicSelector.GetMusic();
        PlaySound(streakMusic, true);

        //Pause disable
        this.inputManager.DisablePause();
        this.pauseIcon.SetActive(true);

        //Destroy(startingRoom);
        GenerateNewStreak();
        nextStage = currentStreak.GetCurrentStage();

        //Do animation
        playerCtrl.ToggleShooting(true);
        playerCtrl.DisableAllAbilities();
        currentArena.GetComponent<RoomController>().DoEndOfStage();

        /*Stage currentStage = currentStreak.GetCurrentStage();
        GoToNextStage(currentStage);*/
    }

    public void StartStage()
    {
        //Enable HUD
        HUDAnimator.SetBool("active", true);

        playerCtrl.EnableAllAbilities();
        EnableEnemies();
        tookDamage = false;
        timer.StartTimer();
    }

    public void EndOfStage()
    {
        DisableEnemies();

        //Remove HUD
        HUDAnimator.SetBool("active", false);
        PlaySound(playerFall);

        RemoveProjectiles();
        playerCtrl.GetComponent<Rigidbody>().Sleep();
        playerCtrl.GetComponent<Dance>().Charge(GetDanceCharge());
        timer.StopTimer();
        if (currentStreak != null) 
        { 
            nextStage = currentStreak.NextStage();
        }

        if (nextStage != null)
        {
            playerCtrl.DisableAllAbilities();
            currentArena.GetComponent<RoomController>().DoEndOfStage();
        }
        else
        {
            timer.ResetTimer();
            currentBreakroom -= endless ? 1 : 0;
            StreakEnded();
            
        }
    }

    public void PlayerHasFallen()
    {
        if (fadeToBlack.color.a == 0)
        {
            StartCoroutine(FadeToBlack());
        }
        else
        {
            Destroy(currentArena);
            playerCtrl.Animate("fall"); //remove fall trigger
            GoToNextStage(nextStage);
        }
    }

    public void PlayerLanded()
    {
        StartStage();
    }

    private void RemoveProjectiles()
    {
        foreach (Transform t in projectilesRoot) 
        {
            Destroy(t.gameObject);
        }
    }

    private void EnableEnemies()
    {
        IEnemy[] enemies = currentArena.GetComponentsInChildren<IEnemy>();
        foreach (IEnemy enemy in enemies)
        {
            enemy.Go();
        }
        foreach (EnemyController enemy in currentArena.GetComponentsInChildren<EnemyController>(true)) 
        {
            enemy.SetProjectilesRoot(projectilesRoot);
        }
    }

    private void DisableEnemies()
    {
        IEnemy[] enemies = currentArena.GetComponentsInChildren<IEnemy>();
        foreach (IEnemy enemy in enemies)
        {
            enemy.Stop();
        }
    }

    private int GetDanceCharge()
    {
        int cheer = timer.GetCheer();
        if (cheer > 1) 
        {
            cheerManager.ExecuteCheer();
        }
        return cheer;
    }

    public void doReset()
    {
        timer.ResetTimer();
        bool fr = firstRun; //If first run repeat first run instead of randomizing (In order to complete tutorial)
        currentBreakroom--; //Don't proceed to next brakeroom
        streaksCompleted--; //Do not increase difficulty
        StreakEnded();
        firstRun = fr;
        playerCtrl.EnableAllAbilities();
        playerIsBeingKilled = false;
        ruleManager.SetNewRuleset(new List<Rule>()); //Empty rules
        RemoveProjectiles();
    }

    private void GoToNextStage(Stage stage)
    {
        if (playerCtrl.GetHealth() > 0) { 
            timer.SetTimer(stage.GetRulesTime());
            currentArena = Instantiate(stage.GetRoom(), RoomPosition(), Quaternion.identity, stagesRoot.transform);
            RoomController room = currentArena.GetComponent<RoomController>();
        
            //Create Rule Objects

            Dictionary<Vector3, Vector3> spacesOccupied = new Dictionary<Vector3, Vector3>(); //Position --> Collider width
            foreach (RuleObject objToSpawn in stage.GetRuleRelatedObjectsToSpawn()) 
            {
                GameObject instantiated = Instantiate(objToSpawn.GetRuleObj(), room.GetPos(objToSpawn.GetPositionType(),spacesOccupied), Quaternion.identity, currentArena.transform);
                Collider col = instantiated.GetComponentInChildren<Collider>();
                //spacesOccupied.Add(instantiated.transform.position,
                //col == null  || col.isTrigger ? new Vector3(1, 1, 1) : col.bounds.extents);
                spacesOccupied.Add(instantiated.transform.position, new Vector3(1.5f, 1.5f, 1.5f));
            }

            //Setup new Arena
            ruleManager.SetNewRuleset(stage.GetRules());
            if (ruleManager.IsCurrentlyRule(AllRules.Danneggiati)) 
            {
                playerCtrl.DivineShield();
            }
            InitEntities(currentArena);
            PlayerPawn.transform.position -= new Vector3(0, roomUnderminingValue, 0);

            //Screen Go to Next Arena
            HUDAnimator.SetBool("active", true);
            ResetCamera();
            StartCoroutine(ReverseFade());
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
        else if (customRunEnabled) 
        {
            currentStreak = streakFactory.GetCustomStreak(customRun);
        }
        else
        {
            minDifficulty = 1 + (streaksCompleted / 3);
            maxDifficulty = minDifficulty + (streaksCompleted / minDifficulty);
            int difficulty = fixedDifficulty == 0 ? 
                UnityEngine.Random.Range(minDifficulty, Mathf.Min(streaksCompleted,maxDifficulty)) 
                : Mathf.Max(Mathf.Min(fixedDifficulty, maxDifficulty), minDifficulty); //If difficulty is preset make sure it is between the two max values
            currentStreak = streakFactory.GetRandomStreak(difficulty, difficultyIncreaseMod);
        }
    }

    private void StreakEnded()
    {
        //UI and Sound
        HUDAnimator.SetBool("active", false);
        PlaySound(breakRoomMusic, true);
        StopSound(streakMusic, true);

        //Pause enable
        this.inputManager.EnablePause();
        this.pauseIcon.SetActive(false);

        //End Streak
        Destroy(currentArena);
        firstRun = false;
        currentStreak = null;

        //Create Break room
        currentBreakroom++;
        GameObject breakoutRoom = Instantiate(breakRooms[currentBreakroom], RoomPosition(), Quaternion.identity, stagesRoot.transform);
        InitEntities(breakoutRoom);
        startingRoom = breakoutRoom;
        currentArena = breakoutRoom;
        PlayerPawn.transform.position -= new Vector3(0, roomUnderminingValue, 0);

        streaksCompleted++;
        playerCtrl.TakeDamage(-playerCtrl.GetMaxHealth());
        playerCtrl.ToggleShooting(false);
        RemoveProjectiles();
        PlayerDamageTrigger();
        ResetCamera();
    }

    #endregion

    private void InitEntities(GameObject obj)
    {
        foreach (IGameEntity entity in obj.GetComponentsInChildren<IGameEntity>())
        {
            entity.Init(this);
        }
    }

    private IEnumerator FadeToBlack()
    {
        Color tempColor = fadeToBlack.color;
        tempColor.a += fadeAmount;
        tempColor.a = Mathf.Min(tempColor.a, 1);
        fadeToBlack.color = tempColor;
        if (fadeToBlack.color.a != 1)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(FadeToBlack());
        }
        else 
        {
            PlayerHasFallen();
        }
    }

    private IEnumerator ReverseFade()
    {
        Color tempColor = fadeToBlack.color;
        tempColor.a -= fadeAmount;
        tempColor.a = Mathf.Max(tempColor.a, 0);
        fadeToBlack.color = tempColor;
        if (fadeToBlack.color.a != 0)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(ReverseFade());
        }
    }

    private void GeneratePlayerPawn()
	{
		this.PlayerPawn = Instantiate(this.playerPrefab, this.spawnPoint.position, Quaternion.identity);
		this.PlayerPawn.transform.SetParent(this.inputReceiversTransform);
        playerCtrl = PlayerPawn.GetComponent<PlayerController>();
	}

    private void ResetCamera() 
    {
        cameraController.transform.position = new Vector3(PlayerPawn.transform.position.x, PlayerPawn.transform.position.y + 10, PlayerPawn.transform.position.z + zCameraOffset); 
    }

    internal void AskForTimerStart()
    {
        if (!timer.IsGoing() && currentStreak != null) 
        { 
            timer.StartTimer(true);
        }
    }

    internal void AskForTimerStop()
    {
        if (timer.IsGoing() && currentStreak != null)
        {
            timer.StopTimer(true);
        }
    }


}