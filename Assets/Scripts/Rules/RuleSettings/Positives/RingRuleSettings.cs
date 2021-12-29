using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RingRuleSettings", menuName = "ScriptableObjects/RuleSettings/RingRuleSettings", order = 8)]
public class RingRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public RingRuleSettings()
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
                throw new System.InvalidOperationException("Unkown Duration Mod Type: " + durationMod);
        }
    }

    public override Rule GetRule()
    {
        RingRule returnedRule = new RingRule(AllRules.Ring, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
