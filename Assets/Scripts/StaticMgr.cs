using System;

public class StaticMgr
{
    private static StaticMgr _instance;

    public static StaticMgr instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StaticMgr();
            }
            return _instance;
        }
    }

    private int shoot = 0;
    private int score = 0;

    public void ShootAddOne()
    {
        shoot++;
    }

    public void ScoreAddOne()
    {
        score++;
    }

    public string Percent()
    {
        UnityEngine.Debug.Log("" + score + ":" + shoot);
        if (shoot == 0)
        {
            return "0%";
        }

        int per = (int)(100.0 * score / shoot);
        string s = string.Format(per + "%");
        return s;
    }

}

