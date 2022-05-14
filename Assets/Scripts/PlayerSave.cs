using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerSave
{
    public List<MyTime> BestTimesByLevels { get; set; }

    public PlayerSave()
    {
        BestTimesByLevels = new List<MyTime>();
        BestTimesByLevels.Add(null);
        BestTimesByLevels.Add(null);
        BestTimesByLevels.Add(null);
    }

    public MyTime GetBestTimeForLevel(int level)
    {
        return BestTimesByLevels[level-1];
    }

    public void SaveResultForLevel(int level, MyTime time)
    {
        Debug.Log(level +":"+ time.GetTime());
        MyTime currentBest = GetBestTimeForLevel(level);
        Debug.Log(currentBest);
        if (currentBest == null || currentBest.LapsedTime > time.LapsedTime)
        {
            BestTimesByLevels[level-1] = time;
        }
    }
}
