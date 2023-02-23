using PatrolBehavior;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoutesController : MonoBehaviour
{
    public GameObject routePrefab;
    public int nbRoute = 4;

    private List<Transform> pointes = new List<Transform>();

    private GameObject routeInitiate;
    private PatrolRoute patrol;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] gObject = GameObject.FindGameObjectsWithTag("PatrolPoint");
        pointes = gObject.Select(x => x.transform).ToList();

        for (int i = 0; i < nbRoute; i++)
        {
            routeInitiate = Instantiate(routePrefab);
            patrol = routeInitiate.GetComponent<PatrolRoute>();

            patrol.SetMaxPoint();
            patrol.waypoints = new Transform[patrol.GetMaxPoint()];

            patrol.waypoints[0] = pointes[Random.Range(0, pointes.Count-1)];
            pointes.Remove(patrol.waypoints[0]);

            for (int j = 1; j < patrol.GetMaxPoint(); j++)
            {
                if (pointes.Count <= 0) break;
                patrol.waypoints[j] = GetClosestWaypoint(j-1, pointes);
                pointes.Remove(patrol.waypoints[j]);
            }
            patrol.waypoints = patrol.waypoints.Where(x => x != null).ToArray();
        }
    }

    /// <summary>
    /// Gets the closest waypoint.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <returns>A Transform.</returns>
    public Transform GetClosestWaypoint(int lastPoint,List<Transform> points)
    {
        float closestDistance = Mathf.Infinity;
        Transform closestWaypoint = null;
        for (var i = 0; i < points.Count - 1; i++)
        {
            float dist = Vector3.Distance(patrol.waypoints[lastPoint].position, points[i].position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestWaypoint = points[i];
            }
        }

        return closestWaypoint;
    }
}
