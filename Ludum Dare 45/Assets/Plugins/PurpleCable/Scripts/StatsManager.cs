using System.Collections.Generic;
using UnityEngine;

namespace PurpleCable
{
    public static class StatsManager
    {
        private static List<IStatsAPI> _statsAPIs = new List<IStatsAPI>();

        public static void AddStatAPI(IStatsAPI statsAPI)
        {
            if (_statsAPIs.Contains(statsAPI))
                return;

            _statsAPIs.Add(statsAPI);
        }

        public static void SubmitStat(string statName, int value)
        {
            PlayerPrefs.SetInt(statName, 1);

            foreach (var statsAPI in _statsAPIs)
            {
                statsAPI.SubmitStat(statName, value);
            }

            Debug.Log($"SubmitStat: {statName}, {value}");
        }

        public static void SubmitAchivement(string achivementName)
        {
            SubmitStat(achivementName, 1);
        }
    }

    public interface IStatsAPI
    {
        void SubmitStat(string statName, int value);
    }
}
