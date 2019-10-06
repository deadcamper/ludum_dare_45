using System;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    public float secondsToLive = 5f;
    float timeCount = 0f;

    public event Action OnTimeRunOut = delegate { };

    public float TimeLeft {
        get
        {
            return secondsToLive - timeCount;
        }
    }

    public void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > secondsToLive)
        {
            OnTimeRunOut();
            Destroy(gameObject);
        }
    }
}
