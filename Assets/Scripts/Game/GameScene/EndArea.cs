using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            WinPanel.Instance.ShowMe();
        }
    }
}
