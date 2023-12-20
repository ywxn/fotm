using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }

}
