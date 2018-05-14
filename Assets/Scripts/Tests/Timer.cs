using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour {

    public int seconds;
    public event Action<int> timerStepAction;
    public event Action timerEndAction;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartTimerCoroutine(seconds));
	}

    private IEnumerator StartTimerCoroutine(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            if (timerStepAction != null)
            {
                timerStepAction(i);
            }
            yield return new WaitForSeconds(1);
        }
        if (timerEndAction != null)
        {
            timerEndAction();
        }
        Destroy(this);
    }
}
