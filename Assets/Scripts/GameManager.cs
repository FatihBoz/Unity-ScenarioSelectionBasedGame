using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly string qualityColorsFileName = "color.json";
    void Start()
    {
        string path = Application.dataPath + "/"+ qualityColorsFileName;
        //Load Item quality colors from json
        ItemQualityColor.LoadColorData(path);
        
    }


}
