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
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Generated:");
            rulesToApply = factory.GetRandomRuleset(Random.Range(minAmount,maxAmount));
        }
    }

    private void PrintRules()
    {
        foreach (Rule rule in rulesToApply)
        {
            Debug.Log(rule.ToString());
        }
    }
}
