using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text gameOverText;

    // Tag
    private Player taggedPlayer;
    private static float tagCooloffDuration = 2;
    private static float tagTimer = tagCooloffDuration + 1;

    // Battle
    private int battleWinScore = 10;

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
                // Win state
                Player winner = (bluePlayer.score == battleWinScore) ? bluePlayer : (redPlayer.score == battleWinScore) ? redPlayer : null;
                if (winner != null)
                {
                    gameOverText.enabled = true;
                    gameOverText.text = "Game Over\n" + winner.color + " Wins!";
                    bluePlayer.DisableMovement();
                    redPlayer.DisableMovement();
                    Invoke("RestartLevel", 5);
                }
                break;
            case GameMode.Tag:
                tagTimer += Time.deltaTime;
                if (countdown.IsExpired())
                    timer.gameObject.SetActive(true);
                if (timer.IsExpired())
                {
                    gameOverText.enabled = true;
                    gameOverText.text = "Game Over\n" + taggedPlayer.otherPlayer.color + " Wins!";
                    bluePlayer.DisableMovement();
                    redPlayer.DisableMovement();
                    Invoke("RestartLevel", 5);
                }
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
    
    void RestartLevel()
    {
        SceneManager.LoadScene("Level_Lava");
    }
}
