using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbsoluteUnit;

public class RespawnSystemTestDriver : TestObject
{
    [Test]
    static public Result RespawnSystemTestOne()
    {
        Vector3[] spawnPoints = new Vector3[]
        {
            new Vector3(564.992f, 163.1895f),
            new Vector3(153.1190f, 892.6244f),
        };

        Vector3[] enemies = new Vector3[]
        {
            new Vector3(893.853f, 224.94729f),
            new Vector3(153.1190f, 892.6244f),
            new Vector3(38.644f, 63.95401f)
        };

        Vector3[] allies = new Vector3[]
        {
            new Vector3(153.1190f, 892.6244f),
            new Vector3(468.135f, 295.114f)
        };

        int spawnIndex = RespawnSystem.GetBestRespawnPoint(spawnPoints, enemies, allies);
        int expected = 1;

        return Verify.AreEqual(expected, spawnIndex, "Correctly spawn away from enemies.");
    }

    [Test]
    static public Result RespawnSystemTestTwo()
    {
        Vector3[] spawnPoints = new Vector3[]
        {
            new Vector3(564.992f, 163.1895f),
            new Vector3(153.1190f, 892.6244f),
        };

        Vector3[] allies = new Vector3[]
        {
            new Vector3(893.853f, 224.94729f),
            new Vector3(153.1190f, 892.6244f),

        };

        Vector3[] enemies = new Vector3[]
        {
            new Vector3(153.1190f, 892.6244f),
            new Vector3(468.135f, 295.114f),
            new Vector3(38.644f, 63.95401f)
        };

        int spawnIndex = RespawnSystem.GetBestRespawnPoint(spawnPoints, enemies, allies);
        int expected = 1;

        return Verify.AreEqual(expected, spawnIndex, "Correctly spawn near allies.");
    }
}
