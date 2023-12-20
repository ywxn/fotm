using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{

    public TMP_Text newScore;
    public TMP_Text playerBest;
    public TMP_Text newHS; // should be title otherwise

    private float currScore;

    // Start is called before the first frame update
    void Start()
    {
        currScore = Timer.GetEndTime();
        playerBest.text = $"Your best: {SettingsValues.highScore:F2} seconds";
        newScore.text = $"You lasted: {currScore:F2} seconds";
        if (currScore > SettingsValues.highScore)
        {
            newHS.text = "New Highscore!";
            SettingsValues.highScore = currScore;
        } else
        {
            newHS.text = "FLOWERS ON THE MOON";
        }
    }

}
