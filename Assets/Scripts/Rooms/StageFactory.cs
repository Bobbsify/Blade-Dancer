﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFactory
{
    GameObject[] defaultRooms;
    RuleFactory factory;

    public StageFactory(GameObject[] defaultRooms, RuleFactory factory)
    {
        this.defaultRooms = defaultRooms;
        this.factory = factory;
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
        return new Stage(randomRoom,submittedRules);
    }

}
