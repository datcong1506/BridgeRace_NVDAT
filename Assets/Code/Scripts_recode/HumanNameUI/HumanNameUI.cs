using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanNameUI : MonoBehaviour
{
    [SerializeField]private TextMeshPro _textMeshProUGUI;
    private Camera _camera;

    private void Start()
    {
        _textMeshProUGUI.text = 
            GameData.Singleton.PlayerName;
        _camera = Camera.main;
        GameData.Singleton.OnChangeNameEvent.AddListener(SetName);
    }

    protected void SetName(string name)
    {
        _textMeshProUGUI.text = name;
    }

    
    

}
