using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakFactory
{
    private StageFactory factory;

    private const int streakDefaultLength = 10;
    private const int maxDifficulty = 6;

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

        tutorialStages.Add(factory.GetFixedStage(AllRules.Muoviti)); //Stage 1
        tutorialStages.Add(factory.GetFixedStage(AllRules.Spara)); //Stage 2
        tutorialStages.Add(factory.GetFixedStage(AllRules.Scatta)); //Stage 3
        tutorialStages.Add(factory.GetFixedStage(AllRules.Balla)); //Stage 4
        tutorialStages.Add(factory.GetFixedStage(AllRules.Muoviti, AllRules.Spara)); //Stage 5
        tutorialStages.Add(factory.GetFixedStage(AllRules.Muoviti, AllRules.NotSpara)); //Stage 6
        tutorialStages.Add(factory.GetFixedStage(AllRules.Muoviti, AllRules.NotScatta)); //Stage 7
        tutorialStages.Add(factory.GetFixedStage(AllRules.Scatta, AllRules.NotSpara)); //Stage 8
        tutorialStages.Add(factory.GetFixedStage(AllRules.Uccidi)); //Stage 9
        tutorialStages.Add(factory.GetFixedStage(AllRules.Uccidi, AllRules.NotScatta, AllRules.NotBalla)); //Stage 10

        return new Streak(tutorialStages);
    }

    public Streak GetCustomStreak(AllRules[] customRun)
    {
        List<Stage> customStages = new List<Stage>();


        for (int i = 0; i < 10; i++)
        {
            Stage customStage = factory.GetFixedStage(customRun);
            customStages.Add(customStage);
        }

        return new Streak(customStages);
    }

    private int compileDifficulty(ref float difficulty, float increaseAmount)
    {
        difficulty += increaseAmount;
        difficulty = Mathf.Min(difficulty, maxDifficulty);
        return (int)difficulty; //Truncate to lowest integer for difficulty
    }
}
