using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotShootRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotShootRuleSettings", order = 3)]
public class NotShootRuleSettings : RuleSetting
{
    [SerializeField]
    [Range(0, 3)]
    private int minAmount = 0;

    [SerializeField]
    [Range(4, 6)]
    private int maxAmount = 6;

    [SerializeField]
    private float coolDownShoot = 0.5f;

    [SerializeField]
    private float extraTime = 1;

    private int amountNotToShoot = 1;

    private float durationModFormula; // => 1.0f;

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
        durationModFormula = (amountNotToShoot * coolDownShoot) + extraTime;
        durationMod = durationModFormula;
        NotShootRule returnedRule = new NotShootRule(AllRules.NotShoot, amountNotToShoot, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
