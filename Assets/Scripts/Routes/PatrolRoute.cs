using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PatrolBehavior
{
    public class PatrolRoute : MonoBehaviour
    {
        public Transform[] waypoints;
        /// <summary>
        /// Gets or sets the max waypoints. Take a random value between 0 and randRange adding the maxWaypoints base value.
        /// </summary>
        private int maxWaypoints = 0;
        public int _maxWaypoints = 4;
        [Range(0,10)]
        public int randRange = 4;

        public int GetClosestWaypointId(Vector3 position)
        {
            float closestDistance = Mathf.Infinity;
            int closestWaypointId = 0;
            for (var i = 0; i < waypoints.Length - 1; i++)
            {
                float dist = Vector3.Distance(position, waypoints[i].position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestWaypointId = i;
                }
            }
            return closestWaypointId;
        }

        private void OnDrawGizmos()
        {
            if(waypoints != null)
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(waypoints[i%waypoints.Length].position, waypoints[(i + 1)%waypoints.Length].position);
                }
            }
            
        }

        public void SetMaxPoint()
        {
            maxWaypoints = _maxWaypoints  + Random.Range(0, randRange+1);
        }

        public int GetMaxPoint()
        {
            return maxWaypoints;
        }
    }

}

