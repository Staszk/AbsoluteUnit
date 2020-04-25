using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbsoluteUnit;

public class EXPCalculatorTestDriver : TestObject
{

    [Test] // Should be able to mark this with an attribute
    static public Result EXPTestCaseOne()
    {
        int playerLevel = 1;
        int enemyLevel = 1;
        float modifier = 1f;
        EXPCalculator.EnemyList enemy = EXPCalculator.EnemyList.ShinraScorpionSentinel;

        int expected = 750;
        int actual = EXPCalculator.CalculateEXP(playerLevel, enemyLevel, enemy, modifier);

        return Verify.AreEqual(expected, actual, "EXP for enemy is not modified.");
    }

    // Test case two fails because the level modifier is not correct in the function
    [Test]
    static public Result EXPTestCaseTwo()
    {
        int playerLevel = 5;
        int enemyLevel = 3;
        float modifier = 1f;
        EXPCalculator.EnemyList enemy = EXPCalculator.EnemyList.ShinraSecurityOfficer;

        int expected = 34;
        int actual = EXPCalculator.CalculateEXP(playerLevel, enemyLevel, enemy, modifier);

        return Verify.AreEqual(expected, actual, "EXP for enemy is correctly modified");
    }
}
