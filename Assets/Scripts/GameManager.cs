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
    public Player bluePlayer;
    public Player redPlayer;

    private Player taggedPlayer;
    private static float tagCooloffDuration = 5;
    private static float tagTimer = tagCooloffDuration + 1;

    // Use this for initialization
    void Start () {
        timer.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);

        switch (gameMode)
        {
            case GameMode.Battle:
                break;
            case GameMode.Tag:
                // Tag a random player
                int random = Random.Range(0, 2);
                if (random == 0)
                    TagPlayer(redPlayer);
                else
                    TagPlayer(bluePlayer);
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
                tagTimer += Time.deltaTime;
                if (countdown.IsExpired())
                    timer.gameObject.SetActive(true);
                if (timer.IsExpired())
                    Debug.Log("GameOver");
                break;
        }
    }

    public float GetTagTimer()
    {
        return tagTimer;
    }

    public void RestartTagTimer()
    {
        tagTimer = 0;
    }

    public void TagPlayer(Player player)
    {
        if (tagTimer < tagCooloffDuration)
            return;

        if (player != taggedPlayer)
        {
            RestartTagTimer();
            if (taggedPlayer != null)
                taggedPlayer.Glow(false);
            player.Glow(true);
            player.Pulse(tagCooloffDuration);
            taggedPlayer = player;
        }
    }
}
