using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text text;
    public float elapsedTime;
    [HideInInspector] public static float finalTime;

    public static bool timerStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStopped)
        {
            elapsedTime += Time.deltaTime;
        }
        if (timerStopped)
        {
            finalTime = elapsedTime;
        }
        // Update the text with the current elapsed time
        text.text = elapsedTime.ToString("F2"); // formats to two decimals
    }

    public float GetTime()
    {
        return elapsedTime;
    }


    public static float GetEndTime()
    {
        return finalTime;
    }

    public static void EndTimer()
    {
        // on death
        timerStopped = true;
    }

}
