using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float regenerationDelay = 7f;
    public float regenerationRate = 10f;
    public Stat damage;
    public Stat armor;

    float lastDamageTime;

    public event System.Action<float, float> OnHealthChanged;

    private float timeSinceLastChange;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // void Start()
    // {
    //     lastDamageTime = Time.time;
    // }

    void Update()
    {
        //if health hasn't changed in 7 seconds, then regenerate health
        if (Time.time - lastDamageTime > regenerationDelay)
        {
            RegenerateHealth();
        }

        //if k is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            //take 10 damage
            Heal(10f);
        }
    }

    public void Heal(float amount)
    {
        print("Healing");
        //increase health by amount
        currentHealth += amount;
        //if current health is greater than max health
        if (currentHealth > maxHealth)
        {
            //set current health to max health
            currentHealth = maxHealth;
        }
    }

    public void RegenerateHealth()
    {
        currentHealth += regenerationRate * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Heal the character.
    // public void Heal(int amount)
    // {
    //     currentHealth += amount;
    //     currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
    // }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
