﻿using System;
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
    private float oddRulesOffset = 125.0f;

    [SerializeField]
    private List<RuleContainerManager> ruleContainerManagers = new List<RuleContainerManager>();

    private Vector3 defaultPos;
    private Vector3 offsettedPos;

    private void OnValidate()
    {
        if (ruleContainerManagers.Count == 0) 
        { 
            ruleContainerManagers.AddRange(GetComponentsInChildren<RuleContainerManager>());
        }
    }
    private void Awake()
    {
        defaultPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        offsettedPos = new Vector3(defaultPos.x + oddRulesOffset, defaultPos.y, defaultPos.z);
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