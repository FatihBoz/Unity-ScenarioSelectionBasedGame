using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ItemQualityColor
{
    private static Dictionary<ItemQuality, Color> qualityColors = new();

    public static Color GetColor(ItemQuality type)
    {

        if (qualityColors.TryGetValue(type, out Color color))
        {
            return color;
        }

        return Color.magenta; 
    }

    public static void LoadColorData(string path)
    {

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var colorData = JsonConvert.DeserializeObject<Dictionary<string, List<float>>>(json);

            foreach (var entry in colorData)
            {
                // Try to parse the string key into an ItemQuality enum
                if (System.Enum.TryParse(entry.Key, out ItemQuality quality) && entry.Value.Count == 4) //RGBA values
                {
                    qualityColors[quality] = new Color(entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[3]);
                }
            }
        }
    }
}
