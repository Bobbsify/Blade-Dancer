using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuleSetting : ScriptableObject
{
    [Header("Rule Duration Settings")]
    [SerializeField]
    protected DurationModType durationType;

    [SerializeField]
    protected float durationMod;
    
    [Header("Rule Functionality Tweaking")]
    [SerializeField]
    protected List<Actions> appliedActions; // actions that cause the rule to be completed

    [SerializeField]
    protected List<RuleObject> ruleRelatedObjects; // (Can Be Empty) eventual objects that are correlated to the rule

    [SerializeField]
    protected List<AllRules> mutuallyExclusives; // Rules this rule cannot be paired with

    public abstract Rule GetRule();
}
