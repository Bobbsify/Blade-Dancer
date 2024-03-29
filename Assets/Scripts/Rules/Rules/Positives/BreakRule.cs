﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRule : Rule
{
    private bool broken = false;
    
    public BreakRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
    {
        this.RuleName = ruleName;
        this.RuleIcon = icon;
        SetDurationMod(durationMod);
        this.appliedActions = appliedActions;
        this.mutuallyExclusives = mutuallyExclusives;
        this.ruleRelatedObjects = ruleRelatedObjects;
    }

    public override bool CheckAction(Actions executedAction)
    {
        foreach (Actions action in appliedActions)
        {
            if (action == executedAction)
            {
                broken = true;
                return broken;
            }
        }
        return broken;
    }

    public override bool IsRuleComplete()
    {
        return broken;
    }

    public override string ToString()
    {
        return "Break";
    }
}
