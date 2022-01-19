using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* STRUTTURALA IN 3 COMPONENTI -- RuleContainerManager, UIRulesController e RuleManager
 * Rule Manager ha aggiornamenti sulle regole e li manda a UIRulesController, RuleeContainerManager si occupa della gestione della regola
 * al suo interno e UIRulesController li controlla e li smista
 */
public class UIRulesController : MonoBehaviour
{
    [SerializeField]
    private List<RuleContainerManager> ruleContainerManagers = new List<RuleContainerManager>();

    private void OnValidate()
    {
        if (ruleContainerManagers.Count == 0) 
        { 
            ruleContainerManagers.AddRange(GetComponentsInChildren<RuleContainerManager>());
        }
    }

    public void SetupRules(RulePacket[] packets) 
    {
        for (int i = 0; i < packets.Length; i++) 
        {
            ruleContainerManagers[i].SetupForRule(packets[i]);
        }
    }

    public void SetupRules(List<RulePacket> packets) 
    {
        for(int i = 0; i < packets.Count; i++) 
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
}


public class RulePacket
{
    private AllRules ruleName;
    private string score;
    private bool isReverse;
    private bool completed;

    public RulePacket(AllRules rule, string score, bool completed,bool reverse)
    {
        this.ruleName = rule;
        this.score = score;
        this.isReverse = reverse;
        this.completed = completed;
    }

    public AllRules GetName() { return this.ruleName; }
    public string GetScore() { return this.score; }
    public bool GetCompleted() { return this.completed; }
    public bool IsReverse() { return this.isReverse; }
}