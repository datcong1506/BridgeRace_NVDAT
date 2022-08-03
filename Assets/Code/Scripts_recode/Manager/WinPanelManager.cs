using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanelManager : MonobehaviourSingletonInterface<WinPanelManager>
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button endTriggerButton;


   
    
    
    private void OnEnable()
    {
        var sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        var countScene = SceneManager.sceneCountInBuildSettings;
        endTriggerButton.onClick.RemoveAllListeners();
        if (sceneBuildIndex == countScene - 1)
        {
            _buttonText.text = "Back To Menu";
            endTriggerButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });   
        }
        else
        {
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(sceneBuildIndex+1);
            string nextLevelName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
            _buttonText.text = nextLevelName;
            var nextBuildIndex = sceneBuildIndex + 1;
            endTriggerButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(nextBuildIndex);
            });
        }
    }
}
