using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    public GameObject[] audioSoundPrefab; // �ǽ��� ȿ����

    // ȿ������ ����ϴ� �޼���
    public void PlayEffect(int idx)
    {
        // AudioSource �������� �ν��Ͻ��� ����
        GameObject effectSource = Instantiate(audioSoundPrefab[idx], transform);
        AudioSource audioSource = effectSource.GetComponent<AudioSource>();

        audioSource.Play();

        // ȿ���� ����� ������ �ν��Ͻ� �ı�
        Destroy(effectSource, audioSource.clip.length);
    }

}
