using System;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class PlayerSave
{
    private static PlayerSave _instance;

    public static PlayerSave Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerSave();
            }

            return _instance;
        }
    }
    private Dictionary<int, MyTime>  BestTimesByLevels { get; set; }

    private PlayerSave()
    {
        BestTimesByLevels = new Dictionary<int, MyTime>();
        // BestTimesByLevels.Add(null);
        // BestTimesByLevels.Add(null);
        // BestTimesByLevels.Add(null);
    }

    public MyTime GetBestTimeForLevel(int level)
    {
        MyTime best;
        if(!BestTimesByLevels.TryGetValue(level - 1, out best))
        {
            Debug.Log("CURRENTBEST:NULL");
            return null;
            
        }
        Debug.Log("CURRENTBEST:"+best.GetText());
        return best;
    }

    public void SaveResultForLevel(int level, MyTime time)
    {
        Debug.Log("RESULT:"+time.LapsedTime);
        MyTime currentBest = GetBestTimeForLevel(level);
        if (currentBest == null || currentBest.LapsedTime > time.LapsedTime)
        {
            BestTimesByLevels[level-1] = time;
        }
        Debug.Log("SAVED:"+BestTimesByLevels[level-1].LapsedTime);
    }
}
