using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float RotateSpeed = 1;

    void Update()
    {
        transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);
    }
}
