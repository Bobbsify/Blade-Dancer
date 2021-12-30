using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotTrapRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotTrapRuleSettings", order = 23)]
public class NotTrapRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotTrapRuleSettings()
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
        NotTrapRule returnedRule = new NotTrapRule(AllRules.NotTrap, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
