using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDieAnimation()
    {
        animator.SetTrigger("Die");
    }

    public void PlayMoveAnimation(Vector2 movement)
    {
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }
}
