using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RightRuleSettings", menuName = "ScriptableObjects/RuleSettings/RightRuleSettings", order = 12)]
public class RightRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public RightRuleSettings()
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
        RightRule returnedRule = new RightRule(AllRules.Destra, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
