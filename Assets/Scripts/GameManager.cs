using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public GameConfig.GameMode gameMode;
    public Timer timer;
    public Countdown countdown;
    public Player bluePlayer;
    public Player redPlayer;
    public Text gameOverText;

    // Tag
    private Player taggedPlayer;
    private static float tagCooloffDuration = 2;
    private static float tagTimer = tagCooloffDuration + 1;
    private int tagWinScore = 0;

    // Battle
    private int battleWinScore = 10;

    private GameConfig config;
    
    // Use this for initialization
    void Start () {

        config = GameObject.FindGameObjectWithTag("GameConfig").GetComponent<GameConfig>();
        gameMode = config.gameMode;

        switch (gameMode)
        {
            case GameConfig.GameMode.Battle:
                {
                    timer.gameObject.SetActive(false);
                    countdown.gameObject.SetActive(true);
                }
                break;
            case GameConfig.GameMode.Tag:
                {
                    timer.gameObject.SetActive(false);
                    countdown.gameObject.SetActive(true);

                    redPlayer.SetScore(3);
                    bluePlayer.SetScore(3);

                    // Tag a random player
                    int random = Random.Range(0, 2);
                    if (random == 0)
                        TagPlayer(redPlayer);
                    else
                        TagPlayer(bluePlayer);
                }
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        switch (gameMode)
        {
            case GameConfig.GameMode.Battle:
                {
                    TeleportPlayer(redPlayer);
                    TeleportPlayer(bluePlayer);

                    // Score
                    Player winner = (bluePlayer.score == battleWinScore) ? bluePlayer : (redPlayer.score == battleWinScore) ? redPlayer : null;
                    if (winner != null)
                        GameOver(winner);
                }
                break;
            case GameConfig.GameMode.Tag:
                {
                    TeleportPlayer(redPlayer);
                    TeleportPlayer(bluePlayer);

                    // Score
                    Player winner = (bluePlayer.score == tagWinScore) ? redPlayer : (redPlayer.score == tagWinScore) ? bluePlayer : null;
                    if (winner != null)
                    {
                        timer.Stop();
                        GameOver(winner);
                    }
                    // Timer
                    else
                    {
                        tagTimer += Time.deltaTime;
                        if (countdown.IsExpired())
                            timer.gameObject.SetActive(true);
                        if (timer.IsExpired())
                            GameOver(taggedPlayer.otherPlayer);
                    }
                }
                break;
        }
    }

    private void TeleportPlayer(Player player)
    {
        if (player == null)
            return;

        float xBoundary = 22;

        if (player.transform.position.x >= xBoundary)
            player.transform.position = new Vector3(-xBoundary, player.transform.position.y, player.transform.position.z);
        else if (player.transform.position.x <= -xBoundary)
            player.transform.position = new Vector3(xBoundary, player.transform.position.y, player.transform.position.z);
    }

    public void GameOver(Player winner)
    {
        gameOverText.enabled = true;
        gameOverText.text = "Game Over\n" + winner.color + " Wins!";
        bluePlayer.Stop();
        redPlayer.Stop();
        Invoke("RestartLevel", 5);
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

    public void PlayerDied(Player player)
    {
        switch (gameMode)
        {
            case GameConfig.GameMode.Battle:
                {
                    player.otherPlayer.IncrementScore();
                }
                break;
            case GameConfig.GameMode.Tag:
                {
                    player.DecrementScore();
                }
                break;
        }
    }
    
    void RestartLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
