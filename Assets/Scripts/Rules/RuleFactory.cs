using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleFactory
{
    private List<RuleSetting> rulesSettings;

    public RuleFactory(List<RuleSetting> ruleSettings)
    {
        this.rulesSettings = ruleSettings;
    }

    public Rule GetRandomRule()
    {
        int allRulesLength = rulesSettings.Count;
        int element = Random.Range(0,allRulesLength);
        return rulesSettings[element].GetRule();
    }

    public List<Rule> GetRandomRuleset(int amount)
    {
        List<Rule> selectedRules = new List<Rule>();
        List<RuleSetting> rulePool = CompileRulePool();

        int rulePoolLength = rulePool.Count;

        //At least one rule must not be reverse so the first rule is always normal
        do
        {
            int allRulesLength = rulePool.Count;
            int randomElement = Random.Range(0, allRulesLength);
            RuleSetting randomRuleSetting = rulePool[randomElement];
            Rule randomRule = randomRuleSetting.GetRule();
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
            RuleSetting randomRuleSetting = rulePool[randomElement];
            Rule randomRule = randomRuleSetting.GetRule();

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
                rulePool.Remove(randomRuleSetting); //Todo add back after completion
            }
            else
            {
                i--;
            }
            if(CheckCompleteExclusivity(selectedRules)){ break; } //Exit if ruleset is mutally exclusive with every rule
        }
        return selectedRules;
    }

    private bool CheckCompleteExclusivity(List<Rule> selectedRules)
    {
        int totalRules = System.Enum.GetNames(typeof(AllRules)).Length;
        List<int> appeared = new List<int>();
        foreach (Rule rule in selectedRules) 
        {
            foreach (AllRules ruleName in rule.GetMutallyExclusives()) 
            {
                if (!appeared.Contains((int)ruleName)) 
                {
                    appeared.Add((int)ruleName);
                }
            }
        }
        return totalRules == appeared.Count;
    }

    public Rule GetRule(AllRules ruleName)
    {
        foreach(RuleSetting setting in rulesSettings)
        {
            Rule r = setting.GetRule();
            if (r.GetRuleName() == ruleName)
            {
                return r;
            }
        }
        throw new System.Exception("Could not find rule " + ruleName);
    }

    private List<RuleSetting> CompileRulePool()
    {
        List<RuleSetting> rulePool = new List<RuleSetting>();
        foreach (RuleSetting r in rulesSettings)
        {
            rulePool.Add(r);
        }
        return rulePool;
    }
}
