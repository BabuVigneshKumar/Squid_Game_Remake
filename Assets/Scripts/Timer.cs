using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField]
    private float initialTime = 60f;
    private float CurrentTime;

    
    void Start()
    {
        CurrentTime = initialTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(CurrentTime);
            timerText.text = timeSpan.ToString(@"mm\:ss");    
            return;
        }
        else
        {

        }
    }
}
