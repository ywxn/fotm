using System.Collections.Generic;
using UnityEngine;

public class JumpSFX : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> audioClips;
    public LayerMask ground;

    public GameObject player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            audioSource.PlayOneShot(audioClips[Mathf.RoundToInt(Random.Range(0f, audioClips.Count-1))]);
        }

    }

    bool IsGrounded()
    {
        float raycastLength = 1.5f; // Adjust this based on your character's size
        Vector3 raycastOrigin = player.transform.position;

        // Cast a ray downwards to check if grounded
        if (Physics.Raycast(raycastOrigin, Vector3.down, raycastLength, ground))
        {
            return true;
        }

        return false;
    }
}
