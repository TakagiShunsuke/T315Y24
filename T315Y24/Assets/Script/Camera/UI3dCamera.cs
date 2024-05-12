using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3dCamera : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
