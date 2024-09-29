using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 51)]
public class GameDataScript : ScriptableObject
{
    public int level = 1;
    public int balls = 6;
    public int points = 0;
    public int pointsToBall = 0;
    public bool resetOnStart;
    public bool music = true;
    public bool sound = true;
    public bool is_reset = false;

    public int bonusBaseFrequency;
    public int bonusSlowFrequency;
    public int bonusFastFrequency;
    public int bonusBallFrequency;
    public int bonusTwoFrequency;
    public int bonusTenFrequency;

    public static int bf;
    public static int bsf;
    public static int bff;
    public static int bbf;
    public static int b2f;
    public static int b10f;

    public void Reset()
    {
        level = 1;
        balls = 6;
        points = 0;
        pointsToBall = 0;
        is_reset = true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("balls", balls);
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("pointsToBall", pointsToBall);
        PlayerPrefs.SetInt("music", music ? 1 : 0);
        PlayerPrefs.SetInt("sound", sound ? 1 : 0);
    }

    public void Load()
    {
        level = PlayerPrefs.GetInt("level", 1);
        balls = PlayerPrefs.GetInt("balls", 6);
        points = PlayerPrefs.GetInt("points", 0);
        pointsToBall = PlayerPrefs.GetInt("pointsToBall", 0);
        music = PlayerPrefs.GetInt("music", 1) == 1;
        sound = PlayerPrefs.GetInt("sound", 1) == 1;

        bf = bonusBaseFrequency;
        bsf = bonusSlowFrequency;
        bff = bonusFastFrequency;
        bbf = bonusBallFrequency;
        b2f = bonusTwoFrequency;
        b10f = bonusTenFrequency;
    }
}
