using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "GameData",menuName = "Data/GameData")]
public class GameData : ScripableSingletonInterface<GameData>
{
    [SerializeField] private string _playerName;
    public string PlayerName
    {
        get
        {
            return _playerName;
        }
        set
        {
            if (value != _playerName)
            {
                _playerName = value;
                OnChangeNameEvent?.Invoke(_playerName);
            }
        }
    }
    public UnityEvent<string> OnChangeNameEvent = new UnityEvent<string>();


    [SerializeField] private int _currentlevel = 0;

    public int CurrentLevel
    {
        get
        {
            return _currentlevel;
        }
        set
        {
            if (value != _currentlevel)
            {
                if (value >= 0 && value < SceneManager.sceneCountInBuildSettings)
                {
                    _currentlevel = value;
                }
            }
        }
    }
}
