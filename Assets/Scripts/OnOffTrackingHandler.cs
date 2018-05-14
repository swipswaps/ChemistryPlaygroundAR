using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class OnOffTrackingHandler : DefaultTrackableEventHandler
{

    protected override void Start()
    {
        base.Start();
        OnTrackingLost();
    }

    protected override void OnTrackingFound()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    protected override void OnTrackingLost()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
