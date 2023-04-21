﻿using System.Collections;
using UnityEngine;

public class ObjectiveReachTargets : Objective
{
    IEnumerator Start()
    {
   
        TimeManager.OnSetTime(totalTimeInSecs, isTimed, gameMode);
        
        yield return new WaitForEndOfFrame();

        title = "The game ends when the timer hits zero";
        
        Register();
    }

    protected override void ReachCheckpoint(int remaining)
    {
        
    }
}
