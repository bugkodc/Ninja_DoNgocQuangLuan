using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolState : ISate
{
    float time;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        time = 0;
        randomTime = Random.Range(2.5f, 6);
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if(enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeSate(new attackState());
            }
            else
            {
                enemy.Moving();
            }
        }
        else
        {
            if (time < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeSate(new idleSate());
            }

        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }

   
}
