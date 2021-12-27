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

    public StreakFactory(GameObject[] defaultRooms, RuleFactory factory)
    {
        this.factory = new StageFactory(defaultRooms, factory);
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

    private int compileDifficulty(ref float difficulty, float increaseAmount)
    {
        difficulty += increaseAmount;
        return (int)difficulty; //Truncate to lowest integer for difficulty
    }
}
