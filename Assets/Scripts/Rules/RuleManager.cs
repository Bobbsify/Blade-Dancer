using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleManager : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManagerObject;

    [SerializeField]
    private UIRuleControllerManager uiRulesManager;
    
    private List<RuleSetting> rules;

    List<Rule> rulesToApply = new List<Rule>();

    private RuleFactory factory;

    private void Awake()
    {
        factory = new RuleFactory(rules);
    }

    private void Start()
    {
        if (uiRulesManager == null) 
        {
            gameManagerObject.GetUIComponent<UIRuleControllerManager>();
        }
    }

    public void SetNewRuleset(List<Rule> newRuleset)
    {
        if (newRuleset.Count == 0) 
        {
            uiRulesManager.ResetContainers();
        }
        rulesToApply = newRuleset;
        uiRulesManager.SetupRules(GetUpdates());
        uiRulesManager.ShowRules();
    }

    public bool IsCurrentlyRule(AllRules rule)
    {
        foreach (Rule r in rulesToApply) 
        {
            if (r.GetRuleName().Equals(rule)) 
            {
                return true; 
            }
        }
        return false;
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
        uiRulesManager.GetUpdate(GetUpdates());
    }

    private List<RulePacket> GetUpdates() 
    {
        List<RulePacket> updates = new List<RulePacket>();
        int ruleAmount = Enum.GetValues(typeof(AllRules)).Length / 2; //AllRules will always be even
        foreach (Rule r in rulesToApply)
        {
            RulePacket update = r.ToPacket();
            if (r.IsReverse()) 
            {
                update = new RulePacket(r.GetPureRuleName(), r.GetRuleIcon(), update.GetScore(), update.GetMaxScore(), update.GetCompleted(),update.IsReverse());
            }
            updates.Add(update);
        }
        return updates;
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
        if (gameManagerObject.PlayerPawn.GetComponent<PlayerController>().GetHealth() > 0) 
        {
            uiRulesManager.StageCompleted();
            gameManagerObject.EndOfStage();
        }
    }

    void IGameEntity.Init(GameManager gameManager)
    {
        gameManagerObject = gameManager;
        rules = gameManager.GetRuleSettings();
    }
}