using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;

    public float H = 20;
    Vector3 temp;
    private void Update()
    {
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            H -= 0.1f;
        }
        else if(Input.GetKey(KeyCode.KeypadMinus))
        {
            H += 0.1f;
        }
    }

    private void LateUpdate()
    {
        if(player != null)
        {
            temp.x = player.position.x;
            temp.y = H;
            temp.z = player.position.z;

            transform.position = temp;
        }
    }
}
