using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootRuleSettings", menuName = "ScriptableObjects/RuleSettings/ShootRuleSettings", order = 2)]
public class ShootRuleSettings : RuleSetting
{
    /*[SerializeField]
    [Tooltip("Amount to divide from 'Amount To Shoot' to define time (works only on Duration Mod Type Rule Dependant)")]
    [Range(1,5)]
    private int durationModDivider = 3;*/

    [SerializeField]
    [Range(1,3)]
    private int minAmount = 1;

    [SerializeField]
    [Range(4, 6)]
    private int maxAmount = 6;

    [SerializeField]
    private float coolDownShoot = 0.5f;

    [SerializeField]
    private float extraTime = 1;

    private int amountToShoot = 3;

    private float durationModFormula; // => amountToShoot / durationModDivider;

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
        durationModFormula = (amountToShoot * coolDownShoot) + extraTime;
        durationMod = durationModFormula;
        ShootRule ruleReturned = new ShootRule(AllRules.Shoot, amountToShoot, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return ruleReturned;
    }
}
