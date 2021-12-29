﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotRingRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotRingRuleSettings", order = 8)]
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
                throw new System.InvalidOperationException("Unkown Duration Mod Type: " + durationMod);
        }
    }

    public override Rule GetRule()
    {
        NotRingRule returnedRule = new NotRingRule(AllRules.Ring, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
