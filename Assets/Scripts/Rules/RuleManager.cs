using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour, IGameEntity
{
    [SerializeField]
    GameManager gameManagerObject;

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
            PrintRules();
        }
    }

    private void PrintRules()
    {
        foreach (Rule rule in rulesToApply)
        {
            string color = rule.IsReverse() ? "cyan" : "red";
            Debug.Log("<color=" + color + ">"+ rule.ToString() + "</color>");
        }
    }

    public void ApplyRule(Actions actionDone)
    {
        foreach (Rule rule in rulesToApply)
        {
            rule.CheckAction(actionDone);
        }

        CheckIfAllCompleted();
    }

    private void CheckIfAllCompleted()
    {
        bool completed = true;
        foreach (Rule rule in rulesToApply)
        {
            completed = completed && rule.IsRuleComplete();

            //If a reverse rule has been failed kill player
            if (rule.IsReverse() && !rule.IsRuleComplete())
            {
                gameManagerObject.KillPlayer();
            }
        }
        if (completed)
        {
            RulesCompleted();
        }
    }

    private void RulesCompleted()
    {

    }

    void IGameEntity.Init(GameManager gameManager)
    {
        gameManagerObject = gameManager;
    }
}
