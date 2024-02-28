using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclesScript : MonoBehaviour
{
    public float speed = 1.0f;

    void Update()
    {
        float translation = speed * Time.deltaTime;
        transform.Translate(Vector3.left * translation);
    }
}
