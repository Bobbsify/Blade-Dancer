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
    private Slider ruleScore;

    private Color normalRuleColor = new Color(0.95311f, 0.95311f, 0.95311f);
    private Color reverseRuleColor = new Color(0.92941f, 0.24314f, 0.60392f);
    

    private AllRules currentRule;
    private float maxAmount = 0;

    private const string tickContainerName = "TICK";
    private const string nameContainerName = "NAME";
    private const string scoreContainerName = "SCORE";

    private void OnValidate()
    {
        if (ruleName == null) {
            ruleName = GetComponentInChildren<Text>();
        }

        if (ruleScore == null)
        {
            ruleScore = GetComponentInChildren<Slider>();
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
        //Setup maxAmount   
        if (packet.GetName().Equals(AllRules.Move) || packet.GetName().Equals(AllRules.NotMove))
        {
            float.TryParse(packet.GetScore(), out maxAmount);
            //SetupSlider
            ruleScore.maxValue = 0;
            ruleScore.minValue = maxAmount;
        }
        else
        {
            if (packet.GetScore().Contains("/"))
            {
                float.TryParse(packet.GetScore().Split('/')[1], out maxAmount); //Second part of 1/6 so 6;}
            }
            //SetupSlider
            ruleScore.maxValue = maxAmount;
            ruleScore.minValue = 0;
        }

        this.currentRule = packet.GetName();
        if (packet.IsReverse())
        {
            ruleName.color = reverseRuleColor;
        }
        else
        {
            ruleName.color = normalRuleColor;
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
        //Get score amount of packet
        float packetScoreAmount = 0;
        float.TryParse(packet.GetScore().Split('/')[0],out packetScoreAmount); //If it has / get only first half if it does not have dash it still gets the first part

        ruleName.text = packet.GetName().ToString();
        ruleScore.value = packetScoreAmount; //If complete hide score
        completed.gameObject.SetActive(!packet.IsReverse() && packet.GetCompleted());
    }
}
