using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public static ItemAttributes Instance;

    private float luck; //additional chance to get high tier items
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
        luck = 0f;
        damageReduction = 0f;
        canBeRevived = false;
    }

    public float GetLuck() => luck;

    public float GetDamageReduction() => damageReduction;

    public bool CanBeRevived() => canBeRevived;
}