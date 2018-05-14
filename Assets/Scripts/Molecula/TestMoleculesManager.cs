#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoleculesManager : MonoBehaviour
{

    [InspectorButton("AddHydrogen")]
    public bool addH;
    [InspectorButton("AddOxygen")]
    public bool addO;
    [InspectorButton("AddCarbon")]
    public bool addC;
    [InspectorButton("Remove")]
    public bool RemoveMolecula;

    // Update is called once per frame
    public void AddHydrogen()
    {
        Add("AtomHydrogen");
    }

    public void AddOxygen()
    {
        Add("AtomOxygen");
    }

    public void AddCarbon()
    {
        Add("AtomCarbon");
    }

    private void Add(string assetName)
    {
        if (Application.isPlaying)
        {
            var testController = gameObject.AddComponent<MoleculaTestController>();
            testController.instanceName = assetName;
            testController.fakePositionXY.Set(Random.Range(0, 1), Random.Range(0, 1));
        }
    }

    public void Remove()
    {
        if (Application.isPlaying)
        {
            var components = GetComponents<MoleculaTestController>();
            if (components.Length > 0)
            {
                Destroy(components[0]);
            }
        }
    }
}
#endif