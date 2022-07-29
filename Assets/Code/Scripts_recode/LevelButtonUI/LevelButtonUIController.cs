using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButtonUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public void SetLevelName(string newName)
    {
        _textMeshProUGUI.text = newName;
    }
}
