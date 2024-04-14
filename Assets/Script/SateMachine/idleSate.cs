using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleSate : ISate
{
    float time;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        time = 0;
        randomTime = Random.Range(2.5f, 6);
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if (time > randomTime) 
        {
            enemy.ChangeSate(new patrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }

  
}
