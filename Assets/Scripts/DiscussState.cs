using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussState : IBaseGuardState
{
    float distanceToFindPlayer = 3;
    float discussDuration = 3;
    GuardStateMachine stateMachine;
    public void OnEnter(GuardStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        _stateMachine.StartCoroutine(GuardDiscuss());
    }

    public void OnExit()
    {

    }

    //Si il voit le joueur il va aller le poursuivre
    public void Update()
    {
        if (Vector3.Distance(stateMachine.player.position, stateMachine.transform.position) <= distanceToFindPlayer)
        {
            stateMachine.Transition(stateMachine.pursuitState);
        }

        else
        {
            stateMachine.agent.destination = stateMachine.transform.position;
        }
    }

    //Il va discuter avec un autre garde pendant quelques secondes
    public IEnumerator GuardDiscuss()
    {
        yield return new WaitForSeconds(discussDuration);
        if(stateMachine.currentState == this)
        {
            stateMachine.Transition(stateMachine.patrolState);
        }
    }
}
