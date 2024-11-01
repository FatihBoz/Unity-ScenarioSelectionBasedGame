using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes Instance;

    public float MaxHp { get; private set; } = 100f;
    public float CurrentHp { get; private set; }
    public float DamageReduction { get; private set; }  //as percentage, between 0-1
    public int Level { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Level = 1;
        DamageReduction = 0;
        CurrentHp = MaxHp;
    }




    public void LevelUp(int levelToUp)
    {
        Level += levelToUp;
        print(Level);
    }

    public void IncreaseMaxHealth(float increasingAmount)
    {
        MaxHp += increasingAmount;
    }

    public void IncreaseDamageReduction(float damageReductionPercentage)
    {
        DamageReduction += damageReductionPercentage;
    }

    public void DecreaseDamageReduction(float damageReductionPercentage)
    {
        DamageReduction -= damageReductionPercentage;
    }



    public void PlayerAttributes_OnHealthDecreased(float damageAmount)
    {
        OnCurrentHealthChanged((-1) * damageAmount);
        if (CurrentHp <= 0)
        {
            if (StaffAttributes.Instance.CanBeRevived)
            {
                //!Should be waited about 1 sec to deplete the health bar
                CurrentHp = MaxHp;
                EventManager<float>.TriggerEvent(EventKey.HEALTH_INCREASED, CurrentHp);
                return;
            }

            //todo:death screen
            //todo:return to a certain scenario
        }
    }

    void PlayerAttributes_OnHealthIncreased(float healAmount)
    {
        OnCurrentHealthChanged(healAmount);
    }

    void OnCurrentHealthChanged(float amount)
    {
        CurrentHp += amount;
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