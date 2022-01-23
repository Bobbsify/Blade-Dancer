using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotTakeDamageRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotTakeDamageRuleSettings", order = 25)]
public class NotTakeDamageRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotTakeDamageRuleSettings()
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
        NotTakeDamageRule returnedRule = new NotTakeDamageRule(AllRules.NotTakeDamage, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
