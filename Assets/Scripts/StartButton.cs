using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var config = GameObject.FindGameObjectWithTag("GameConfig").GetComponent<GameConfig>();
            SceneManager.LoadScene("Level" + config.level.ToString());
        }
    }
}
