using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public LevelManager levelManager;
    private float fireRate;

    public GameObject gun;
    public GameObject bulletPrefab;

    private float shootTime;

    public AudioSource s;

    public AudioClip shootSFX;


    private Vector3 originalGunPosition;
    private float recoilDistance = 0.1f;  // Adjust the recoil distance as needed

    // Start is called before the first frame update
    void Start()
    {
        gun = gameObject;
        originalGunPosition = gun.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        fireRate = levelManager.fireRate;

        if (Input.GetMouseButton(0) && Time.time - shootTime >= 1f / fireRate)
        {
            Vector3 spawnPosition = gun.transform.position + gun.transform.forward * 1.5f;
            Shoot(spawnPosition);

            // Apply recoil to the gun
            StartCoroutine(Recoil());

            shootTime = Time.time;  // Update shootTime after shooting
        }

        // You can add additional logic or behaviors here
    }

    void Shoot(Vector3 spawnPosition)
    {
        s.PlayOneShot(shootSFX);
        Instantiate(bulletPrefab, spawnPosition, gun.transform.rotation);
    }

    IEnumerator Recoil()
    {
        float elapsedTime = 0f;
        float totalRecoilTime = 0.5f * (1f / fireRate);

        while (elapsedTime < totalRecoilTime)
        {
            // Use an easing function for smoother recoil
            float t = elapsedTime / totalRecoilTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // Apply recoil to the gun
            gun.transform.localPosition = Vector3.Lerp(originalGunPosition, originalGunPosition - gun.transform.forward * recoilDistance, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the gun returns to the original position smoothly
        float returnElapsedTime = 0f;

        while (returnElapsedTime < totalRecoilTime)
        {
            float t = returnElapsedTime / totalRecoilTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // Gradually move the gun back to the original position
            gun.transform.localPosition = Vector3.Lerp(originalGunPosition - gun.transform.forward * recoilDistance, originalGunPosition, t);

            returnElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the gun reaches the exact original position
        gun.transform.localPosition = originalGunPosition;
    }
}
