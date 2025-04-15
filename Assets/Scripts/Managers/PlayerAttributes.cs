using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes Instance;

    private float maxHp = 100f;
    private float maxMana = 100f;
    private float damageReduction;
    private float currentHp;
    private int level;


    public float MaxHp { get => maxHp;}
    public float CurrentHp { get => currentHp;}
    public float DamageReduction { get => damageReduction;}  //as percentage, between 0-1
    public int Level { get => level;}

    public float MaxMana { get => maxMana;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        level = 1;
        damageReduction = 0;
        currentHp = MaxHp;
    }


    public void LevelUp(int levelToUp)
    {
        level += levelToUp;
        print(Level);
    }

    public void IncreaseMaxHealth(float increasingAmount)
    {
        maxHp += increasingAmount;
    }

    public void IncreaseDamageReduction(float damageReductionPercentage)
    {
        damageReduction += damageReductionPercentage;
        damageReduction = Mathf.Clamp01(damageReduction);
    }

    public void DecreaseDamageReduction(float damageReductionPercentage)
    {
        damageReduction -= damageReductionPercentage;
        damageReduction = Mathf.Clamp01(damageReduction);
    }



    public void PlayerAttributes_OnHealthDecreased(float damageAmount)
    {
        OnCurrentHealthChanged((-1) * damageAmount);
        
        if (CurrentHp <= 0)
        {
            EventManager<PlayerAttributes>.TriggerEvent(EventKey.Player_Died, this);
        }
    }

    void PlayerAttributes_OnHealthIncreased(float healAmount)
    {
        OnCurrentHealthChanged(healAmount);
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    void OnCurrentHealthChanged(float amount)
    {
        currentHp += amount;

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        EventManager<float>.TriggerEvent(EventKey.HEALTH_UI_CHANGED, (CurrentHp / MaxHp));
    }

    private void OnEnable()
    {
        EventManager<float>.Subscribe(EventKey.HEALTH_DECREASED, PlayerAttributes_OnHealthDecreased);
        EventManager<float>.Subscribe(EventKey.HEALTH_INCREASED, PlayerAttributes_OnHealthIncreased);
        
    }

    private void OnDisable()
    {
        EventManager<float>.Unsubscribe(EventKey.HEALTH_DECREASED, PlayerAttributes_OnHealthDecreased);
        EventManager<float>.Unsubscribe(EventKey.HEALTH_INCREASED, PlayerAttributes_OnHealthIncreased);
    }
}