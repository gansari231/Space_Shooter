using UnityEngine;

public class AchievementSystem : SingletonGeneric<AchievementSystem>
{
    [SerializeField] 
    private AchievementHolder achievementSOList;
    private int currentBulletFiredAchivementLevel;

    private void Start()
    {
        currentBulletFiredAchivementLevel = 0;
    }

    public void BulletsFiredCountCheck(int bulletCount)
    {
        for(int i = 0; i < achievementSOList.bulletsFiredAchievementSO.Achievements.Length; i++)
        {
            if( achievementSOList.bulletsFiredAchievementSO.Achievements[i].requirement == bulletCount)
            {
                UIManager.Instance.LaserAchievementSystem(achievementSOList.bulletsFiredAchievementSO.Achievements[i].name, achievementSOList.bulletsFiredAchievementSO.Achievements[i].info);
                currentBulletFiredAchivementLevel = i + 1;
            }
        }
    }
}
