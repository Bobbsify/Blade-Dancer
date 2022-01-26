using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotGatherRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotGatherRuleSettings", order = 17)]
public class NotGatherRuleSettings : RuleSetting
{
    [SerializeField]
    [Tooltip("Amount that each collectible spawned adds to time")]
    [Range(0, 3)]
    private int durationModMultiplier = 2;

    [SerializeField]
    [Range(1, 3)]
    int minAmountToGather = 1;

    [SerializeField]
    [Range(4, 16)]
    int maxAmountToGather = 4;

    int amountNotToGather;

    private float durationModFormula => amountNotToGather * durationModMultiplier;

    public NotGatherRuleSettings()
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
        amountNotToGather = UnityEngine.Random.Range(minAmountToGather, maxAmountToGather);
        if (durationType == DurationModType.RuleDependant) { 
            durationMod = durationModFormula;
        }
        NotGatherRule returnedRule = new NotGatherRule(AllRules.NotGather, amountNotToGather, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
