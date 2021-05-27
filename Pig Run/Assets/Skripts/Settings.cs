using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public void VibrationOn()
    {
        Vibrator(1);
    }
    public void VibrationOff()
    {
        Vibrator(0);
    }
    public static bool Vibrator(int vibrationOnOff)
    {
        if(vibrationOnOff == 1)
            return true;
        else
            return false;
    }


    public void ShadowsOn()
    {
        Shadows(1);
    }
    public void ShadowsOff()
    {
        Shadows(0);
    }
    public static bool Shadows(int shadowsOnOff)
    {
        if (shadowsOnOff == 1)
        {
            QualitySettings.shadows = ShadowQuality.All;
            return true;
        }
        else
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            return false;
        }
    }

    public void SoundsOn()
    {
        Sounds(1);
    }
    public void SoundsOff()
    {
        Sounds(0);
    }
    public static bool Sounds(int soundsOnOff)
    {
        if (soundsOnOff == 1)
        {
            AudioListener.volume = 1; 
            return true;
        }
        else
        {
            AudioListener.volume = 0;
            return false;
        }
    }
}
