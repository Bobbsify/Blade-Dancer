using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotReachRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotReachRuleSettings", order = 27)]
public class NotReachRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotReachRuleSettings()
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
        NotReachRule returnedRule = new NotReachRule(AllRules.NotReach, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
