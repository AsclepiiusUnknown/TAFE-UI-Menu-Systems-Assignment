﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int score { get; private set; }
    float lastEnemyKilledTime;
    int streakCount;
    float streakExpriryTime = 1;

    private void Start()
    {
        score = 0;
        Enemy.OnDeathStatic += OnEnemyKilled;
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }

    void OnEnemyKilled()
    {
        if(Time.time < lastEnemyKilledTime + streakExpriryTime)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }

        lastEnemyKilledTime = Time.time;

        score += 5 + (int)Mathf.Pow(2, streakCount);
    }

    void OnPlayerDeath()
    {
        Enemy.OnDeathStatic -= OnEnemyKilled;
    }
}
