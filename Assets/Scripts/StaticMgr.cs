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
        if (shoot == 0)
        {
            return "0%";
        }

        float per = 100.0f * score / shoot;
        string s = String.Format("{0:0.00}%", per);
        return s;
    }

}

