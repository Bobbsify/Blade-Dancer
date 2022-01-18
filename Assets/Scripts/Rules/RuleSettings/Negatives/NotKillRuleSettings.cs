using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotKillRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotKillRuleSettings", order = 19)]
public class NotKillRuleSettings : RuleSetting
{
    [SerializeField]
    [Tooltip("Amount that each enemy spawned adds to time")]
    [Range(0, 3)]
    private float durationModMultiplier = 1.5f;

    [SerializeField]
    [Range(1, 3)]
    int minAmountOfEnemies = 1;

    [SerializeField]
    [Range(4, 7)]
    int maxAmountOfEnemies = 7;

    int amountOfEnemies;

    private float durationModFormula; // => (amountOfEnemies * durationModMultiplier) + extraTime;


    public NotKillRuleSettings()
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
        amountOfEnemies = UnityEngine.Random.Range(minAmountOfEnemies, maxAmountOfEnemies);
        durationModFormula = 0f;
        durationMod = durationModFormula;
        NotKillRule returnedRule = new NotKillRule(AllRules.NotKill, amountOfEnemies, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
