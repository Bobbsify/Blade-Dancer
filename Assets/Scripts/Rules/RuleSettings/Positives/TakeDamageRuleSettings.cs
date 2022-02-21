using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TakeDamageRuleSettings", menuName = "ScriptableObjects/RuleSettings/TakeDamageRuleSettings", order = 24)]
public class TakeDamageRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public TakeDamageRuleSettings()
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
        TakeDamageRule returnedRule = new TakeDamageRule(AllRules.Danneggiati, ruleIcon, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
