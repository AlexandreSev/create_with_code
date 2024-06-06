using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timeSinceBeginning = 0f;
    public Text timeText;

    private bool timeRunning;
    // Start is called before the first frame update
    void Start()
    {
        timeRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning)
        {
            timeSinceBeginning += Time.deltaTime;
        }
        DisplayTime();
    }

    public void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeSinceBeginning / 60);
        float seconds = Mathf.FloorToInt(timeSinceBeginning % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Stop()
    {
        this.timeRunning = false;
    }

    public float GetTime()
    {
        return timeSinceBeginning;
    }
}
