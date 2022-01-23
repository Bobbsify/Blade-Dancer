using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotRightRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotRightRuleSettings", order = 13)]
public class NotRightRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotRightRuleSettings()
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
        NotRightRule returnedRule = new NotRightRule(AllRules.NotRight, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
