using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponPickup : MonoBehaviour
{
    public float floatHeight = 0.5f;
    public float floatSpeed = 1.0f;
    public float rotationSpeed = 30.0f;

    private Vector3 startPosition;

    public SceneTransition sceneTransition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Floating movement using easing function
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotation
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Game");
    }
}