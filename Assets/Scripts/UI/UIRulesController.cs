using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* STRUTTURALA IN 3 COMPONENTI -- RuleContainerManager, UIRulesController e RuleManager
 * Rule Manager ha aggiornamenti sulle regole e li manda a UIRulesController, RuleeContainerManager si occupa della gestione della regola
 * al suo interno e UIRulesController li controlla e li smista
 */

/*treMezzi,    2160 * 1440     x larghezza y altezza
  cinqueQuarti,  1280 * 1024
  quattroTerzi,  1024 * 768
  sediciNoni,   1920 * 1080      
  sediciDecimi,  1920 * 1200    
  HD             2560 * 1080*/


public class UIRulesController : MonoBehaviour
{
    [Header("Animation")]

    [SerializeField]
    private List<Animator> ruleAnimators = new List<Animator>();

    [Header("Settings")]

    [SerializeField]
    private float oddRulesOffsetTreMezzi = 125.0f;

    [SerializeField]
    private float oddRulesOffsetCinqueQuarti = 125.0f;

    [SerializeField]
    private float oddRulesOffsetQuattroTerzi = 125.0f;

    [SerializeField]
    private float oddRulesOffsetSediciNoni= 125.0f;

    [SerializeField]
    private float oddRulesOffsetSediciDecimi = 125.0f;

    [SerializeField]
    private float oddRulesOffsetHD = 125.0f;

    [SerializeField]
    private List<RuleContainerManager> ruleContainerManagers = new List<RuleContainerManager>();

    private Resolution screenResolution;
    private Vector3 defaultPos;
    private Vector3 offsettedPos;

    private void OnValidate()
    {
        if (ruleContainerManagers.Count == 0) 
        { 
            ruleContainerManagers.AddRange(GetComponentsInChildren<RuleContainerManager>());
        }
        if (ruleAnimators.Count == 0) 
        {
            ruleAnimators.AddRange(GetComponentsInChildren<Animator>());
        }
    }
    private void Awake()
    {
        defaultPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        Screen.SetResolution(1280, 1024, false);
        screenResolution = Screen.currentResolution;
    }

    private void Start()
    {

        if (screenResolution.width == 2160 && screenResolution.height == 1440)
        {
            offsettedPos = new Vector3(defaultPos.x + oddRulesOffsetTreMezzi, defaultPos.y, defaultPos.z);
        }

        else if(screenResolution.width == 1280 && screenResolution.height == 1024)
        {
            offsettedPos = new Vector3(defaultPos.x + oddRulesOffsetCinqueQuarti, defaultPos.y, defaultPos.z);
        }
        else if (screenResolution.width == 1024 && screenResolution.height == 768)
        {
            offsettedPos = new Vector3(defaultPos.x + oddRulesOffsetQuattroTerzi, defaultPos.y, defaultPos.z);
        }
        else if (screenResolution.width ==  1920 && screenResolution.height == 1080)
        {
            offsettedPos = new Vector3(defaultPos.x + oddRulesOffsetSediciNoni, defaultPos.y, defaultPos.z);
        }

        else if (screenResolution.width == 1920 && screenResolution.height == 1200)
        {
            offsettedPos = new Vector3(defaultPos.x + oddRulesOffsetSediciDecimi, defaultPos.y, defaultPos.z);
        }

        else if (screenResolution.width == 2560 && screenResolution.height == 1080)
        {
            offsettedPos = new Vector3(defaultPos.x + oddRulesOffsetHD, defaultPos.y, defaultPos.z);
        }
    }

    public void ResetContainers()
    {
        foreach (RuleContainerManager rcm in ruleContainerManagers) 
        {
            rcm.ResetContainer();
        }
    }

    public void SetupRules(RulePacket[] packets) 
    {
        //Setup position
        if (packets.Length % 2 == 0)
        {
            this.transform.position = defaultPos;
        }
        else 
        {
            this.transform.position = offsettedPos;
        }

        for (int i = 0; i < packets.Length; i++) 
        {
            ruleContainerManagers[i].SetupForRule(packets[i]);
        }
    }

    public void SetupRules(List<RulePacket> packets)
    {
        //Setup position
        if (packets.Count % 2 == 0)
        {
            this.transform.position = defaultPos;
        }
        else
        {
            this.transform.position = offsettedPos;
        }

        for (int i = 0; i < packets.Count; i++) 
        {
            ruleContainerManagers[i].SetupForRule(packets[i]);
        }
    }

    public void StageCompleted() 
    {
        foreach (RuleContainerManager manager in ruleContainerManagers) 
        {
            manager.Reset();
        }
    }

    public void GetUpdate(RulePacket[] packets)
    {
        foreach (RulePacket packet in packets)
        {
            foreach (RuleContainerManager manager in ruleContainerManagers)
            {
                if (packet.GetName() == manager.GetCurrentRule())
                {
                    manager.SendPacket(packet);
                    break;
                }
            }
        }
    }

    public void GetUpdate(List<RulePacket> packets)
    {
        foreach (RulePacket packet in packets)
        {
            foreach (RuleContainerManager manager in ruleContainerManagers)
            {
                if (packet.GetName() == manager.GetCurrentRule())
                {
                    manager.SendPacket(packet);
                    break;
                }
            }
        }
    }

    public List<RuleContainerManager> GetContainers()
    {
        return this.ruleContainerManagers;
    }

    public void ShowRules() 
    {
        foreach (Animator anim in ruleAnimators) 
        {
            anim.SetTrigger("showrule");
        }
    }
}


public class RulePacket
{
    private AllRules ruleName;
    private float score;
    private float maxScore;
    private bool isReverse;
    private bool completed;

    public RulePacket(AllRules rule, float score, float maxScore, bool completed,bool reverse)
    {
        this.ruleName = rule;
        this.score = score;
        this.maxScore = maxScore;
        this.isReverse = reverse;
        this.completed = completed;
    }

    public AllRules GetName() { return this.ruleName; }
    public float GetScore() { return this.score; }
    public float GetMaxScore() { return this.maxScore; }
    public bool GetCompleted() { return this.completed; }
    public bool IsReverse() { return this.isReverse; }
}