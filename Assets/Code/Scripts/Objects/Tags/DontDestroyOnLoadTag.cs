using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadTag : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
