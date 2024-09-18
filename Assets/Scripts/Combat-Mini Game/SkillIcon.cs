using UnityEngine;

public class SkillIcon
{
    private Skill skill;
    private RectTransform rectTransform;    

    public SkillIcon(Skill s, RectTransform r)
    {
        Skill = s;
        RectTransform = r;
    }

    public Skill Skill { get => skill; set => skill = value; }
    public RectTransform RectTransform { get => rectTransform; set => rectTransform = value; }
}
