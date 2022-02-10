using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakRuleSettings", menuName = "ScriptableObjects/RuleSettings/BreakRuleSettings", order = 14)]
public class BreakRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public BreakRuleSettings()
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
        BreakRule returnedRule = new BreakRule(AllRules.Rompi, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
