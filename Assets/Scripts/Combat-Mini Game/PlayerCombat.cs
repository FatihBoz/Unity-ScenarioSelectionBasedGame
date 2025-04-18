using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    [Header("PLAYER ATTRIBUTES")]

    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float manaRegenPerSecond = 7f;

    [Header("PLAYER SKILL PREFAB")]

    [SerializeField] private SkillSO fireSkill;
    [SerializeField] private SkillSO waterSkill;
    [SerializeField] private SkillSO windSkill;
    [SerializeField] private SkillSO earthSkill;


    private float currentMana;
    private float currentHp;

    private Dictionary<string, SkillSO> skillMap;


    protected override void Awake()
    {
        base.Awake();
        skillMap = new Dictionary<string, SkillSO>
        {
            { "Fire", fireSkill },
            { "Water", waterSkill },
            { "Wind", windSkill },
            { "Earth", earthSkill }
        };
    }

    private void Start()
    {
        currentHp = maxHp;
        currentMana = PlayerAttributes.Instance.MaxMana;
    }

    private void FixedUpdate()
    {
        RegenerateMana();
    }


    public void OnSkillButtonPressed(string elementName)
    {
        //Get related skill
        if (skillMap.TryGetValue(elementName, out SkillSO skill) && currentMana >= skill.ManaToCast)
        {
            currentMana -= skill.ManaToCast;

            MiniGameCombatUI.Instance.PlayerCastSkill(currentMana / PlayerAttributes.Instance.MaxMana);

            GameObject obj = Instantiate(skillIcon, spawnPoint.position, Quaternion.identity,spawnPoint);

            Skill newSkill = new(skill);

            if (obj.TryGetComponent<RectTransform>(out var rect))
            {
                //if it is not null , make it move
                Tween tween = rect.DOAnchorPos(destination, 1 / newSkill.Speed) //speed value is between 0 and 1
                    //Ensure that it is moving with constant velocity
                    .SetEase(Ease.Linear)
                    //call a method from super class on complete
                    .OnComplete(() => OnSkillMovementFinished(rect,newSkill));

                MiniGameCombatManager.Instance.PlayerQ.Add(new SkillIcon(newSkill, rect));
            }
            //Call a method to set image from super class
            SetImage(obj, newSkill);
        }
    }

    protected override void RegenerateMana()
    {
        currentMana += manaRegenPerSecond * Time.fixedDeltaTime;
        //Limits mana between specified values
        currentMana = Mathf.Clamp(currentMana, 0, PlayerAttributes.Instance.MaxMana);
        //Update UI
        MiniGameCombatUI.Instance.UpdatePlayerManaBar(currentMana / PlayerAttributes.Instance.MaxMana);
    }



    protected override void OnSkillMovementFinished(RectTransform skillIcon, Skill skill)
    {
        base.OnSkillMovementFinished(skillIcon, skill);
        MiniGameCombatManager.Instance.PlayerQ.RemoveAt(0);
        EventManager<Skill>.TriggerEvent(EventKey.MiniGameCombat_Enemy_TakeDamage, skill);
    }


    private void PlayerCombat_OnPlayerTakeDamage(float damageAmount)
    {
        currentHp -= damageAmount;
        //Update UI
        MiniGameCombatUI.Instance.UpdatePlayerHealthBarUI(currentHp / maxHp);
        if (currentHp < 0)
        {
            EventManager<Combat>.TriggerEvent(EventKey.MiniGame_Finished, this);
        }
    }
    private void OnDisable()
    {
        EventManager<float>.Unsubscribe(EventKey.MiniGameCombat_Player_TakeDamage, PlayerCombat_OnPlayerTakeDamage);
    }


    private void OnEnable()
    {
        EventManager<float>.Subscribe(EventKey.MiniGameCombat_Player_TakeDamage, PlayerCombat_OnPlayerTakeDamage);
    }



}
