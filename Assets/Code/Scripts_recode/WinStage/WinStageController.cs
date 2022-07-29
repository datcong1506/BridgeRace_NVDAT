using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinStageController : MonobehaviourSingletonInterface<WinStageController>
{

    [SerializeField] private Transform _top1;
    [SerializeField] private Transform _top2;
    [SerializeField] private Transform _top3;

    [SerializeField] private GameObject _winEffect;
    
    public void OnEndGame()
    {
        var humans = LevelSystem.Singleton.Enemy;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Singleton.State == (int) GameState.End)
        {
            return;
        }
        // only human have statecontroller
        if (other.TryGetComponent < StateController>(out var controller))
        {
            var humans = LevelSystem.Singleton.Enemy;
            var sortByDistance = from human in humans
                orderby Vector3.Magnitude(human.transform.position - transform.position)
                select human;

            var targetHumans = 2;


            if (sortByDistance.ElementAt(0).TryGetComponent<IWinable>(out var win1))
            {
                win1.Win(_top1,1);
            }
            if (sortByDistance.ElementAt(1).TryGetComponent<IWinable>(out var win2))
            {
                win2.Win(_top2,2);
            }
            if (sortByDistance.ElementAt(2).TryGetComponent<IWinable>(out var win3))
            {
                win3.Win(_top3,2);
            }

            GameManager.Singleton.State = (int) GameState.End;
            _winEffect.SetActive(true);
        }
    }
}
