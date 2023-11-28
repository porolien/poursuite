using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IBaseGuardState
{
    float distanceToSeeTheHeardArea = 2;
    float distanceToFindPlayer = 2;
    GuardStateMachine stateMachine;
    public Vector3 noiceHeard;
    public void OnEnter(GuardStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public void OnExit()
    {

    }

    //L'ennemie va vérifier si il voit le joueur,
    //puis il vérifie si il est arriver dans la zone du son, sinon il va aller vers le bruit
    public void Update()
    {
        if (Vector3.Distance(stateMachine.player.position, stateMachine.transform.position) <= distanceToFindPlayer)
        {
            stateMachine.Transition(stateMachine.pursuitState);
        }
        else if (Vector3.Distance(noiceHeard, stateMachine.transform.position) <= distanceToSeeTheHeardArea)
        {
            stateMachine.StartCoroutine(WaitInTheHeardArea());
        }
        else
        {
            stateMachine.agent.destination = noiceHeard;
        }
    }

    //Permet d'attendre quelques secondes avant de patrouiller
    public IEnumerator WaitInTheHeardArea()
    {
        yield return new WaitForSeconds(3);
        if (stateMachine.currentState == this)
        {
            stateMachine.Transition(stateMachine.patrolState);
        }
    }
}
