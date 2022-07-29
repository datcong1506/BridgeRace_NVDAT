using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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
}