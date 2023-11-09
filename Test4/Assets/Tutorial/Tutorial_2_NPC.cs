using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tutorial_2_NPC : MonoBehaviour
{
    public Rigidbody2D Farm;
    public Animator FarmAnimation;
    public SpriteRenderer spriteRenderer;

    public void FarmMove(float distance)
    {
        StartCoroutine(Moving(distance));
    }

    public void Flip(int repeat)
    {
        StartCoroutine(RepeatCharacterFlip(repeat));
    }

    IEnumerator RepeatCharacterFlip(int repeat)
    {
        for (int i = 0; i < repeat; i++)
        {
            yield return StartCoroutine(CharacterFlip());
        }
    }

    IEnumerator CharacterFlip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Moving(float distance)
    {
        float count = 0f;

        // �̵� ���� ���
        float moveDirection = Mathf.Sign(distance);

        // ĳ���Ͱ� �̵��ϴ� ���⿡ ���� ��������Ʈ ������
        spriteRenderer.flipX = moveDirection > 0;

        // �̵� �� �÷��̾� �ִϸ��̼� ���
        FarmAnimation.Play("Farm_Move");

        while (count <= Mathf.Abs(distance))
        {
            // Rigidbody�� �ӵ� ����
            Farm.velocity = new Vector2(moveDirection * 3f, Farm.velocity.y);

            // �̵� �Ÿ� ����
            count += Mathf.Abs(Farm.velocity.x) * Time.deltaTime;

            yield return null;
        }

        // �̵� �Ϸ� �� �ӵ��� 0���� ����
        Farm.velocity = Vector2.zero;

        // �̵� �Ϸ� �� �÷��̾� �ִϸ��̼��� Idle�� ����
        FarmAnimation.Play("Farm_Idle");
    }


}
