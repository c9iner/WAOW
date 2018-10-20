using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectRadioButton : RadioButton {

    public GameConfig.Level level;
    private GameConfig config;

    public new void Awake()
    {
        base.Awake();
        config = GameObject.FindGameObjectWithTag("GameConfig").GetComponent<GameConfig>();
    }

    public void Start()
    {
        if (config.level == level)
            Select();
    }

    public override void Execute()
    {
        config.level = level;
    }
}
