using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig instance = null;

    public enum GameMode
    {
        Battle,
        Tag
    }
    public GameMode gameMode = GameMode.Battle;

    public enum Level
    {
        Lava,
        Spike
    }
    public Level level = Level.Lava;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }
    
}
