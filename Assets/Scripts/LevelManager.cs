using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static float currXP;
    int currLevel;
    List<float> levelThreshold = new List<float>();

    public float fireRate = 10f;
    public int maxLevels = 100;
    public float xpIncrement = 10f;

    private RawImage xpBar;

    public Canvas canvas;

    private void Start()
    {
        currXP = 0f;
        currLevel = 1;

        // Generate the first 100 levels with an increment of 10
        for (int i = 0; i < maxLevels; i++)
        {
            levelThreshold.Add(i * xpIncrement);
        }

        // Create a Canvas dynamically if it doesn't exist
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        // Create a RawImage
        xpBar = new GameObject("XPBar").AddComponent<RawImage>();
        xpBar.transform.SetParent(canvas.transform);
        xpBar.rectTransform.anchorMin = new Vector2(0, 1); // Set anchor to top-left
        xpBar.rectTransform.anchorMax = new Vector2(1, 0);
        xpBar.rectTransform.pivot = new Vector2(0, 1);
        xpBar.rectTransform.anchoredPosition = new Vector2(0, 0);
        xpBar.rectTransform.sizeDelta = new Vector2(Screen.width, 10); // Set the size

        xpBar.color = new Color(0.4f, 0.4f, 0.4f); // #666666 in RGB values
    }

    private void Update()
    {
        if (currXP >= levelThreshold[currLevel - 1])
        {
            LevelUp();
        }

        Debug.Log($"CurrentXP: {currXP}"); // this isnt changing for some reason??? yay i fixed that somehow this game is so shit
        // Calculate modifier for xpBar width
        float modifier = currXP / levelThreshold[currLevel - 1];
        xpBar.rectTransform.sizeDelta = new Vector2(Screen.width * modifier, 10); // starts way too big for some reason
    }

    public static void IncrementXP(float xpValue)
    {
        currXP += xpValue;
    }

    public void LevelUp()
    {
        currLevel += 1;
        currXP = 0;
        fireRate += 2f;
    }
}
