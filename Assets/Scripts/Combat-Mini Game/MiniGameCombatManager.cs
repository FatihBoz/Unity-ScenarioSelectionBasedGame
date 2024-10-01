using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCombatManager : MonoBehaviour
{
    public static MiniGameCombatManager Instance;
    //!Initially, queues were used, but since skills had different speeds,
    //!it was necessary to dynamically sort them, so we turned to List.
    private List<SkillIcon> playerQ = new();

    private List<SkillIcon> enemyQ = new();

    private Canvas canvas;

    private ElementalStrength elementalStrength;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        elementalStrength = new();

        canvas = GameObject.FindObjectOfType<Canvas>();
    }



    private void FixedUpdate()
    {
        CheckCollisions();
        UpdateSkillLists();
    }


    void UpdateSkillLists()
    {
        //higher y position comes first
        playerQ.Sort((a, b) => b.RectTransform.anchoredPosition.y.CompareTo(a.RectTransform.anchoredPosition.y));
        //Lower y position comes first
        enemyQ.Sort((a,b) => a.RectTransform.anchoredPosition.y.CompareTo(b.RectTransform.anchoredPosition.y));
    }


    void CheckCollisions()
    {
        if (playerQ.Count == 0 || enemyQ.Count == 0)
        {
            return;
        }

        if (IsRectTransformOverlapping(playerQ[0].RectTransform, enemyQ[0].RectTransform))
        {
            OnSkillCollision(playerQ[0].Skill, enemyQ[0].Skill);
            ScreenShake.Instance.Shake();
        }
    }




    void OnSkillCollision(Skill playerSkill,Skill enemySkill)
    {
        //if player skill is strong against enemy skill
        if (elementalStrength.IsElementStronger(playerSkill.Element, enemySkill.Element))
        {
            DestroySkill(enemySkill, enemyQ);
            //Remove enemy skill text
            MiniGameCombatUI.Instance.RemoveSkill(enemySkill);
        }

        //if enemy skill is stronger
        else if(elementalStrength.IsElementStronger(enemySkill.Element, playerSkill.Element))
        {
            DestroySkill(playerSkill, playerQ);
        }

        //if they are equal
        else
        {
            DestroySkill(enemySkill, enemyQ);
            DestroySkill(playerSkill, playerQ);
            //Remove enemy skill text
            MiniGameCombatUI.Instance.RemoveSkill(enemySkill);
        }

    }


    bool IsRectTransformOverlapping(RectTransform rect1, RectTransform rect2)
    {

        Rect rect1World = GetWorldRect(rect1);

        Rect rect2World = GetWorldRect(rect2);

        return rect1World.Overlaps(rect2World);
    }


    Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector2 size = rectTransform.rect.size;

        // consider its scale
        Vector3 scale = rectTransform.lossyScale;
        size.x *= scale.x;
        size.y *= scale.y;

        // Get coordinates in world space
        Vector2 position = rectTransform.TransformPoint(rectTransform.rect.position);

        return new Rect(position, size);
    }

    void DestroySkill(Skill skill, List<SkillIcon> q)
    {
        InstantiateExplosionEffect(skill.ExplosionPrefab, q[0].RectTransform.position);
        //Stop dotween for this object
        DOTween.Kill(q[0].RectTransform);
        //Destroy player skill
        Destroy(q[0].RectTransform.gameObject);

        q.RemoveAt(0);
    }


    void InstantiateExplosionEffect(GameObject explosionPrefab,Vector2 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity, canvas.transform);
        Destroy(explosion, .5f);
    }

    public void AddSkillIcon(SkillIcon skill)
    {
        playerQ.Add(skill);
    }

    public List<SkillIcon> PlayerQ => playerQ;

    public List<SkillIcon> EnemyQ => enemyQ;
}
