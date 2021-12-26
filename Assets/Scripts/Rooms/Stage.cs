using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    private GameObject room;
    private List<Rule> stageRules = new List<Rule>();

    public Stage(GameObject defaultRoom, List<Rule> stageRules)
    {
        this.room = defaultRoom;
        foreach (Rule rule in stageRules)
        {
            foreach (RuleObject obj in rule.GetRuleRelatedObjects())
            {
                GameObject clone = obj.ruleObjPrefab;
                clone.transform.position = FetchObjectPosition(obj.pos);
            }
        }
    }

    private Vector3 FetchObjectPosition(PositionType pos)
    {
        RoomController rc;
        room.TryGetComponent(out rc);
        return rc.GetPos(pos);
    }

    public GameObject GetRoom() { return this.room; }
    public List<Rule> GetRules() { return this.stageRules; }
}
