using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRadioButton : RadioButton
{
    GameConfig.GameMode gameMode = GameConfig.GameMode.Battle;
    GameConfig config;

    public new void Awake()
    {
        base.Awake();
        config = GameObject.FindGameObjectWithTag("GameConfig").GetComponent<GameConfig>();
    }

    public void Start()
    {
        if (config.gameMode == gameMode)
            Select();
    }

    public override void Execute()
    {
        config.gameMode = gameMode;
    }
}
