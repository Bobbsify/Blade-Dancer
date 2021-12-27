using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotDashRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotDashRuleSettings", order = 5)]
public class NotDashRuleSettings : RuleSetting
{
    [SerializeField]
    [Tooltip("Amount to divide from 'Amount of dashes' to define time")]
    [Range(1, 5)]
    private int durationModDivider = 3;

    [SerializeField]
    [Range(1, 4)]
    private int minAmount = 1;

    [SerializeField]
    [Range(5, 10)]
    private int maxAmount = 6;

    private int targetAmountOfDashes;

    private float durationModFormula => targetAmountOfDashes / durationModDivider;

    public NotDashRuleSettings()
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
        targetAmountOfDashes = UnityEngine.Random.Range(minAmount, maxAmount);
        NotDashRule returnedRule = new NotDashRule(AllRules.NotDash,targetAmountOfDashes, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
