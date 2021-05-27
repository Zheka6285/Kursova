using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class SaveProgress : MonoBehaviour
{
    public Text bestScoreText;
    public Text scoreText;

    public Text allCoinText;
    public Text coinText;
    public static int allCoin;

    private Save sv = new Save();
    private string path;

    public GameObject shadowsOn;
    public GameObject shadowsOff;

    private int shadowMod;

    public GameObject vibrationOn;
    public GameObject vibrationOff;

    public int vibrationMod = 1;
    public static int vibration;

    public GameObject soundOn;
    public GameObject soundOff;

    public static int soundMod = 1;

    public void SendPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
    }

    void Start()
    {
        SendPath();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            bestScoreText.text = sv.score.ToString();
            allCoinText.text = sv.coin.ToString();
            allCoin = sv.coin;
            vibration = sv.vibration;
            if(sv.shadows == 1)
            {
                shadowsOff.SetActive(false);
                shadowsOn.SetActive(true);
                Settings.Shadows(1);
            }
            else
            {
                shadowsOff.SetActive(true);
                shadowsOn.SetActive(false);
                Settings.Shadows(0);
            }

            if (sv.vibration == 1)
            {

                vibrationOff.SetActive(false);
                vibrationOn.SetActive(true);
            }
            else
            {

                vibrationOff.SetActive(true);
                vibrationOn.SetActive(false);
            }

            if (sv.sound == 1)
            {
                Settings.Sounds(sv.sound);
                soundOff.SetActive(false);
                soundOn.SetActive(true);
            }
            else
            {
                Settings.Sounds(sv.sound);
                soundOff.SetActive(true);
                soundOn.SetActive(false);
            }
        }
        else
        {
            vibration = 1;
            int zero = 0;
            bestScoreText.text = zero.ToString();
            allCoinText.text = zero.ToString();
            shadowsOff.SetActive(false);
            shadowsOn.SetActive(true);
            vibrationOff.SetActive(false);
            vibrationOn.SetActive(true);
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }
    }



    public void SendCoin()
    {
        SendPath();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            allCoinText.text = sv.coin.ToString();
        }
    }

  
    public void WriteCoin()
    {
        int iCoinText = Convert.ToInt32(coinText.text);
        int iAllCoinText = Convert.ToInt32(allCoinText.text);
        sv.coin = iCoinText + iAllCoinText;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }


    public void SendScore()
    {
        SendPath();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            bestScoreText.text = sv.score.ToString();
        }
    }

    public void WriteScore()
    {
        
        int iScoreText = Convert.ToInt32(scoreText.text);
        int iBestScoreText = Convert.ToInt32(bestScoreText.text);
        if (iScoreText > iBestScoreText)
        {
            sv.score = iScoreText;
        }
    }

    public void ShadowsOn() { 
        shadowMod = 1;
        WriteSettingsShadows();
    }

    public void ShadowsOff() { 
        shadowMod = 0;
        WriteSettingsShadows();
    }

    public void WriteSettingsShadows()
    {
        SendPath();
        sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        if (Settings.Shadows(shadowMod) == true)
        {
            sv.shadows = 1;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
        else
        {
            sv.shadows = 0;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
    }

    public void SoundOn()
    {
        soundMod = 1;
        WriteSettingsSounds();
    }

    public void SoundOff()
    {
        soundMod = 0;
        WriteSettingsSounds();
    }

    public void WriteSettingsSounds()
    {
        SendPath();
        sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        if (Settings.Sounds(soundMod) == true)
        {
            sv.sound = 1;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
        else
        {
            sv.sound = 0;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
    }
    public void VibrationOn()
    {
        vibrationMod = 1;
        WriteSettingsVibration();
    }

    public void VibrationOff()
    {
        vibrationMod = 0;
        WriteSettingsVibration();
    }

    public void WriteSettingsVibration()
    {
        SendPath();
        sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        if (Settings.Vibrator(vibrationMod) == true)
        {
            sv.vibration = 1;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
        else
        {
            sv.vibration = 0;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
    }
    private void Update()
    {
        if (BuyObject.isBuy==true)
        {
            BuyObject.isBuy = false;
            Start();
        }
        WriteScore();
        if(Time.timeScale == 0f)
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        if (Pleyer.IsDeadGoToSave() == true)
        {
            WriteCoin();
            allCoin = sv.coin;
        }
    }

}
[Serializable]
public class Save
{
    public int score;
    public int coin;
    public int shadows;
    public int vibration;
    public int sound;
}