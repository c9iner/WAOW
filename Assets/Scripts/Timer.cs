using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float duration;
    
    private bool isExpired = false;
    private bool isStopped = false;
    private Text text;
    private float currentTime;

	// Use this for initialization
	void Start () {
        currentTime = duration;

        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isStopped)
            return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            isExpired = true;
        }

        int minutes = (int)currentTime / 60;
        string sMinutes = minutes > 9 ? minutes.ToString() : "0" + minutes.ToString();

        int seconds = (int)currentTime % 60;
        string sSeconds = seconds > 9 ? seconds.ToString() : "0" + seconds.ToString();
        
        text.text = sMinutes + ":" + sSeconds;
        
    }

    public bool IsExpired()
    {
        return isExpired;
    }

    public void Stop()
    {
        isStopped = true;
    }
}
