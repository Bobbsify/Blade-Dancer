using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveRuleSettings", menuName = "ScriptableObjects/RuleSettings/MoveRuleSettings", order = 0)]
public class MoveRuleSettings : RuleSetting
{
    [SerializeField]
    [Range(2,3)]
    private int minDuration = 2;

    [SerializeField]
    [Range(3, 5)]
    private int maxDuration = 5;

    private int durationOfMove = 3;

    private float durationModFormula => durationOfMove + extraTime;

    [SerializeField]
    private float extraTime = 1.0f;

    public MoveRuleSettings()
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
        durationOfMove = UnityEngine.Random.Range(minDuration,maxDuration);
        durationMod = durationType == DurationModType.FixedAmount ? durationMod : durationModFormula;
        MoveRule ruleReturned = new MoveRule(AllRules.Muoviti, durationOfMove, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return ruleReturned;
    }
}
