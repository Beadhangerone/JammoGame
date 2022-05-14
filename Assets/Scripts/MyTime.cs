using System;

public class MyTime
{
    public float LapsedTime { get; set; }
        
    public MyTime()
    {
        LapsedTime = 0f;
    }

    public int GetMinutes()
    {
        return (int)(LapsedTime / 60f);
    }
  
    public int GetSeconds()
    {
        return (int)(LapsedTime)%60;
    }
  
    public float GetMilliseconds()
    {
        return (float)(LapsedTime - System.Math.Truncate(LapsedTime));
    }

    public string GetTime()
    {
        string mins = (GetMinutes() < 10 ? "0" : "") + GetMinutes();
        string secs = (GetSeconds() < 10 ? "0" : "") + GetSeconds();
        return mins + ":" + secs;
    }

    public string GetTimePrecise()
    {
        string mins = (GetMinutes() < 10 ? "0" : "") + GetMinutes();
        string secs = (GetSeconds() < 10 ? "0" : "") + GetSeconds();
        string msecs = Math.Round(GetMilliseconds() * 100f) + ""; 
        return mins + ":" + secs + "."+ msecs;
    }
}