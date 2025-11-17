using TMPro;
using UnityEngine;

public class HighscoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public bool newHighscore = false;

    void OnEnable()
    {
        if (newHighscore)
            _text.text = $"New Highscore: {PlayerPrefs.GetInt("Highscore")}";
        else
            _text.text = $"Highscore: {PlayerPrefs.GetInt("Highscore")}";
    }
}