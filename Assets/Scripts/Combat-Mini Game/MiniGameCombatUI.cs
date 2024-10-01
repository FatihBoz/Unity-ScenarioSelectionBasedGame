using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameCombatUI : MonoBehaviour
{
    public static MiniGameCombatUI Instance;

    [Header("PLAYER UI")]
    [SerializeField] private Image playerHealth;

    [SerializeField] private Image playerMana;

    [Header("ENEMY UI")]
    [SerializeField] private Image enemyHealth;

    [SerializeField] private List<ElementIcon> elementIcons;

    [SerializeField] private TextMeshProUGUI enemyName;

    [SerializeField] private GameObject skillTextPrefab;

    [SerializeField] private Transform skillContainer; 

    private Dictionary<Skill,GameObject> activeSkills = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void AddSkill(Skill skill)
    {
        GameObject obj = Instantiate(skillTextPrefab,skillContainer);

        if (obj.TryGetComponent<SkillText>(out var t))
        {
            t.SetName(skill.Name);

            foreach(var icon in elementIcons)
            {
                if (icon.element == skill.Element)
                {
                    t.SetImage(icon.standartElementIconSprite);
                }
            }

            activeSkills.Add(skill,obj);
        }
    }

    public void RemoveSkill(Skill skill)
    {
        if (activeSkills.ContainsKey(skill))
        {
            Destroy(activeSkills[skill]);
            activeSkills.Remove(skill);
        }
    }



    private void MiniGameCombatUI_OnEnemyFound(EnemySO enemy)
    {
        enemyName.text = enemy.Name;

        // Iterate through each ElementIcon and set its state based on the enemy's weaknesses and resistances
        foreach (var icon in elementIcons)
        {
            // Check if the enemy has any interaction for the current element
            var interaction = enemy.Interactions.FirstOrDefault(i => i.element == icon.element);

            // Activate the appropriate icon based on the interaction type
            if (interaction.interactionType == ElementInteractionType.Weakness)
            {
                icon.weakElementIconImage.SetActive(true);
            }
            else if (interaction.interactionType == ElementInteractionType.Resistance)
            {
                icon.resistantElementIconImage.SetActive(true);
            }
        }

    }



    public void UpdatePlayerHealthBarUI(float healthFillAmount)
    {
        playerHealth.DOFillAmount(healthFillAmount, 1f);
    }

    public void UpdatePlayerManaBar(float manaFillAmount)
    {
        playerMana.DOFillAmount(manaFillAmount, 1f);
    }

    public void PlayerCastSkill(float manaFillAmount)
    {
        playerMana.DOFillAmount(manaFillAmount, 1f);
    }

    public void UpdateEnemyHealthBar(float healthFillAmount)
    {
        enemyHealth.DOFillAmount(healthFillAmount, 1f);
    }

    private void OnEnable()
    {
        EventManager<EnemySO>.Subscribe(EventKey.ENEMY_FOUND, MiniGameCombatUI_OnEnemyFound);
    }

    private void OnDisable()
    {
        EventManager<EnemySO>.Unsubscribe(EventKey.ENEMY_FOUND, MiniGameCombatUI_OnEnemyFound);
    }
}


[Serializable]
public struct ElementIcon
{
    //just needed to SetActive for image

    public GameObject weakElementIconImage;
    public GameObject resistantElementIconImage;
    public Sprite standartElementIconSprite;
    public Element element;
}
