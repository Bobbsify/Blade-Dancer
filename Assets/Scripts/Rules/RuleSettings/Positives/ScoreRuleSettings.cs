using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreRuleSettings", menuName = "ScriptableObjects/RuleSettings/ScoreRuleSettings", order = 20)]
public class ScoreRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public ScoreRuleSettings()
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
        ScoreRule returnedRule = new ScoreRule(AllRules.Segna, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
