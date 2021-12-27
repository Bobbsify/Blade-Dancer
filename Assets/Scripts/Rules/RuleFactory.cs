using System.Collections;
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
        List<Rule> rulePool = rules.getAll();

        int rulePoolLength = rulePool.Count;

        //At least one rule must not be reverse so the first rule is always normal
        do
        {
            int allRulesLength = rulePool.Count;
            int randomElement = Random.Range(0, allRulesLength);
            Rule randomRule = rulePool[randomElement];
            if (!randomRule.IsReverse())
            {
                selectedRules.Add(randomRule);
            }
        } while (selectedRules.Count == 0);

        //All other rules are completely randomized
        for (int i = 1; i < amount; i++)
        {
            int allRulesLength = rulePool.Count;
            int randomElement = Random.Range(0, allRulesLength);
            Rule randomRule = rulePool[randomElement];

            bool exclusive = false;
            foreach (Rule rule in selectedRules)
            {
                if (randomRule.IsMutuallyExclusive(rule.GetRuleName()))
                {
                    exclusive = true;
                }
            }
            if (!exclusive)
            {
                selectedRules.Add(randomRule);
                rulePool.Remove(randomRule);
            }
            else
            {
                i--;
            }
        }
        return selectedRules;
    }
}
