using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    public GameObject[] audioSoundPrefab; // 피스톨 효과음

    // 효과음을 재생하는 메서드
    public void PlayEffect(int idx)
    {
        // AudioSource 프리팹의 인스턴스를 생성
        GameObject effectSource = Instantiate(audioSoundPrefab[idx], transform);
        AudioSource audioSource = effectSource.GetComponent<AudioSource>();

        audioSource.Play();

        // 효과음 재생이 끝나면 인스턴스 파괴
        Destroy(effectSource, audioSource.clip.length);
    }

}
