using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotDanceRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotDanceRuleSettings", order = 7)]
public class NotDanceRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;
    
    public NotDanceRuleSettings()
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
        NotDanceRule returnedRule = new NotDanceRule(AllRules.NotBalla,durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
