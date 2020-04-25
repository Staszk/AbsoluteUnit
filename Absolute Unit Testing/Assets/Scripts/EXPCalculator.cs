using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to use as an example for the unit testing
/// </summary>
public static class EXPCalculator
{
    public enum EnemyList
    { 
        ShinraGuardDog,
        ShinraSecurityOfficer,
        ShinraSentryRay,
        ShinraMonodrive,
        ShinraScorpionSentinel,
        ShinraSweeper,
        ShinraShockTrooper
    }

    private static readonly Dictionary<EnemyList, int> BaseEXPChart = new Dictionary<EnemyList, int>()
    {
        {EnemyList.ShinraGuardDog,  58},
        {EnemyList.ShinraSecurityOfficer,  35},
        {EnemyList.ShinraSentryRay,  30},
        {EnemyList.ShinraMonodrive,  30},
        {EnemyList.ShinraScorpionSentinel,  750},
        {EnemyList.ShinraSweeper,  104},
        {EnemyList.ShinraShockTrooper,  80},
    };

    public static int CalculateEXP(int playerLevel, int enemyLevel, EnemyList enemy, float modifier = 1f)
    {
        int levelDiff = enemyLevel - playerLevel;
        float levelModifier = 1 + 0.015f * levelDiff;

        return Mathf.RoundToInt(BaseEXPChart[enemy] * 1f * 1f);
    }
}
