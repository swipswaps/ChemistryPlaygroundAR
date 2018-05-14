using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculaTestController : MonoBehaviour
{

    [Blend2D]
    public Vector2 fakePositionXY;

    [SerializeField]
    private float Multiplier = 10;

    public string instanceName;

    private GameObject instance;

    // Use this for initialization
    void Start()
    {
        instance = Instantiate(Resources.Load<GameObject>(string.Format("Prefabs/{0}", instanceName)));
    }

    // Update is called once per frame
    void Update()
    {
        instance.transform.position = new Vector3(fakePositionXY.x * Multiplier - Multiplier / 2, instance.transform.position.y, fakePositionXY.y * Multiplier - Multiplier / 2);
    }
}
