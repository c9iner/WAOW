using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameMode
    {
        Battle,
        Tag
    }

    public GameMode gameMode;

    public Timer timer;
    public Countdown countdown;

	// Use this for initialization
	void Start () {
        timer.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);

        switch (gameMode)
        {
            case GameMode.Battle:
                break;
            case GameMode.Tag:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameMode)
        {
            case GameMode.Battle:
                break;
            case GameMode.Tag:
                if (countdown.IsExpired())
                    timer.gameObject.SetActive(true);
                if (timer.IsExpired())
                    Debug.Log("GameOver");
                break;
        }
    }
}
