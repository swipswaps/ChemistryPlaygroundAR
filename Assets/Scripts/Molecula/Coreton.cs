using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coreton : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public float radius = 0.3f;

    void Start()
    {
        transform.localPosition = Random.onUnitSphere * radius;
        speed = Random.Range(0.5f, 2);
        transform.LookAt(target);
    }

    void Update()
    {
        transform.RotateAround(target.position, transform.up, 90 * speed * Time.deltaTime);
    }
}
