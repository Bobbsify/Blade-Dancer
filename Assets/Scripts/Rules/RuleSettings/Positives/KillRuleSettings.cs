using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillRuleSettings", menuName = "ScriptableObjects/RuleSettings/KillRuleSettings", order = 18)]
public class KillRuleSettings : RuleSetting
{
    [SerializeField]
    [Tooltip("Amount that each enemy spawned adds to time")]
    [Range(1, 5)]
    private float durationModMultiplier = 1.5f;

    [SerializeField]
    [Range(1, 3)]
    int minAmountOfEnemies = 1;

    [SerializeField]
    [Range(4, 7)]
    int maxAmountOfEnemies = 7;

    int amountOfEnemies;

    private float durationModFormula; // => (amountOfEnemies * durationModMultiplier) + extraTime;

    public KillRuleSettings()
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
        durationModFormula = amountOfEnemies * durationModMultiplier;
        durationMod = durationModFormula;
        KillRule returnedRule = new KillRule(AllRules.Uccidi, ruleIcon, amountOfEnemies, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
