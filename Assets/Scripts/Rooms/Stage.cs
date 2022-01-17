using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    private GameObject room;
    private List<Rule> stageRules = new List<Rule>();
    private List<RuleObject> ruleRelatedObjectsToSpawn = new List<RuleObject>();

    public Stage(GameObject defaultRoom, List<Rule> stageRules, GameManager gm)
    {
        this.room = defaultRoom;

        foreach (Rule rule in stageRules)
        {
            foreach (RuleObject obj in rule.GetRuleRelatedObjects())
            {
                GameObject clone = obj.GetRuleObj();
                if (clone != null) { 
                    IGameEntity[] gameEntitiesInClone = clone.GetComponents<IGameEntity>();
                    foreach (IGameEntity entity in gameEntitiesInClone)
                    {
                        entity.Init(gm);
                    }
                }
                RuleObject compiledRuleObject = new RuleObject(obj.GetName(), clone, obj.GetPositionType());
                ruleRelatedObjectsToSpawn.Add(compiledRuleObject);
            }
        }

        this.stageRules = stageRules;
    }

    public GameObject GetRoom() { return this.room; }
    public List<Rule> GetRules() { return this.stageRules; }
    public List<RuleObject> GetRuleRelatedObjectsToSpawn() { return this.ruleRelatedObjectsToSpawn; }
    public float GetRulesTime() 
    {
        float amountOfTime = 0.0f;
        foreach (Rule r in stageRules) 
        {
            amountOfTime += r.GetDuration();
        }
        return amountOfTime;
    }
}
