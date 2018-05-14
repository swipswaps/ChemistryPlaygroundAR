using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Binding : MonoBehaviour {

    public AtomView first;
    public AtomView second;
    public int electronesCount;
	
    void Start()
    {
        var electronGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Electron"), transform);
        electronGameObject.GetComponentInChildren<TrailRenderer>().time = 2;
        var height = Random.Range(0.7f, 2);
        electronGameObject.transform.DOShakePosition(1, strength:0.5f, vibrato:3, fadeOut: false).SetLoops(-1);
    }

	// Update is called once per frame
	void FixedUpdate () {
        var diff = first.transform.position - second.transform.position;
        transform.position = Vector3.Lerp(first.transform.position + Vector3.ClampMagnitude(diff, 0.4f), second.transform.position - Vector3.ClampMagnitude(diff, 0.4f), Mathf.Sin(Time.time * 2) / 2 + 0.5f);
        transform.LookAt(first.transform);
	}
}
