using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    [Header("ENEMY")]
    [SerializeField] private EnemySO enemy;

    [Header("COMBAT VALUES ")]
    [SerializeField] private float weaknessMultiplier;
    [SerializeField] private float resistanceMultiplier;


    private readonly float timeBetweenConsecutiveSkills = .4f;
    private readonly float timeToStartCombat = 2f;

    private float manaRecoveryTimer = 0f;
    private float currentHp;
    private float currentMana;
    private bool canUseSkill;
    private EnemyState currentEnemyState;

    private void Start()
    {
        currentHp = enemy.MaxHp;
        currentMana = enemy.MaxMana;
        currentEnemyState = enemy.DefaultEnemyState;

        Invoke(nameof(StartCombat), timeToStartCombat);
    }

    void StartCombat()
    {
        canUseSkill = true;
    }

    private void FixedUpdate()
    {
        RegenerateMana();

        if (!canUseSkill)
        {
            return;
        }

        //if enemy's behaviour is flexible
        if(enemy.DefaultEnemyState == EnemyState.None)
        {
            DetermineEnemyState();
        }


        ActAccordingToState();
    }

    void ActAccordingToState()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Aggresive:
                StartCoroutine(
                    ConsecutiveSkillCasting(Random.Range(2,3)));
                break;

            case EnemyState.Defensive:
                StartCoroutine(
                    ConsecutiveSkillCasting(SkillCountDifference()));
                break;

            case EnemyState.ManaRecovery:
                HandleManaRecovery();
                break;
        }
    }

    void DetermineEnemyState()
    {
        if (MiniGameCombatManager.Instance.PlayerQ.Count > 0)
        {
            currentEnemyState = EnemyState.Defensive;
        }
        else if (currentMana >= enemy.MaxMana * enemy.ManaPercentageThresholdForCombo)
        {
            currentEnemyState = EnemyState.Aggresive;
        }
        else
        {
            currentEnemyState = EnemyState.ManaRecovery;
            return;
        }

        manaRecoveryTimer = 0f;
    }

    void HandleManaRecovery()
    {
        manaRecoveryTimer += Time.fixedDeltaTime;

        if (manaRecoveryTimer >= enemy.TimeBetweenSkillCastingInManaRecovery)
        {
            CastSkill();
            manaRecoveryTimer = 0f;
        }
    }

    int SkillCountDifference()
    {
        int diff = MiniGameCombatManager.Instance.PlayerQ.Count - MiniGameCombatManager.Instance.EnemyQ.Count;

        return Mathf.Max(0,diff);
    }

    protected override void RegenerateMana()
    {
        currentMana += enemy.ManaRegenPerSecond * Time.fixedDeltaTime;
    }



    private IEnumerator ConsecutiveSkillCasting(int repeatCount)
    {//Start of consecutive skill casting
        canUseSkill = false;
        int counter = 0;

        while (counter < repeatCount)
        {
            CastSkill();
            counter++;
            yield return new WaitForSeconds(timeBetweenConsecutiveSkills);
        }
        //END
        canUseSkill = true;
    }


    private void CastSkill()
    {
        int r = Random.Range(0, enemy.Skills.Count);

        Skill newSkill = new(enemy.Skills[r]);  

        if (currentMana < newSkill.ManaToCast)
        {
            return;
        }
        
        currentMana -= newSkill.ManaToCast;

        GameObject obj = Instantiate(skillIcon, spawnPoint.position, Quaternion.identity, spawnPoint);
        //choose a random skill enemy have
        MiniGameCombatUI.Instance.AddSkill(newSkill);

        if (obj.TryGetComponent<RectTransform>(out var rect))
        {
            //make it move
            rect.DOAnchorPos(destination, 1 / newSkill.Speed)
                //Ensure that it is moving with constant velocity
                .SetEase(Ease.Linear)

                .OnComplete(() => OnSkillMovementFinished(rect, newSkill));

            MiniGameCombatManager.Instance.EnemyQ.Add(new SkillIcon(newSkill, rect));
        }
        //Call a method to set image to skill icon from super class
        SetImage(obj, newSkill);
    }

    protected override void OnSkillMovementFinished(RectTransform skillIcon, Skill skill)
    {
        base.OnSkillMovementFinished(skillIcon, skill);
        //Remove skill from the skill list
        MiniGameCombatManager.Instance.EnemyQ.RemoveAt(0);
        //Remove Skill text
        MiniGameCombatUI.Instance.RemoveSkill(skill);
        //Skill reached enemy side
        EventManager<float>.TriggerEvent(EventKey.MiniGameCombat_Player_TakeDamage, skill.Damage);
    }

    public List<ItemSO> LootItemDrop { get => enemy.LootItemDrop; }



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
        MiniGameCombatUI.Instance.UpdateEnemyHealthBar(currentHp / enemy.MaxHp);

        if (currentHp <= 0)
        {
            EventManager<Combat>.TriggerEvent(EventKey.MiniGame_Finished, this);
        }
    }

    private void EnemyCombat_OnEnemyFound(EnemySO enemySO)
    {
        enemy = enemySO;
    }

    private void OnEnable()
    {
        EventManager<EnemySO>.Subscribe(EventKey.ENEMY_FOUND, EnemyCombat_OnEnemyFound);
        EventManager<Skill>.Subscribe(EventKey.MiniGameCombat_Enemy_TakeDamage, EnemyCombat_OnEnemyTakeDamage);
    }

    private void OnDisable()
    {

        EventManager<EnemySO>.Unsubscribe(EventKey.ENEMY_FOUND, EnemyCombat_OnEnemyFound);
        EventManager<Skill>.Unsubscribe(EventKey.MiniGameCombat_Enemy_TakeDamage, EnemyCombat_OnEnemyTakeDamage);
    }


}

public enum EnemyState
{
    None,
    ManaRecovery, //Active at low mana, waiting its mana to fill while using skills one by one with certain time intervals 
    Defensive, //Matching player skills
    Aggresive //using multiple skill at high mana
}