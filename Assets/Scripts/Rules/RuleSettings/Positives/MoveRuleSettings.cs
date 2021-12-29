using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveRuleSettings", menuName = "ScriptableObjects/RuleSettings/MoveRuleSettings", order = 0)]
public class MoveRuleSettings : RuleSetting
{
    [SerializeField]
    [Range(2,5)]
    private int minDuration = 2;

    [SerializeField]
    [Range(5, 10)]
    private int maxDuration = 10;

    private int durationOfMove = 3;

    private float durationModFormula => durationOfMove + MERCY;
    private const float MERCY = 1.0f;

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
        MoveRule ruleReturned = new MoveRule(AllRules.Move, durationOfMove, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return ruleReturned;
    }
}
