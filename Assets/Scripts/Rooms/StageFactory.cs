using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFactory
{
    private GameObject[] defaultRooms;
    private RuleFactory factory;
    private GameManager gm;

    public StageFactory(GameObject[] defaultRooms, RuleFactory factory, GameManager gm)
    {
        this.defaultRooms = defaultRooms;
        this.factory = factory;
        this.gm = gm;
    }

    public Stage GetRandomStage(int difficulty,GameObject room = null)
    {
        GameObject randomRoom = room != null ? room : defaultRooms[Random.Range(0, defaultRooms.Length)]; //If room is passed the random room is the room else it is a random room
        List<Rule> ruleSet = factory.GetRandomRuleset(difficulty);
        List<Rule> submittedRules = new List<Rule>();
        foreach (Rule r in ruleSet)
        {
            submittedRules.Add(r);
        }
        return new Stage(randomRoom,submittedRules,gm);
    }

    public Stage GetFixedStage(GameObject room = null, params AllRules[] rules)
    {
        GameObject randomRoom = room;
        List<Rule> ruleSet = new List<Rule>();
        foreach (AllRules rule in rules)
        {
            ruleSet.Add(factory.GetRule(rule));
        }
        return new Stage(randomRoom, ruleSet,gm);
    }

    public Stage GetFixedStage(params AllRules[] rules)
    {
        GameObject randomRoom = defaultRooms[Random.Range(0, defaultRooms.Length)];
        List<Rule> ruleSet = new List<Rule>();
        foreach (AllRules rule in rules)
        {
            ruleSet.Add(factory.GetRule(rule));
        }
        return new Stage(randomRoom, ruleSet,gm);
    }
}
