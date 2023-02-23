using PatrolBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.ActionState
{
    /// <summary>
    /// A state where the enemy move between point in a sequence.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class Patrol : State
    {
        [Header("Patrol parameters")]
        public NavMeshAgent agent;
        public PatrolRoute route;
        public int actualPoint = 0;

        public bool reverse = false;

        private float timeFromStart;
        public float maxTimeBeforeIdle = 10f;
        [Range(0f,10f)]
        public float randomIdleTime = 5f;

        /// <summary>
        /// Enters the state Patrol. The enemy will get its component NavMeshAgent and get the first point of destination
        /// </summary>
        public override void Enter()
        {
            agent = GetComponent<NavMeshAgent>();
            timeFromStart = 0f;
            if(route == null) route = GetRoute();
            actualPoint = route.GetClosestWaypointId(agent.transform.position);
            agent.SetDestination(route.waypoints[actualPoint].position);
        }

        /// <summary>
        /// Updates the state Patrol. The enemy will search if he is next to actual point and ask for next point of his route
        /// </summary>
        public override void UpdateState()
        {
            if (agent.remainingDistance <= 0.1f)
            {
                actualPoint = reverse ? (actualPoint + route.waypoints.Length - 1) % route.waypoints.Length : (actualPoint + 1) % route.waypoints.Length;
                agent.SetDestination(route.waypoints[actualPoint].position);
            }
            Debug.DrawLine(agent.transform.position, route.waypoints[actualPoint].position, Color.white);

            timeFromStart += Time.deltaTime;

            //Verif for next state
            IdleStateVerif();
            ChaseStateVerif();

        }
        public override void Exit()
        {
            nextState = null;
            timeFromStart = 0f;
        }

        public PatrolRoute GetRoute()
        {
            var routes = FindObjectsOfType<PatrolRoute>();
            List<Transform> wps = new List<Transform>();
            foreach(var r in routes) 
            {
                 int i = r.GetClosestWaypointId(transform.position);
                wps.Add(r.waypoints[i]);
            }
            float closestDistance = Mathf.Infinity;
            Transform finalWP = null;
            foreach (var way in wps)
            {
                float dist = Vector3.Distance(transform.position, way.position);
                if(dist < closestDistance)
                {
                    closestDistance = dist;
                    finalWP = way;
                }
            }
            PatrolRoute finalR = null;
            int a = 0;
            while(finalR == null)
            {
                foreach(var w in routes[a].waypoints)
                {
                    if (finalWP == w)
                    {
                        finalR = routes[a];
                        break;
                    }
                }
                a++;
            }
            return finalR;
        }



        private void ChaseStateVerif()
        {
            if (lineSight.GetViewPlayer())
            {
                nextState = GetComponent<ChasePlayer>();
            }
        }
        private void IdleStateVerif()
        {
            if(timeFromStart >= (maxTimeBeforeIdle + Random.Range(0, randomIdleTime)))
            {
                nextState = GetComponent<Idle>();
            }
        }

    }

}

