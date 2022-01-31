using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotMoveRuleSettings", menuName = "ScriptableObjects/RuleSettings/NotMoveRuleSettings", order = 1)]
public class NotMoveRuleSettings : RuleSetting
{
    private float durationModFormula => -2f;

    public NotMoveRuleSettings()
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
        NotMoveRule returnedRule = new NotMoveRule(AllRules.NotMuoviti, durationMod, appliedActions, mutuallyExclusives, ruleRelatedObjects);
        return returnedRule;
    }
}
