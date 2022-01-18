using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streak
{
    private List<Stage> stages = new List<Stage>();

    private int currentStage = 0;

    public Streak(List<Stage> stages)
    {
        this.stages = stages;
    }

    public Stage GetCurrentStage()
    {
        try
        {
            return this.stages[currentStage];
        }
        catch (System.IndexOutOfRangeException)
        {
            return null;
        }
    }

    public Stage NextStage()
    {
        try
        {
            return this.stages[++currentStage];
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}
