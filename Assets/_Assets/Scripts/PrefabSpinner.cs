using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpinner : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Spin the object around the y-axis at a given speed.
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
