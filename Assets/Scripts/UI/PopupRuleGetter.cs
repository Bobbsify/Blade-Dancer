using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupRuleGetter : MonoBehaviour
{
    [SerializeField]
    private UIRulesController urc;

    [SerializeField]
    private List<Text> texts = new List<Text>();

    private void OnValidate()
    {
        if (urc == null) 
        {
            urc = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<UIRulesController>(true);
        }
        if (texts.Count == 0) 
        {
            texts.AddRange(GetComponentsInChildren<Text>(true));
        }
    }

    public void CompileTexts() 
    {
        try
        {
            List<RuleContainerManager> RCMs = urc.GetContainers();
            for (int i = 0; i < RCMs.Count; i++)
            {
                string rule = RCMs[i].GetCurrentRule().ToString();
                texts[i].text = rule;
            }
        }
        catch (System.Exception) 
        {
            Debug.LogError("Text number does not correspond to Rule containers, dropping operation");
            return;
        }
    }


}
