using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;
    

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
