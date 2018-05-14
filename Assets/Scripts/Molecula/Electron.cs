using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour {

    public Transform target;
    public float speed = 5;

	void Start () {
        transform.localPosition = Random.onUnitSphere;
        transform.LookAt(target);
    }
	
	void Update () {
        transform.RotateAround(target.position, transform.up, 90 * speed * Time.deltaTime);
    }
}
