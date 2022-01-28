using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeftRuleSettings", menuName = "ScriptableObjects/RuleSettings/LeftRuleSettings", order = 10)]
public class LeftRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public LeftRuleSettings()
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
        LeftRule returnedRule = new LeftRule(AllRules.Sinistra, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
