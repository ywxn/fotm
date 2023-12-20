using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float xpValue = 2f;
    public float health = 5f;

    private GameObject player;
    public float circleDistance = 3f;
    public float radius = 5f;
    public float spinSpeed = 180f; // Adjust the spin speed as needed

    public float fadeDuration = 0.75f;

    public Material material;
    public MeshRenderer mr;
    readonly string propertyName = "Fade";

    public GameObject xpObject;

    private AudioSource s;
    public AudioClip near;

    public float approachSpeed;
    public bool tookDamage = false;

    public AudioClip deathSFX;

    public virtual float Health
    {
        get { return health; }
        set { health = value; }
    }
    public virtual float XP
    {
        get { return xpValue; }
        set { xpValue = 1f; }
    }

    protected enum State
    {
        Approach,
        Circle
    }

    protected State currentState;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        s = GetComponent<AudioSource>();
        currentState = State.Approach;
        mr = GetComponentInChildren<MeshRenderer>();
        material = mr.material;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Approach:
                ApproachPlayer();
                break;

            case State.Circle:
                CirclePlayer();
                s.PlayOneShot(near);
                break;
        }
    }

    protected virtual void ApproachPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        transform.LookAt(player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) < circleDistance)
        {
            currentState = State.Circle;
            return;
        }
    }

    protected private virtual void CirclePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > circleDistance)
        {
            currentState = State.Approach;
            return;
        }

        Vector3 dirToPlayer = player.transform.position - transform.position;
        Vector3 tangent = Vector3.Cross(dirToPlayer.normalized, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tangent, approachSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    public virtual void TakeDamage() //this doesnt work for some fucking reason and i want to die
    {
        if (!tookDamage)
        {
            health--;
        }
        //Debug.Log($"Current health after taking damage: {health}");
        tookDamage = true;
        if (health <= 0f)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        StartCoroutine(FadeCoroutine());
        GameObject xpBall = Instantiate(xpObject, transform.position, Quaternion.identity);
        xpBall.GetComponent<XPObject>().Initialize(player, xpValue);
        s.PlayOneShot(deathSFX);
        Destroy(gameObject);
    }

    protected virtual void GG()
    {
        Timer.EndTimer();
        SceneManager.LoadScene("Score");
    }

    protected IEnumerator FadeCoroutine()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float targetValue = 1f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            float currentValue = Mathf.Lerp(startValue, targetValue, t);

            material.SetFloat(propertyName, currentValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            GG();
        }
        if (collision.gameObject.CompareTag("B"))
        {
            TakeDamage();
        }
    }
}
