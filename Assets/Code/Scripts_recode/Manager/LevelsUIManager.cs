using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsUIManager : MonobehaviourSingletonInterface<LevelsUIManager>
{
    [SerializeField] private GameObject _playableScene;
    [SerializeField] private GameObject _unPlayableScene;

    [SerializeField] private Transform _container;
    private void Start()
    {
        var totalLevel = SceneManager.sceneCountInBuildSettings;
        var currentLevel = GameData.Singleton.CurrentLevel;

        for (int i = 0; i <= currentLevel; i++)
        {
            AddNewLevelToUi(_playableScene, i);
        }

        for (int i = currentLevel + 1; i < totalLevel; i++)
        {
            AddNewLevelToUi(_unPlayableScene, i);
        }
    }

    private void AddNewLevelToUi(GameObject levelUI,int i)
    {
        var newlevelUI = Instantiate(levelUI,_container);
        if (newlevelUI.TryGetComponent<LevelButtonUIController>(out var button))
        {
            button.SetLevelName(SceneManager.GetSceneAt(i).name);
        }

        if (newlevelUI.TryGetComponent<Button>(out var newLevelButton))
        {
            var iCopy = i;
            newLevelButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(i);
            });
        }
    }
}
