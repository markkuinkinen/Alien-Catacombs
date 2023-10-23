using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timer = 0f;
    public Text timerText;

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }
}
