using UnityEngine;
using UnityEngine.AI;

namespace FSM.ActionState
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class ChasePlayer : State
    {
        [Header("Chase parameters")]
        public Transform target;
        public float pathRefreshDuration = 0.2f;
        public bool debugDestination;
    
        private NavMeshAgent agent;
        private float nextRefreshTime;

        private Vector3 lastSeenPosition;


        public override void Enter()
        {
            agent = GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void UpdateState()
        {
            if (Time.time >= nextRefreshTime)
            {
                nextRefreshTime = Time.time + pathRefreshDuration;
                RefreshPath();
            }

            if (Application.isEditor && debugDestination)
            {
                Debug.DrawLine(transform.position, agent.destination, Color.black);
            }

            //All verification for next state
            PatrolStateVerif();
        }

        private void RefreshPath()
        {
            if (lineSight.GetViewPlayer())
            {
                agent.SetDestination(target.position);
            }
            
        }

        public override void Exit()
        {
            nextState = null;
            target = null;
        }


        /// <summary>
        /// Verify if the next state will be Patrol based on the line of sight and seeing player.
        /// </summary>
        private void PatrolStateVerif()
        {
            if (!lineSight.GetViewPlayer() && agent.remainingDistance <= 0.1f)
            {
                nextState = GetComponent<Patrol>();
            }
        }
    }
}
