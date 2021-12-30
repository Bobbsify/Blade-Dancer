using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootRuleSettings", menuName = "ScriptableObjects/RuleSettings/ShootRuleSettings", order = 2)]
public class ShootRuleSettings : RuleSetting
{
    [SerializeField]
    [Tooltip("Amount to divide from 'Amount To Shoot' to define time (works only on Duration Mod Type Rule Dependant)")]
    [Range(1,5)]
    private int durationModDivider = 3;

    [SerializeField]
    [Range(1,4)]
    private int minAmount = 1;

    [SerializeField]
    [Range(5,10)]
    private int maxAmount = 6;

    private int amountToShoot = 3;

    private float durationModFormula => amountToShoot / durationModDivider;

    public ShootRuleSettings()
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
        amountToShoot = UnityEngine.Random.Range(minAmount, maxAmount);
        durationMod = durationModFormula;
        ShootRule ruleReturned = new ShootRule(AllRules.Shoot, amountToShoot, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return ruleReturned;
    }
}
