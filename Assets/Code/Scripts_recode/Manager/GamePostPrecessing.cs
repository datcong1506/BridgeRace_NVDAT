using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GamePostPrecessing : MonobehaviourSingletonInterface<GamePostPrecessing>
{
    private Volume _volume;

    private Vignette _vignette;

    private void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
    }

    public void TurnOnVignette()
    {
        _vignette.active = true;
    }

    public void TurnOffVignette()
    {
        _vignette.active = false;
    }
    
    
}
