using UnityEngine;
using UnityEngine.UI;

public class BoostMeter : MonoBehaviour
{
    public Player player;
    public Slider slider;

    void Update()
    {
        slider.value = player.energy;
    }
}