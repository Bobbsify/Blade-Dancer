using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rules", menuName = "ScriptableObjects/AllRules", order = 0)]
public class AllRulesObject : ScriptableObject
{
    [Header("-------- Move --------")]
    public MoveRule moveRule;
    public NotMoveRule doNotMoveRule;

    [Header("-------- Shoot --------")]
    public ShootRule shootRule;
    public NotShootRule doNotShootRule;

    [Header("-- Dash --")]
    public DashRule dashRule;
    public NotDashRule doNotDashRule;

    [Header("-------- Dance --------")]
    public DanceRule danceRule;
    public NotDanceRule doNotDanceRule;

    public List<Rule> getAll()
    {
        List<Rule> allRules = new List<Rule>();

        allRules.Add(moveRule);
        allRules.Add(doNotMoveRule);

        allRules.Add(shootRule);
        allRules.Add(doNotShootRule);

        allRules.Add(dashRule);
        allRules.Add(doNotDashRule);

        allRules.Add(danceRule);
        allRules.Add(doNotDanceRule);

        return allRules;
    }
}