﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotRingRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotRingRuleSettings", order = 9)]
public class NotRingRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotRingRuleSettings()
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
        NotRingRule returnedRule = new NotRingRule(AllRules.NotSuona, ruleIcon, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
