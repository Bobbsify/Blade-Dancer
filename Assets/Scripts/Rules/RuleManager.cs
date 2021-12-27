using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleManager : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManagerObject;

    [SerializeField]
    [Tooltip("!! TEMPORARY TREAD AS TEXT FIELD OBJECT INSIDE GAMEOBJECT !! (May be permanent)")]
    private GameObject ruleInformationContainer;

    private Text ruleContainer;

    [SerializeField]
    private AllRulesObject rules;

    List<Rule> rulesToApply = new List<Rule>();

    private RuleFactory factory;

    private void Awake()
    {
        factory = new RuleFactory(rules);
        ruleContainer = ruleInformationContainer.GetComponent<Text>();
    }

    public void SetNewRuleset(List<Rule> newRuleset)
    {
        rulesToApply = newRuleset;
        UpdateRulesOnScreen();
    }

    public void ApplyRule(Actions actionDone)
    {
        foreach (Rule rule in rulesToApply)
        {
            rule.CheckAction(actionDone);
        }
        UpdateRulesOnScreen();
        CheckIfAllCompleted();
    }

    private void UpdateRulesOnScreen()
    {
        string rules = "Rules: \n";
        foreach (Rule r in rulesToApply)
        {
            rules += (r.IsReverse() ? " X " : " V ") + r.ToString() + "\n";
        }
        ruleContainer.text = rules;
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
        gameManagerObject.EndOfStage();
    }

    void IGameEntity.Init(GameManager gameManager)
    {
        gameManagerObject = gameManager;
    }
}
