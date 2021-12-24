﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleFactory
{
    private AllRulesObject rules;

    public RuleFactory(AllRulesObject rules)
    {
        this.rules = rules;
    }

    public Rule GetRandomRule()
    {
        int allRulesLength = rules.getAll().Count;
        int element = Random.Range(0,allRulesLength);
        return rules.getAll()[element];
    }

    public List<Rule> GetRandomRuleset(int amount)
    {
        List<Rule> selectedRules = new List<Rule>();
        int allRulesLength = rules.getAll().Count;
        for (int i = 0; i < amount; i++)
        {
            int randomElement = Random.Range(0, allRulesLength);
            Rule randomRule = rules.getAll()[randomElement];

            bool exclusive = false;
            foreach (Rule rule in selectedRules)
            {
                if (rule.IsMutuallyExclusive(randomRule.GetRuleName()))
                {
                    exclusive = true;
                }
            }
            if (!exclusive)
            {
                selectedRules.Add(randomRule);
            }
            else
            {
                i--;
            }
        }
        return selectedRules;
    }
}
