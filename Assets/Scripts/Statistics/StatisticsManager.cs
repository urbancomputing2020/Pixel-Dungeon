﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class StatisticsManager : MonoBehaviour
{
    /// <summary>
    /// Signleton Pattern
    /// </summary>
    private static StatisticsManager statisticsInstance;
    public static StatisticsManager StatisticsInstance
    {
        get
        {
            if (statisticsInstance == null)
                Debug.LogError("There is no " + StatisticsInstance.GetType() + " set.");
            return statisticsInstance;
        }
        private set
        {
            if (statisticsInstance != null)
                Debug.LogError("Two instances of the " + StatisticsInstance.GetType() + " are sethere is no DirectorAI set.");
            statisticsInstance = value;
        }
    }

    private void Awake()
    {
        StatisticsInstance = this;
    }

    //#TODO needs to return more information such that the DirectorAI can create and request new levels
    public int Retrieve()
    {
        return 0;
    }

    /// <summary>
    /// This sends and message to the analytics dashboard.
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public bool SendEvent(messageType encoding)
    {
        AnalyticsResult result = AnalyticsResult.InvalidData;
        switch(encoding)
        {
            case messageType.death:
                result = Analytics.CustomEvent("Death", new Dictionary<string, object>
                {
                    { "level_id", 0 },
                    { "score", 0 },
                    { "difficulty", 0}
                });
                break;
            case messageType.levelComplete:
                result = Analytics.CustomEvent("Level Completed", new Dictionary<string, object>
                {
                    { "level_id", 0 },
                    { "time_elapsed", Time.timeSinceLevelLoad },
                    { "difficulty", 0}
                });
                break;
            default:
                break;
        }
        return result == AnalyticsResult.Ok;
    }
}
public enum messageType
{
    levelComplete,
    death,
    gameover
}