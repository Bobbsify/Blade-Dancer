using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotScoreRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotScoreRuleSettings", order = 21)]
public class NotScoreRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotScoreRuleSettings()
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
        NotScoreRule returnedRule = new NotScoreRule(AllRules.NotScore, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
