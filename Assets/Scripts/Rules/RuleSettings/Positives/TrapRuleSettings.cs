﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapRuleSettings", menuName = "ScriptableObjects/RuleSettings/TrapRuleSettings", order = 22)]
public class TrapRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public TrapRuleSettings()
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
        TrapRule returnedRule = new TrapRule(AllRules.Cattura, ruleIcon, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
