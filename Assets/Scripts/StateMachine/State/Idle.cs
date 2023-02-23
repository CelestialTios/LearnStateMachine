
using PatrolBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.ActionState
{
    public class Idle : State
    {
        public float delayIdle = 3f;
        public float maxRandom = 2f;
        private float timeRemaining;
        private NavMeshAgent agent;

        public override void Enter()
        {
            timeRemaining = delayIdle + Random.Range(0,maxRandom);
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = true;
        }

        public override void Exit()
        {
            timeRemaining = delayIdle;
            nextState = null;
            agent.isStopped = false;
        }

        public override void UpdateState()
        {
            if( timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
            }
            if( timeRemaining <= 0f )
            {
                nextState = GetComponent<Patrol>();
            }
        }
    }

}

