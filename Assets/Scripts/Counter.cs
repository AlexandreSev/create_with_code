using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    public GameObject YouWin;

    public GameObject timer;

    private int Count = 0;

    private void Start()
    {
        Count = 0;
        YouWin.SetActive(false);
        timer.GetComponent<Timer>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent < PlayerController > ().Respawn();
            return;
        }

        Count += 1;
        CounterText.text = "Count : " + Count;

        if (other.CompareTag("Barrel"))
        {
            ObjectPooler.Instance.Destroy("Barrel", other.gameObject);
        }

        if (Count == 50)
        {
            YouWin.SetActive(true);
            timer.GetComponent<Timer>().Stop();
            HighScores.Instance.AddNewScore(timer.GetComponent<Timer>().GetTime());
        }
    }
}
