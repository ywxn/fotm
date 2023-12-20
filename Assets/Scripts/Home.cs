using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{

    public GameObject Timer;
    // Update is called once per frame

    private void Start()
    {
        if (Timer != null)
            Timer.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.Tab) && SceneManager.GetActiveScene().name != "WeaponPickup") // TODO changed to int to avoid fps drop pressing tab in WeaponPickup
        {
            Timer.SetActive(!Timer.activeSelf);
        }
    }
}
