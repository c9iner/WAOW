using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string color;
    public Text scoreText;
    public Text gameOverText;
    public Player otherPlayer;
    public Object effectPrefab;
    public GameObject respawnPlatform;

    GameManager gameManager;
    int score = 0;
    Vector3 startPosition;
    GameObject effect;
    Renderer render;
    Collider2D collide;
    SpriteGlow spriteGlow;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        render = GetComponent<Renderer>();
        collide = GetComponent<Collider2D>();
        spriteGlow = GetComponent<SpriteGlow>();
        startPosition = transform.position;
        Invoke("HideRespawnPlatform", 5);
        Invoke("EnableMovement", 5);

    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "InstantDeath")
            Die();

        if (collision.gameObject.tag == "Player")
            gameManager.TagPlayer(collision.gameObject.GetComponent<Player>());
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == respawnPlatform)
            HideRespawnPlatform();
    }

    void Die()
    {
        render.enabled = false;
        collide.enabled = false;
        effect = Instantiate(effectPrefab) as GameObject;
        effect.transform.position = transform.position;
        otherPlayer.IncrementScore();

        // Wait 5 seconds and respawn
        Invoke("Respawn", 3);
    }

    void Respawn()
    {
        respawnPlatform.SetActive(true);
        Invoke("HideRespawnPlatform", 5);
        Destroy(effect);
        transform.position = startPosition;
        render.enabled = true;
        collide.enabled = true;
    }

    public void IncrementScore()
    {
        score += 1;
        scoreText.text = score.ToString();

        if (score == 10)
        {
            gameOverText.enabled = true;
            gameOverText.text = "Game Over\n" + color + " Wins!";
            EnableMovement();
            otherPlayer.EnableMovement();
            Invoke("RestartLevel", 5);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Level_Lava");
    }

    void HideRespawnPlatform()
    {
        respawnPlatform.SetActive(false);
    }

    public void EnableMovement()
    {
        GetComponent<Move>().enabled = true;
    }

    public void Glow(bool glow)
    {
        spriteGlow.enabled = glow;
    }

    public void Pulse(float seconds)
    {
        StartCoroutine(PulseEnumerator(seconds));
    }

    private IEnumerator PulseEnumerator(float seconds)
    {
        float[] hdrColors = { spriteGlow.GlowColor.r, spriteGlow.GlowColor.g, spriteGlow.GlowColor.b };
        float maxBrightness = Mathf.Max(hdrColors);
        float elapsedTime = 0;
        float pulsePeriod = 0.2f;
        while (elapsedTime < seconds)
        {
            elapsedTime += Time.deltaTime;
            float wave = elapsedTime / pulsePeriod * Mathf.PI;
            float brightness = (Mathf.Sin(wave) + 1) / 2;

            spriteGlow.GlowColor.r = Mathf.Lerp(hdrColors[0], hdrColors[0] / maxBrightness, brightness);
            spriteGlow.GlowColor.g = Mathf.Lerp(hdrColors[1], hdrColors[1] / maxBrightness, brightness);
            spriteGlow.GlowColor.b = Mathf.Lerp(hdrColors[2], hdrColors[2] / maxBrightness, brightness);
            
            yield return null;
        }

        yield break;
    }
}
