using UnityEngine;


[CreateAssetMenu(fileName = "New Skill",menuName = "Skill")]
public class SkillSO : ScriptableObject
{
    [SerializeField] private string skillName;

    [SerializeField] private Element skillElement;

    [SerializeField] private Sprite skillSprite;

    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private float manaToCast;

    [SerializeField] private float damage;

    [SerializeField] private float speed;

    


    public string SkillName { get => skillName; set => skillName = value; }
    public Element SkillElement { get => skillElement; set => skillElement = value; }
    public Sprite SkillSprite { get => skillSprite; set => skillSprite = value; }
    public GameObject ExplosionPrefab { get => explosionPrefab; set => explosionPrefab = value; }
    public float ManaToCast { get => manaToCast; set => manaToCast = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
}
