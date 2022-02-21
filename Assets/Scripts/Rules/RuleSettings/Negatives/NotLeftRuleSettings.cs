using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotLeftRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotLeftRuleSettings", order = 11)]
public class NotLeftRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotLeftRuleSettings()
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
        NotLeftRule returnedRule = new NotLeftRule(AllRules.NotSinistra, ruleIcon, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
