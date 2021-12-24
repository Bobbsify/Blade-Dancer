using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    [SerializeField]
    private AllRulesObject rules;

    [SerializeField]
    [Range(1, 3)]
    private int minAmount = 1;

    [SerializeField]
    [Range(4, 8)]
    private int maxAmount = 4;

    List<Rule> rulesToApply = new List<Rule>();

    private RuleFactory factory;

    private void Awake()
    {
        factory = new RuleFactory(rules);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Generated:");
            rulesToApply = factory.GetRandomRuleset(Random.Range(minAmount,maxAmount));
            PrintRules();
        }
    }

    private void PrintRules()
    {
        foreach (Rule rule in rulesToApply)
        {
            string color = rule.GetRuleName().ToString().ToLower().Contains("not") ? "blue" : "red";
            Debug.Log("<color=" + color + ">"+ rule.ToString() + "</color>");
        }
    }
}
