using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{

    #region Singleton

    private static CommonData instance;

    public static CommonData Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        
    }
    #endregion

    const string level_SAVE = "lvl";
    const string coin_SAVE = "coin";
    const string soundOn_SAVE = "sound";

    [SerializeField] int minStickmansPerLevel;
    [SerializeField] int maxStickmansPerLevel;

    public int allHumansCount;
    [HideInInspector] public int currentLevel;
    [HideInInspector] public int coins;
    public int scoreWrittenInAnimCoin = 0;

    public int currentKilledHumanCount;

    public int maxHumanCountPerBall = 0;

    public bool soundOn;

    public void GetDatas()
    {
        if (!PlayerPrefs.HasKey(soundOn_SAVE))
        {
            soundOn = true;
            PlayerPrefs.SetInt(soundOn_SAVE, 1);
        }
        else
        {
            int soundInt = PlayerPrefs.GetInt(soundOn_SAVE);
            if (soundInt == 1)
            {
                soundOn = true;
            }
            else
            {
                soundOn = false;
            }
        }
        allHumansCount = Random.Range(minStickmansPerLevel, maxStickmansPerLevel);
        currentLevel = PlayerPrefs.GetInt(level_SAVE);
        if (currentLevel == 0)
        {
            currentLevel = 1;
        }
        coins = PlayerPrefs.GetInt(coin_SAVE);
    }

    public void SaveLocally()
    {
        PlayerPrefs.SetInt(level_SAVE, currentLevel);
        PlayerPrefs.SetInt(coin_SAVE, coins);
    }
}
