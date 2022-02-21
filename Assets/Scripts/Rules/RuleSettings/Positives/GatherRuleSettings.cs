using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GatherRuleSettings", menuName = "ScriptableObjects/RuleSettings/GatherRuleSettings", order = 16)]
public class GatherRuleSettings : RuleSetting
{
    [SerializeField]
    [Tooltip("Amount that each collectible spawned adds to time")]
    [Range(1, 5)]
    private int durationModMultiplier = 2;

    [SerializeField]
    [Range(1, 3)]
    int minAmountToGather = 1;

    [SerializeField]
    [Range(4, 16)]
    int maxAmountToGather = 4;

    int amountToGather;

    private float durationModFormula => amountToGather * durationModMultiplier;

    public GatherRuleSettings()
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
        amountToGather = UnityEngine.Random.Range(minAmountToGather, maxAmountToGather);
        durationMod = durationModFormula;
        GatherRule returnedRule = new GatherRule(AllRules.Raccogli, ruleIcon, amountToGather, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
