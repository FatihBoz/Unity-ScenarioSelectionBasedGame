using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public static ItemAttributes Instance;

    private float damageReduction;
    private bool canBeRevived;

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
        damageReduction = 0f;
        canBeRevived = false;
    }

    public void SetDamageReduction(float damageReduction)
    {
        ResetAllAttributes();
        this.damageReduction = damageReduction;
    }

    public void EscapeFromDeath()
    {
        ResetAllAttributes();
        canBeRevived = true;
    }

    public float GetDamageReduction() => damageReduction;

    public bool CanBeRevived() => canBeRevived;
}