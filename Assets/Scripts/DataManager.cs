using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static float score = 0;
    public static int actualLevel = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (PlayerPrefs.HasKey("level"))
        {
            actualLevel = PlayerPrefs.GetInt("level");
        }
    }

    void Update()
    {
        
    }

    public static void Score(float value)
    {
        score = value;
    }

    public static float Score()
    {
        return score;
    }

    public static void Level(int value)
    {
        actualLevel = value;
        PlayerPrefs.SetInt("level", value);
    }

    public static int Level()
    {
        return actualLevel;
    }
}
