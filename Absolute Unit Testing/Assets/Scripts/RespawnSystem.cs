using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RespawnSystem
{
    private struct RespawnPoint
    {
        public Vector3 pos;
        public int idFromArray;
        public int value;
    };

    public static int GetBestRespawnPoint(Vector3[] respawnPoints, Vector3[] enemies, Vector3[] allies)
    {
        RespawnPoint[] points = new RespawnPoint[respawnPoints.Length];

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            points[i] = new RespawnPoint()
            {
                pos = respawnPoints[i],
                idFromArray = i,
                value = 0
            };
        }

        for (int i = 0; i < points.Length; i++)
        {
            EvaluateRespawnPoint(ref points[i], enemies, allies);
        }

        points = points.OrderBy(p => p.value).ToArray();

        return points[0].idFromArray;
    }

    private static void EvaluateRespawnPoint(ref RespawnPoint point, Vector3[] enemies, Vector3[] allies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if ((enemies[i] - point.pos).sqrMagnitude < 10)
            {
                point.value += 100;
            }
        }

        for (int i = 0; i < allies.Length; i++)
        {
            if ((allies[i] - point.pos).sqrMagnitude < 6)
            {
                point.value += 49;
            }
        }
    }
}
