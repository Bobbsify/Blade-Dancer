using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rules", menuName = "ScriptableObjects/AllRules", order = 0)]
public class AllRulesObject : ScriptableObject
{
    public MoveRule moveRule;
    public NotMoveRule doNotMoveRule;
    public ShootRule shootRule;
    public NotShootRule doNotShootRule;
}