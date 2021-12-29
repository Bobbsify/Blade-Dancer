﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotShootRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotShootRuleSettings", order = 3)]
public class NotShootRuleSettings : RuleSetting
{
    [SerializeField]
    [Range(0, 4)]
    private int minAmount = 0;

    [SerializeField]
    [Range(5, 8)]
    private int maxAmount = 6;

    private int amountNotToShoot = 1;

    private float durationModFormula => 1.0f;

    public NotShootRuleSettings() 
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
                throw new System.InvalidOperationException("Unkown durationModType: " + durationType);
        }
    }

    public override Rule GetRule()
    {
        amountNotToShoot = UnityEngine.Random.Range(minAmount, maxAmount);
        NotShootRule returnedRule = new NotShootRule(AllRules.NotShoot, amountNotToShoot, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}