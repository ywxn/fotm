using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XPObject : MonoBehaviour
{
    public GameObject player;

    public float xpValue;

    private Vector3 playerPos;

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        // only moves towards the player while the player is NOT shooting
        bool v = Input.GetKey(KeyCode.Mouse0);
        if (!v)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
        }
    }

    public void Initialize(GameObject tplayer, float value)
    {
        player = tplayer;
        xpValue = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.IncrementXP(xpValue);
            Destroy(gameObject);
        }
    }
}
