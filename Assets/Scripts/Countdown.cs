using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public float timer = 5;

    private Text text;
    private bool isExpired = false;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        text.text = timer.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            text.text = ((int)timer + 1).ToString();
        }
        else if (timer <= 0 && timer > -1)
        {
            text.text = "GO!";
            isExpired = true;
        }
        else
        {
            text.enabled = false;
        }
    }

    public bool IsExpired()
    {
        return isExpired;
    }
}
