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

    int score = 0;
    Vector3 startPosition;
    GameObject effect;
    Renderer render;
    Collider2D collide;

    // Use this for initialization
    void Start()
    {
        render = GetComponent<Renderer>();
        collide = GetComponent<Collider2D>();
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
}
