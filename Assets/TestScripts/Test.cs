using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float value;
    [ContextMenu("timescale")]
    public void SetTimeScale()
    {
        Time.timeScale = value;
    }
}
