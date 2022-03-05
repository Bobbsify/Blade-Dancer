﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotBreakRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotBreakRuleSettings", order = 15)]
public class NotBreakRuleSettings : RuleSetting
{
    private float durationModFormula => 1.0f;

    public NotBreakRuleSettings()
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
        NotBreakRule returnedRule = new NotBreakRule(AllRules.NotRompi, ruleIcon, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
