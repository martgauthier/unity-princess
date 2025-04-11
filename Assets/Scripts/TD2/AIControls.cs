using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Windows;

public class AIControls : MonoBehaviour
{
    private Vector2 input;
    public UnityEvent<Vector2> onInput;

    public Transform waypointsHolder;
    private List<Transform> waypoints;
    private Transform nextWaypoint;
    private Vector3 nextWaypointPosition;

    public float maxDistanceToTarget = 5f;
    public float maxDistanceToReverse = 10f;
    public float randomJitterOnPosition = .5f;

    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    public float stuckCheckInterval = 2f;
    public float minDistanceMoved = 0.5f;

    private bool isUnstucking = false;

    public Animator voileAnimator;
    public Animator fanAnimator;

    private bool shouldDrive;

    void Awake()
    {
        waypoints = new List<Transform>();
        waypoints.AddRange(waypointsHolder.GetComponentsInChildren<Transform>().Skip(1));
    }

    void Start()
    {
        SelectWaypoint(waypoints[0]);
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!isUnstucking)
        {
            float distanceToTarget = Vector3.Distance(transform.position, nextWaypointPosition);
            if (distanceToTarget < maxDistanceToTarget)
            {
                int nextIndex = waypoints.IndexOf(nextWaypoint) + 1;
                SelectWaypoint(nextIndex < waypoints.Count ? waypoints[nextIndex] : waypoints[0]);
            }

            Vector3 diff = nextWaypointPosition - transform.position;
            float componentForward = Vector3.Dot(diff, transform.forward.normalized);
            float componentRight = Vector3.Dot(diff, transform.right.normalized);
            input = new Vector2(componentRight, componentForward).normalized;

            // Si la cible est trop loin derrière
            if (componentForward < -0.2f && distanceToTarget > maxDistanceToReverse)
            {
                Debug.Log($"{gameObject.name} doit faire demi-tour");
                input = new Vector2(Mathf.Sign(componentRight), -1f);
            }

            // Check si bloqué
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckCheckInterval)
            {
                float moved = Vector3.Distance(transform.position, lastPosition);
                if (moved < minDistanceMoved)
                {
                    Debug.Log($"{gameObject.name} est bloqué, recalcul...");
                    StartCoroutine(TemporarilyForceUnstuck());
                }
                stuckTimer = 0f;
                lastPosition = transform.position;
            }
            //  Anti-glissade contre les murs
            Vector3 toTarget = (nextWaypointPosition - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, toTarget);

// Si angle trop grand (avance en biais) mais la distance ne diminue pas => agir
            if (angle > 70f && distanceToTarget > maxDistanceToTarget * 1.2f)
            {
                Debug.Log($"{gameObject.name} glisse en biais, recentrage...");
                input = new Vector2(Mathf.Sign(componentRight), -1f); // tourne + recule
            }

            if (input.y > 0 && !shouldDrive)
            {
                shouldDrive = true;
                voileAnimator.SetTrigger("gonfle_on");
                fanAnimator.SetTrigger("ventilateur_on");
            }
            else if(shouldDrive && input.y == 0)
            {
                shouldDrive = false;
                voileAnimator.SetTrigger("gonfle_off");
                fanAnimator.SetTrigger("ventilateur_off");
            }


            onInput?.Invoke(input);
        }
    }

    private IEnumerator TemporarilyForceUnstuck()
    {
        isUnstucking = true;
        float t = 0f;

        // 🔁 Calcul de la direction vers le waypoint
        Vector3 toTarget = (nextWaypointPosition - transform.position).normalized;

        // 🔁 Produit vectoriel pour savoir si le waypoint est à gauche ou à droite
        float turnDirection = Vector3.Cross(transform.forward, toTarget).y > 0 ? 1f : -1f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            input = new Vector2(turnDirection, -1f); // tourne dans la bonne direction en reculant
            onInput?.Invoke(input);
            yield return null;
        }

        isUnstucking = false;
    }

    void SelectWaypoint(Transform waypoint)
    {
        nextWaypoint = waypoint;
        nextWaypointPosition = nextWaypoint.position + new Vector3(
            UnityEngine.Random.Range(-randomJitterOnPosition, randomJitterOnPosition),
            0,
            UnityEngine.Random.Range(-randomJitterOnPosition, randomJitterOnPosition)
        );
    }
}
