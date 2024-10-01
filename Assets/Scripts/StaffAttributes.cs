using UnityEngine;

public class StaffAttributes : MonoBehaviour
{
    public static StaffAttributes Instance;
    public bool CanBeRevived { get; private set; }
    public float NullifyChance { get; private set; }

    private float damageReduction = 0f;  //as percentage, between 0-1
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }


    private void ResetAllAttributes()
    {
        PlayerAttributes.Instance.DecreaseDamageReduction(damageReduction);
        damageReduction = 0f;
        NullifyChance = 0f;
        CanBeRevived = false;
    }

    public void SetDamageReduction(float DamageReduction)
    {
        ResetAllAttributes();
        damageReduction = DamageReduction;
        PlayerAttributes.Instance.IncreaseDamageReduction(DamageReduction);
    }

    public void EscapeFromDeath()
    {
        ResetAllAttributes();
        CanBeRevived = true;
    }

    public void SetNullifyChance(float chance)
    {
        ResetAllAttributes();
        NullifyChance = chance;
    }
}