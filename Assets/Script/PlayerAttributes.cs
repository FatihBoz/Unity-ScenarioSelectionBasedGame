using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes Instance;

    public float MaxHp { get; private set; } = 100f;
    public float CurrentHp { get; private set; }
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
        CurrentHp = MaxHp;
    }

    public void LevelUp()
    {
        Level++;
    }

    public void PlayerAttributes_OnHealthDecreased(float damageAmount)
    {
        CurrentHp -= damageAmount;
    }


    private void OnEnable()
    {
        EventManager<float>.Subscribe(EventKey.HEALTH_DECREASED, PlayerAttributes_OnHealthDecreased);
    }

    private void OnDisable()
    {
        EventManager<float>.Unsubscribe(EventKey.HEALTH_DECREASED, PlayerAttributes_OnHealthDecreased);
    }
}