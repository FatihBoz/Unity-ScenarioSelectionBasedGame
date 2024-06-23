using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes Instance;

    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    private int level;

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
        level = 1;
        currentHp = maxHp;
    }

    public void LevelUp()
    {
        level++;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHp -= damageAmount;
    }

    public int GetLevel() => level;

    public float GetCurrentHp() => currentHp;

    public float GetMaxHp() => maxHp;
}