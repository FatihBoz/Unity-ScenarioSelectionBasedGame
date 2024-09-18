using UnityEngine;

public class Skill
{
    private readonly float damage;
    private readonly float manaToCast;
    private readonly Sprite sprite;
    private readonly Element element;
    private readonly string name;
    private readonly GameObject explosionPrefab;
    private readonly float speed; //between 0 and 1

    public float Damage => damage;

    public float ManaToCast => manaToCast;

    public Sprite Sprite => sprite;

    public Element Element => element;

    public string Name => name;

    public GameObject ExplosionPrefab => explosionPrefab;

    public float Speed => speed;

    public Skill(SkillSO s)
    {
        sprite = s.SkillSprite;
        element = s.SkillElement;
        name = s.SkillName;
        explosionPrefab = s.ExplosionPrefab;
        damage = s.Damage;
        manaToCast = s.ManaToCast;
        speed = s.Speed;    
    }
}
