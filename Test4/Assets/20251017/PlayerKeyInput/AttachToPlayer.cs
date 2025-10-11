using UnityEngine;

public class AttachToPlayer : MonoBehaviour
{
    void Awake()
    {

        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            transform.SetParent(player.transform);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Debug.Log($"{gameObject.name} 오브젝트가 {player.name}의 자식으로 설정되었습니다.");
        }
        else
        {
            Debug.LogWarning("Player 오브젝트를 찾을 수 없습니다!");
        }
    }
}
