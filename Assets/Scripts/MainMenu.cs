using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Play()
    {
        // this scene should only be loaded when pressing the play button
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("WeaponPickup");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }


}
