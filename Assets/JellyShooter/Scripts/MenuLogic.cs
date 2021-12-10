using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{

    #region Singleton

    private static MenuLogic instance;

    public static MenuLogic Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject gameWinCanvas;

    [SerializeField] GameObject touchToPlayButton;
    [SerializeField] GameObject settingsButton;

    [SerializeField] HumanSpawner humanSpawner;
    [SerializeField] Shooter shooter;
    // UI---------------------------------------
    [Header("UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text allCoinText_InGameCanvas;
    [SerializeField] TMP_Text allCoinText_WinCanvas;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] TMP_Text levelPassedText;

    [SerializeField] GameObject soundRedCross;
    [SerializeField] Image fillingReloadImage;
    [SerializeField] RectTransform tickImage;
    [SerializeField] float reloadAnimTime = 1f;

    public GameObject comboPanel;
    public TMP_Text comboText;
    public Image levelFill;

    [SerializeField] GameObject soundRect;
    private bool settingsOpen;
    //------------------------------------------

    public int allScore;
    public int currentScore = 0;

    private void Start()
    {
        currentScore = 0;
        inGameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        gameWinCanvas.SetActive(false);
        humanSpawner.canSpawnHuman = false;
    }

    public void UpdateUI()
    {
        allCoinText_InGameCanvas.text = CommonData.Instance.coins.ToString();
        allCoinText_WinCanvas.text = CommonData.Instance.coins.ToString();
        currentLevelText.text = CommonData.Instance.currentLevel.ToString();
        nextLevelText.text = (CommonData.Instance.currentLevel + 1).ToString();
    }

    public void GameWin()
    {
        // update all datas in ui
        allCoinText_WinCanvas.text = CommonData.Instance.coins.ToString();
        levelPassedText.text = "Level " + CommonData.Instance.currentLevel + " passed";
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameWinCanvas.SetActive(true);
    }

    public void NextLevel_OnClick()
    {
        CommonData.Instance.currentLevel++;
        CommonData.Instance.SaveLocally();
        Restart_OnClick();
    }

    public void Restart_OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame_OnClick()
    {
        Time.timeScale = 1f;
        humanSpawner.canSpawnHuman = true;
        inGameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        gameWinCanvas.SetActive(false);
        touchToPlayButton.SetActive(false);
        settingsButton.SetActive(false);
    }

    public void AddScore(int addingScore)
    {
        CommonData.Instance.coins++;
        currentScore += addingScore;
        allCoinText_InGameCanvas.text = CommonData.Instance.coins.ToString();
    }

    public IEnumerator ShowScoreText(int score, float showTime)
    {
        scoreText.text = "+ " + score;
        scoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showTime);
        scoreText.gameObject.SetActive(false);
        scoreText.text = "";
    }

    public void OnClick_Settings()
    {
        if (settingsOpen)
        {
            LeanTween.moveLocalY(soundRect, 0f, .3f);
            settingsOpen = false;
        }
        else
        {
            LeanTween.moveLocalY(soundRect, 140f, .3f);
            settingsOpen = true;
        }
    }

    public void SetCombo(int comboCount)
    {
        comboText.text = comboCount.ToString() + "x";
        LeanTween.scale(comboPanel, Vector3.one, 0.3f).setEaseOutBack().setOnComplete(()=> {
            LeanTween.scale(comboPanel, Vector3.zero, 0.3f).setEaseInBack();
        }).setDelay(1f);
    }

    public void MakeReloadUIDefault()
    {
        shooter.canShoot = false;
        tickImage.gameObject.SetActive(false);
        tickImage.localScale = Vector3.zero;
        fillingReloadImage.fillAmount = 0f;
    }

    public void StartReloadAnimUI()
    {
        LeanTween.value(0f,1f, reloadAnimTime).setOnComplete(()=> 
        {
            tickImage.gameObject.SetActive(true);
            LeanTween.scale(tickImage, Vector3.one , 0.2f).setEaseOutBack();
            shooter.canShoot = true;
        }).setOnUpdate(OnReloadUIUpdate);
    }

    void OnReloadUIUpdate(float val)
    {
        fillingReloadImage.fillAmount = val;
    }

    public void SoundOn_OnClick()
    {
        if (CommonData.Instance.soundOn)
        {
            CommonData.Instance.soundOn = false;
            soundRedCross.SetActive(true);
        }
        else
        {
            CommonData.Instance.soundOn = true;
            soundRedCross.SetActive(false);
        }
    }

    [SerializeField] RectTransform destinationForCoinAnim;
    [SerializeField] RectTransform InGameCanvas;
    public RectTransform animatingCoindImage;
    public TMP_Text animCoinText;

    // Update is called once per frame
    public void StartCoinAnim(Transform startPos)
    {
        animatingCoindImage.gameObject.SetActive(true);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, startPos.position);
        animatingCoindImage.anchoredPosition = screenPoint - InGameCanvas.sizeDelta / 2f;
        LeanTween.move(animatingCoindImage, destinationForCoinAnim.localPosition, .5f).setOnComplete(()=> 
        {
            CommonData.Instance.scoreWrittenInAnimCoin = 0;
            animatingCoindImage.gameObject.SetActive(false);
            AddScore(1);
        });
    }
}
