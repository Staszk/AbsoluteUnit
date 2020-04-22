using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbsoluteUnit;

public class EXPCalculatorTestDriver
{

    //[Test] -- Should be able to mark this with an attribute
    public Result EXPTestCaseOne()
    {
        int playerLevel = 1;
        int enemyLevel = 1;
        float modifier = 1f;
        EXPCalculator.EnemyList enemy = EXPCalculator.EnemyList.ShinraScorpionSentinel;

        int expected = 750;
        int actual = EXPCalculator.CalculateEXP(playerLevel, enemyLevel, enemy, modifier);

        return Verify.AreEqual(expected, actual);
    }

    // Test case two should fail because expected is incorrect
    //[Test]
    public Result EXPTestCaseTwo()
    {
        int playerLevel = 5;
        int enemyLevel = 3;
        float modifier = 1f;
        EXPCalculator.EnemyList enemy = EXPCalculator.EnemyList.ShinraSecurityOfficer;

        int expected = 35;
        int actual = EXPCalculator.CalculateEXP(playerLevel, enemyLevel, enemy, modifier);

        return Verify.AreEqual(expected, actual);
    }
}
