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
    private Image ruleIcon;

    [SerializeField]
    private Image completed;

    [SerializeField]
    private Slider ruleScore;

    private Color normalRuleColor = new Color(0.95311f, 0.95311f, 0.95311f);
    private Color reverseRuleColor = new Color(0.92941f, 0.24314f, 0.60392f);
    

    private AllRules currentRule;

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

        if (ruleIcon == null)
        {
            foreach (Image i in GetComponentsInChildren<Image>()) 
            {
                if (i != completed) 
                {
                    ruleIcon = i;
                    break;
                }
            }
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
        //SetupSlider
        ruleScore.maxValue = packet.GetMaxScore();
        ruleScore.minValue = 0;

        ruleIcon.sprite = packet.GetIcon();

        this.currentRule = packet.GetName();
        if (packet.IsReverse())
        {
            ruleName.color = reverseRuleColor;
            ruleIcon.color = reverseRuleColor;
        }
        else
        {
            ruleName.color = normalRuleColor;
            ruleIcon.color = normalRuleColor;
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
        ruleIcon.gameObject.SetActive(true);
        ruleScore.gameObject.SetActive(true);
    }

    private void DisableComponents()
    {
        ruleName.gameObject.SetActive(false);
        ruleIcon.gameObject.SetActive(false);
        ruleScore.gameObject.SetActive(false);
        completed.gameObject.SetActive(false);
    }

    private void UpdateInformation(RulePacket packet)
    {
        ruleName.text = packet.GetName().ToString();
        ruleScore.value = packet.GetScore(); 
        //If complete hide score
        completed.gameObject.SetActive(!packet.IsReverse() && packet.GetCompleted());
    }
}
