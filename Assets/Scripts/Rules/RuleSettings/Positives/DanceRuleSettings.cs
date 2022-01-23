using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DanceRuleSettings", menuName = "ScriptableObjects/RuleSettings/DanceRuleSettings", order = 6)]
public class DanceRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public DanceRuleSettings()
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
        DanceRule returnedRule = new DanceRule(AllRules.Dance, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
