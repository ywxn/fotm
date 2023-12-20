/*

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour // not used anymore!
{
    public Transform player;
    public float circleDistance = 3f;
    public float radius = 5f;
    public float approachSpeed = 5f;

    public float fadeDuration = 0.75f;

    public Material material;
    public MeshRenderer mr;
    string propertyName = "Fade";

    public GameObject xpObject;

    private enum State
    {
        Approach,
        Circle
    }

    private State currentState;

    private void Start()
    {
        currentState = State.Approach;
        mr = GetComponentInChildren<MeshRenderer>();
        material = mr.material;

    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Approach:
                ApproachPlayer();
                break;

            case State.Circle:
                CirclePlayer();
                break;
        }
    }

    private void ApproachPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, approachSpeed * Time.deltaTime);
        transform.LookAt(player.position);
        if (Vector3.Distance(transform.position, player.position) < circleDistance)
        {
            currentState = State.Circle;
            return;
        }
    }

    private void CirclePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > circleDistance)
        {
            currentState = State.Approach;
            return;
        }

        Vector3 dirToPlayer = player.transform.position - transform.position;
        Vector3 tangent = Vector3.Cross(dirToPlayer.normalized, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tangent, approachSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
*/