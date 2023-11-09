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

        // 이동 방향 계산
        float moveDirection = Mathf.Sign(distance);

        // 캐릭터가 이동하는 방향에 따라 스프라이트 뒤집기
        spriteRenderer.flipX = moveDirection > 0;

        // 이동 중 플레이어 애니메이션 재생
        playerAnimation.Play("Player_Move");

        while (count <= Mathf.Abs(distance))
        {
            // Rigidbody에 속도 설정
            player.velocity = new Vector2(moveDirection * 3f, player.velocity.y);

            // 이동 거리 갱신
            count += Mathf.Abs(player.velocity.x) * Time.deltaTime;

            yield return null;
        }

        // 이동 완료 후 속도를 0으로 설정
        player.velocity = Vector2.zero;

        // 이동 완료 후 플레이어 애니메이션을 Idle로 변경
        playerAnimation.Play("Player_Idle");
    }


}
