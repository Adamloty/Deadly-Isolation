using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sitteng : MonoBehaviour
{
    public void ChangeQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
