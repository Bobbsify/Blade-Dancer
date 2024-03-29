﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* STRUTTURALA IN 3 COMPONENTI -- RuleContainerManager, UIRulesController e RuleManager
 * Rule Manager ha aggiornamenti sulle regole e li manda a UIRulesController, RuleeContainerManager si occupa della gestione della regola
 * al suo interno e UIRulesController li controlla e li smista
 */


public class UIRulesController : MonoBehaviour
{
    [Header("Animation")]

    [SerializeField]
    private List<Animator> ruleAnimators = new List<Animator>();

    [Header("Settings")]

    [SerializeField]
    private List<RuleContainerManager> ruleContainerManagers = new List<RuleContainerManager>();

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

    public void ResetContainers()
    {
        foreach (RuleContainerManager rcm in ruleContainerManagers) 
        {
            rcm.ResetContainer();
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
    private Sprite icon;
    private float score;
    private float maxScore;
    private bool isReverse;
    private bool completed;

    public RulePacket(AllRules rule,Sprite icon, float score, float maxScore, bool completed,bool reverse)
    {
        this.ruleName = rule;
        this.icon = icon;
        this.score = score;
        this.maxScore = maxScore;
        this.isReverse = reverse;
        this.completed = completed;
    }

    public AllRules GetName() { return this.ruleName; }
    public Sprite GetIcon() { return this.icon; }
    public float GetScore() { return this.score; }
    public float GetMaxScore() { return this.maxScore; }
    public bool GetCompleted() { return this.completed; }
    public bool IsReverse() { return this.isReverse; }
}