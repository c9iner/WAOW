using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public int score = 0;
    public string color;
    public Text scoreText;
    public Player otherPlayer;
    public Object effectPrefab;
    public GameObject respawnPlatform;

    GameManager gameManager;
    Vector3 startPosition;
    GameObject effect;
    Renderer render;
    Collider2D collide;
    SpriteGlow spriteGlow;
    bool isStopped = false;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        render = GetComponent<Renderer>();
        collide = GetComponent<Collider2D>();
        spriteGlow = GetComponent<SpriteGlow>();
        startPosition = transform.position;
    }

    void Start()
    {
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
        gameManager.PlayerDied(this);
        Invoke("Respawn", 3);
    }

    void Respawn()
    {
        if (isStopped)
            return;

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
        SetScore(score);
    }

    public void DecrementScore()
    {
        score -= 1;

        if (score < 0)
            score = 0;

        SetScore(score);
    }

    public void SetScore(int s)
    {
        score = s;
        scoreText.text = score.ToString();
    }

    void HideRespawnPlatform()
    {
        respawnPlatform.SetActive(false);
    }

    public void EnableMovement()
    {
        GetComponent<Move>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    public void Stop()
    {
        isStopped = true;
        GetComponent<Move>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
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
