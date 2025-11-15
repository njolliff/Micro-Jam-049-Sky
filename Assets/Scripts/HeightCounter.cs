using UnityEngine;
using TMPro;

public class HeightCounter : MonoBehaviour
{
    public Player player;
    public TMP_Text text;

    void Update()
    {
        text.text = $"{player.currentHeight} M";
    }
}