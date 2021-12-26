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
        return this.stages[currentStage];
    }

    public Stage NextStage()
    {
        return this.stages[++currentStage];
    }
}
