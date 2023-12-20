using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public RawImage fadeImage;
    public float fadeSpeed = 0.5f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }

        // Deactivate RawImage after fade-in
        fadeImage.gameObject.SetActive(false);
    }
    void SetAlpha(float alpha)
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
    }
}
