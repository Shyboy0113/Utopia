using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    public int health = 100;
    public int attackPower = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ���� ���� ó��
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
