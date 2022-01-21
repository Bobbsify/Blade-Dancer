using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleContainerManager : MonoBehaviour
{
    [SerializeField]
    private Text ruleName;

    [SerializeField]
    private Image completed;

    [SerializeField]
    private Text ruleScore;

    private AllRules currentRule;
    private bool setup = false;

    private const string tickContainerName = "TICK";
    private const string nameContainerName = "NAME";
    private const string scoreContainerName = "SCORE";

    private void OnValidate()
    {
        if (ruleName == null && ruleScore == null) { 
            foreach (Text t in GetComponentsInChildren<Text>())
            {
                if (t.name.ToUpper().Equals(nameContainerName))
                {
                    ruleName = t;
                }
                if (t.name.ToUpper().Equals(scoreContainerName))
                {
                    ruleScore = t;
                }
            }
        }

        if (completed == null) 
        {
            completed = GetComponentInChildren<Image>();
        }
    }

    private void Start()
    {
        DisableComponents();
    }

    public void ResetContainer() 
    {
        DisableComponents();
    }

    public void SetupForRule(RulePacket packet)
    {
        setup = true;
        this.currentRule = packet.GetName();
        if (packet.IsReverse())
        {
            ruleName.color = Color.black;
        }
        else
        {
            ruleName.color = Color.white;
        }
        EnableComponents();
        UpdateInformation(packet);
    }

    public AllRules GetCurrentRule() 
    {
        return this.currentRule;
    }

    public void SendPacket(RulePacket packet) 
    {
        UpdateInformation(packet);
    }

    public void Reset()
    {
        setup = false;
        DisableComponents();
    }

    private void EnableComponents()
    {
        ruleName.gameObject.SetActive(true);
        ruleScore.gameObject.SetActive(true);
    }

    private void DisableComponents()
    {
        ruleName.gameObject.SetActive(false);
        ruleScore.gameObject.SetActive(false);
        completed.gameObject.SetActive(false);
    }

    private void UpdateInformation(RulePacket packet)
    {
        ruleName.text = packet.GetName().ToString();
        ruleScore.text = packet.GetScore(); //If complete hide score
        completed.gameObject.SetActive(!packet.IsReverse() && packet.GetCompleted());
    }
}
