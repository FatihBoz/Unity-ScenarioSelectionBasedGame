using DG.Tweening;
using UnityEngine;

public class EnemyCombat : Combat
{
    [Header("ENEMY")]
    [SerializeField] private EnemySO enemy;

    [SerializeField] private float weaknessMultiplier;
    [SerializeField] private float resistanceMultiplier;

    private float currentHp;

    private void Start()
    {
        EventManager<EnemySO>.TriggerEvent(EventKey.ENEMY_FOUND, enemy);

        if (enemy != null)
        {
            InvokeRepeating(nameof(CastSkill), .2f, 2f);
        }

    }

    private void CastSkill()
    {
        GameObject obj = Instantiate(skillIcon, spawnPoint.position, Quaternion.identity, spawnPoint);
        //choose a random skill enemy have
        int r = Random.Range(0, enemy.Skills.Count);

        Skill newSkill = new(enemy.Skills[r]);

        MiniGameCombatUI.Instance.AddSkill(newSkill);

        if (obj.TryGetComponent<RectTransform>(out var rect))
        {
            //if it is not null , make it move
            rect.DOAnchorPos(destination, 1 / newSkill.Speed)
                //Ensure that it is moving with constant velocity
                .SetEase(Ease.Linear)

                .OnComplete(() => OnSkillMovementFinished(rect, newSkill));

            MiniGameCombatManager.Instance.enemyQ.Add(new SkillIcon(newSkill, rect));
        }
        //Call a method to set image to skill icon from super class
        SetImage(obj, newSkill);
    }

    protected override void OnSkillMovementFinished(RectTransform skillIcon, Skill skill)
    {
        base.OnSkillMovementFinished(skillIcon, skill);
        //Remove Skill text
        MiniGameCombatUI.Instance.RemoveSkill(skill);
        //Remove skill from the skill list
        MiniGameCombatManager.Instance.enemyQ.RemoveAt(0);
        //Skill reached enemy side
        EventManager<float>.TriggerEvent(EventKey.MiniGameCombat_Player_TakeDamage, skill.Damage);
    }


    private void EnemyCombat_OnEnemyTakeDamage(Skill skill)
    {

        if (enemy.IsWeakTo(skill.Element))
        {
            currentHp -= skill.Damage * weaknessMultiplier;
        }
        else if(enemy.IsResistantTo(skill.Element))
        {
            currentHp -= skill.Damage * resistanceMultiplier;
        }
        else
        {
            currentHp -= skill.Damage;
        }


        //Update UI
        MiniGameCombatUI.Instance.EnemyTakeDamage(currentHp / enemy.MaxHp);
    }


    private void OnEnable()
    {
        currentHp = enemy.MaxHp;

        EventManager<EnemySO>.Subscribe(EventKey.ENEMY_FOUND, EnemyCombat_OnEnemyFound);
        EventManager<Skill>.Subscribe(EventKey.MiniGameCombat_Enemy_TakeDamage, EnemyCombat_OnEnemyTakeDamage);
    }

    private void EnemyCombat_OnEnemyFound(EnemySO enemySO)
    {
        enemy = enemySO;
    }

    private void OnDisable()
    {

        EventManager<EnemySO>.Unsubscribe(EventKey.ENEMY_FOUND, EnemyCombat_OnEnemyFound);
        EventManager<Skill>.Unsubscribe(EventKey.MiniGameCombat_Enemy_TakeDamage, EnemyCombat_OnEnemyTakeDamage);
    }


}