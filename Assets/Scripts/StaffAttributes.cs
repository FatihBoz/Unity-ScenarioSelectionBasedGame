using UnityEngine;

public class StaffAttributes : MonoBehaviour
{
    public static StaffAttributes Instance;
    public float DamageReduction { get; private set; }
    public bool CanBeRevived { get; private set; }
    public float NullifyChance { get; private set; }

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
        DamageReduction = 0f;
        NullifyChance = 0f;
        CanBeRevived = false;
    }

    public void SetDamageReduction(float DamageReduction)
    {
        ResetAllAttributes();
        this.DamageReduction = DamageReduction;
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