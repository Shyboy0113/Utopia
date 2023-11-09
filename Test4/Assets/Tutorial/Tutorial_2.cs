using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tutorial_2 : MonoBehaviour
{
    public Rigidbody2D player;
    public Animator playerAnimation;
    public SpriteRenderer spriteRenderer;

    public void PlayerMove(float distance)
    {
        StartCoroutine(Moving(distance));
    }

    public void PlayerFall()
    {
        playerAnimation.Play("Player_FallDown");
    }

    public void PlayerIdle()
    {
        playerAnimation.Play("Player_Idle");
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
        playerAnimation.Play("Player_Move");

        while (count <= Mathf.Abs(distance))
        {
            // Rigidbody�� �ӵ� ����
            player.velocity = new Vector2(moveDirection * 3f, player.velocity.y);

            // �̵� �Ÿ� ����
            count += Mathf.Abs(player.velocity.x) * Time.deltaTime;

            yield return null;
        }

        // �̵� �Ϸ� �� �ӵ��� 0���� ����
        player.velocity = Vector2.zero;

        // �̵� �Ϸ� �� �÷��̾� �ִϸ��̼��� Idle�� ����
        playerAnimation.Play("Player_Idle");
    }


}
