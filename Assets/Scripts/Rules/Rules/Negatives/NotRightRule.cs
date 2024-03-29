﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRightRule : Rule
{
    private bool left = true;

    public NotRightRule(AllRules ruleName, Sprite icon, float durationMod, List<Actions> appliedActions, List<AllRules> mutuallyExclusives, List<RuleObject> ruleRelatedObjects)
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
                left = false;
                return left;
            }
        }
        return left;
    }

    public override bool IsRuleComplete()
    {
        return left;
    }

    public override string ToString()
    {
        return "Stay Right";
    }
}
