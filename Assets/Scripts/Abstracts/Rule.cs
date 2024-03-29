﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Rule : IRule
{
    protected AllRules RuleName { get; set; }
    
    private DurationModType DurationModType { get; set; }
    
    private float durationModifier; // modificatore che definisce la quantità in secondi di cui deve aumentare la durata in presenza di questa regola

    [SerializeField]
    protected Sprite RuleIcon { get; set; }

    [Header("Rule Functionality Tweaking")]
    [SerializeField]
    protected List<Actions> appliedActions = new List<Actions>(); // actions that cause the rule to be completed

    [SerializeField]
    protected List<RuleObject> ruleRelatedObjects = new List<RuleObject>(); // (Can Be Empty) eventual objects that are correlated to the rule

    [SerializeField]
    protected List<AllRules> mutuallyExclusives = new List<AllRules>(); // Other rules which can't be paired with this one

    public virtual void SetDurationMod(float amount)
    {
        this.durationModifier = amount;
    }

    public virtual bool IsMutuallyExclusive(AllRules r)
    {
        foreach (AllRules rule in mutuallyExclusives)
        {
            if (rule == r)
            {
                return true;
            }
        }
        return false;
    }

    public virtual RulePacket ToPacket()
    {
        return new RulePacket(this.RuleName, RuleIcon, IsReverse() ? (IsRuleComplete() ? 0 : 1) : (IsRuleComplete() ? 1 : 0), 1, this.IsRuleComplete(),IsReverse());
    }

    public virtual List<RuleObject> GetRuleRelatedObjects()
    {
        return this.ruleRelatedObjects;
    }

    public virtual bool IsReverse()
    {
        return this.RuleName.ToString().ToLower().Contains("not");
    }

    public virtual AllRules GetRuleName()
    {
        return this.RuleName;
    }

    public virtual Sprite GetRuleIcon()
    {
        return this.RuleIcon;
    }

    public virtual AllRules GetPureRuleName()
    {
        return (AllRules)((int)this.RuleName - (Enum.GetValues(typeof(AllRules)).Length / 2));
    }

    public virtual List<AllRules> GetMutallyExclusives()
    {
        return this.mutuallyExclusives;
    }

    public virtual float GetDuration()
    {
        return this.durationModifier;
    }

    public virtual bool CheckAction(Actions executedAction)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsRuleComplete()
    {
        throw new System.NotImplementedException();
    }

}

[System.Serializable]
public class RuleObject
{
    [SerializeField]
    private string name;

    [SerializeField]
    private GameObject ruleObjPrefab;

    [SerializeField]
    private PositionType pos;

    public RuleObject(string name, GameObject ruleObjPrefab, PositionType pos)
    {
        this.name = name;
        this.ruleObjPrefab = ruleObjPrefab;
        this.pos = pos;
    }

    public string GetName()
    {
        return this.name;
    }

    public GameObject GetRuleObj()
    {
        return this.ruleObjPrefab;
    }

    public PositionType GetPositionType()
    {
        return this.pos;
    }
}

// 0 Any Random
// 1, 2, 3 Limited Random
// 4, 5, 6... Fixed Positions
public enum PositionType
{
    Random = 0,
    RandomNoCorner,
    AnyCorner,
    AnyLeft,
    AnyRight,
    TopLeftCorner,
    TopCenter,
    TopRightCorner,
    CenterLeft,
    Center,
    CenterRight,
    BotLeftCorner,
    BotCenter,
    BotRightCorner,
}

//Defines wether or not the amount written in the "Duration Modifier" Box affects the duration of the rule or if it is calculated by the rule itself
public enum DurationModType
{
    RuleDependant,
    FixedAmount
}

public enum Actions
{
    Move,
    Shoot,
    Dash,
    Dance,
    Ring,
    Left,
    Right,
    Break,
    Gather,
    Kill,
    Score,
    Trap,
    TakeDamage,
    Reach
}

public enum AllRules
{
    Muoviti,
    Spara,
    Scatta,
    Balla,
    Suona,
    Sinistra,
    Destra,
    Rompi,
    Raccogli,
    Uccidi,
    Segna,
    Cattura,
    Danneggiati,
    Raggiungi,
    //Negatives
    NotMuoviti,
    NotSpara,
    NotScatta,
    NotBalla,
    NotSuona,
    NotSinistra,
    NotDestra,
    NotRompi,
    NotRaccogli,
    NotUccidi,
    NotSegna,
    NotCattura,
    NotDanneggiati,
    NotRaggiungi
}
