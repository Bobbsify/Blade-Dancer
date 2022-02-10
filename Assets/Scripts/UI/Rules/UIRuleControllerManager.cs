using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRuleControllerManager : MonoBehaviour
{
    Dictionary<bool, UIRulesController> ruleControllers = new Dictionary<bool, UIRulesController>();

    private UIRulesController currentRuleController;

    private void Awake()
    {
        foreach (UIRulesController controller in this.transform.GetComponentsInChildren<UIRulesController>(true)) 
        {
            ruleControllers.Add(controller.transform.childCount % 2 == 0, controller);
        }
    }
    public void GetUpdate(List<RulePacket> rulePackets)
    {
        currentRuleController.GetUpdate(rulePackets);
    }

    public void ShowRules()
    {
        currentRuleController.ShowRules();
    }

    public void ResetContainers()
    {
        currentRuleController.ResetContainers();
    }

    public void SetupRules(List<RulePacket> rulePackets)
    {
        currentRuleController = ruleControllers[rulePackets.Count % 2 == 0];

        currentRuleController.SetupRules(rulePackets);
    }

    public void StageCompleted()
    {
        currentRuleController.StageCompleted();
    }
}
