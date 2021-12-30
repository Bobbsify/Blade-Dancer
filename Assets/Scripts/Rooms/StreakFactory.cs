using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakFactory
{
    private StageFactory factory;

    private const int streakDefaultLength = 10;

    public StreakFactory(StageFactory factory)
    {
        this.factory = factory;
    }

    public StreakFactory(GameObject[] defaultRooms, RuleFactory factory, GameManager gm)
    {
        this.factory = new StageFactory(defaultRooms, factory, gm);
    }

    public Streak GetRandomStreak(float difficulty,float difficultyIncreaseAmount, int streakLength = streakDefaultLength)
    {
        List<Stage> stages = new List<Stage>();
        for (int i = 0; i < streakLength; i++)
        {
            stages.Add(factory.GetRandomStage(compileDifficulty(ref difficulty, difficultyIncreaseAmount)));
        }
        return new Streak(stages);
    }

    public Streak GetTutorialStreak()
    {
        List<Stage> tutorialStages = new List<Stage>();

        tutorialStages.Add(factory.GetFixedStage(AllRules.Move)); //Stage 1
        tutorialStages.Add(factory.GetFixedStage(AllRules.Shoot)); //Stage 2
        tutorialStages.Add(factory.GetFixedStage(AllRules.Dash)); //Stage 3
        tutorialStages.Add(factory.GetFixedStage(AllRules.Dance)); //Stage 4
        tutorialStages.Add(factory.GetFixedStage(AllRules.Move, AllRules.Shoot)); //Stage 5
        tutorialStages.Add(factory.GetFixedStage(AllRules.Move, AllRules.NotShoot)); //Stage 6
        tutorialStages.Add(factory.GetFixedStage(AllRules.Move, AllRules.NotDash)); //Stage 7
        tutorialStages.Add(factory.GetFixedStage(AllRules.Dash, AllRules.NotShoot)); //Stage 8
        tutorialStages.Add(factory.GetFixedStage(AllRules.Kill)); //Stage 9
        tutorialStages.Add(factory.GetFixedStage(AllRules.Kill, AllRules.NotDash, AllRules.NotDance)); //Stage 10

        return new Streak(tutorialStages);
    }

    private int compileDifficulty(ref float difficulty, float increaseAmount)
    {
        difficulty += increaseAmount;
        return (int)difficulty; //Truncate to lowest integer for difficulty
    }
}
