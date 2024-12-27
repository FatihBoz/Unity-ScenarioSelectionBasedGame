using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly string qualityColorsFileName = "color.json";
    void Start()
    {
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;

        string path = Application.dataPath + "/"+ qualityColorsFileName;
        //Load Item quality colors from json
        //ItemQualityColor.LoadColorData(path);
    }


}
