using UnityEngine;
using UnityEngine.UI;

public abstract class Combat : MonoBehaviour
{
    [Header("SPAWN POINTS")]
    [SerializeField] protected RectTransform spawnPoint;

    [SerializeField] protected Vector2 destination;

    [Header("SKILL ICON PREFAB")]
    [SerializeField] protected GameObject skillIcon;

    private readonly float destroyTime = .5f;

    private Canvas canvas;


    protected virtual void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    protected virtual void OnSkillMovementFinished(RectTransform skillIcon,Skill skill)
    {
        GameObject explosionPrefab = skill.ExplosionPrefab;

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(skill.ExplosionPrefab, skillIcon.position, Quaternion.identity, canvas.transform);
            
            Destroy(explosion,destroyTime);
            ScreenShake.Instance.Shake();
        }

        Destroy(skillIcon.gameObject);
    }

    protected void SetImage(GameObject obj,Skill newSkill)
    {
        //Get granchild of skill icon object which has actual image component
        Transform skillImage = obj.transform.GetChild(0).GetChild(0);

        if (skillImage != null)
        {
            Image image = skillImage.GetComponentInChildren<Image>();

            if (image != null)
            {
                image.sprite = newSkill.Sprite;
            }
        }
    }

    protected abstract void RegenerateMana();

}
