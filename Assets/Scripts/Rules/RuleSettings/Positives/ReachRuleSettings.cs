using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReachRuleSettings", menuName = "ScriptableObjects/RuleSettings/ReachRuleSettings", order = 26)]
public class ReachRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public ReachRuleSettings()
    {
        switch (durationType)
        {
            case DurationModType.RuleDependant:
                durationMod = durationModFormula;
                break;
            case DurationModType.FixedAmount:
                //Duration is already set in inspector
                break;
            default:
                throw new System.InvalidOperationException("Unknown durationModType: " + durationType);
        }
    }

    public override Rule GetRule()
    {
        ReachRule returnedRule = new ReachRule(AllRules.Reach, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
