using Unity.VisualScripting;
using UnityEngine;

public class Trigger_Wall : MonoBehaviour
{
    public float bounceForce = 0.1f; // 튕겨내는 힘의 세기
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject _gameObject = col.gameObject;

        if (_gameObject.CompareTag("Boss"))
        {
            RedRat _redrat = _gameObject.GetComponent<RedRat>();
            
            //Debug.Log(_redrat.GetCurrentEnumState());
            
            if (_redrat.GetCurrentEnumState() == RedRat.RedRatStateEnum.GROGGY && _redrat != null)
            {
                Vector2 directionToWall = (transform.position - col.transform.position).normalized;
                Rigidbody2D colRigidbody = _gameObject.GetComponent<Rigidbody2D>();
                
                Debug.Log(directionToWall);

                if (colRigidbody != null)
                {
                    // 튕겨내는 힘을 AddForce 메서드로 적용
                    colRigidbody.AddForce(directionToWall * bounceForce, ForceMode2D.Impulse);
                }
                
            }

        }

        if( col.gameObject.CompareTag("Player")){
        
            // 충돌한 객체의 RigidBody2D를 얻어옵니다.
            Rigidbody2D colRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

            if (colRigidbody != null)
            {
                // 충돌한 객체의 속도를 절대값으로 얻어옵니다.
                float collisionVelocity = Mathf.Abs(colRigidbody.velocity.magnitude);
                //Debug.Log(collisionVelocity);

                // 날아오던 속도가 설정한 임계값보다 크면 게임 오버 처리를 수행합니다.
                if (collisionVelocity >= 50f)
                {
                    // 게임 오버 처리 로직을 여기에 추가하십시오.
                    Time.timeScale = 0.0f;
                    //Debug.Log("게임 오버!");
                }
            }
        }
    }
}