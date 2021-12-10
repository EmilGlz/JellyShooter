using UnityEngine;
public class Screenshoter : MonoBehaviour
{
    [SerializeField] int imageCount;
    const string Save_Count = "imgC";

    private void Start()
    {
        imageCount = PlayerPrefs.GetInt(Save_Count);
    }

    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot("JellyScreen" + imageCount + ".png");
        imageCount++;
        PlayerPrefs.SetInt(Save_Count, imageCount);
    }
}