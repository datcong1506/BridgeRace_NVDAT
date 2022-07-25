using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBetakeable
{
    public void OnBetake(GameObject from,StackController stackController);
}
