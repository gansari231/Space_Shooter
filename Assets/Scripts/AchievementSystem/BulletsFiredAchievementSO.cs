using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BulletsFiredAchievementSO", menuName = "ScriptableObjects/Achievement/NewBulletsFiredAchievmentSO")]
public class BulletsFiredAchievementSO : ScriptableObject
{
    public AchievementType[] Achievements;

    [Serializable]
    public class AchievementType
    {
        public enum BulletAchievements
        {
            None,
            Shots_Fired,
            Destructor,
            Terminator
        }

        public string name;
        public string info;
        public BulletAchievements selectAchievement;
        public int requirement;
    }
}
