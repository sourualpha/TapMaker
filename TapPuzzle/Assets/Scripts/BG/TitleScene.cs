using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // �萔�̒�`
    private const float TARGET_VOLUME = 0.4f;
    private const float VOLUME_STEP = 0.05f;
    private const float VOLUME_UP_INTERVAL = 0.1f;

    [SerializeField]
    private Fade fade; // FadeCanvas�̎擾

    [SerializeField]
    private float fadeTime; // �t�F�[�h���ԁi�b�j

    private AudioSource audioSource; // BGM�p��AudioSource

    [SerializeField]
    private AudioClip soundEffect; // ���ʉ�

    private static TitleScene instance; // �V���O���g���C���X�^���X
    public bool isplayMode; // �v���C���[�h�t���O
    public bool istitle; // �^�C�g���t���O

    // �V�[���J�n���ɌĂ΂��
    void Start()
    {
        // �^�C�g���V�[���ł���΃t�F�[�h�A�E�g���s��
        if (istitle)
        {
            fade.FadeOut(fadeTime);
        }

        isplayMode = false;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(VolumeUp()); 
        DontDestroyOnLoad(gameObject); 
    }

    // �v���C���[�h�ւ̃V�[���J��
    public void ChangePlayMode()
    {
        Initiate.Fade("GameScene", Color.white, 1.0f);
    }

    // �^�C�g���V�[���ɖ߂�
    public void BackTitle()
    {
        Initiate.Fade("TitleScene", Color.white, 1.0f);
    }

    // �N���G�C�g���[�h�ւ̃V�[���J��
    public void CreateMode()
    {
        audioSource.PlayOneShot(soundEffect); // ���ʉ����Đ�

        // �t�F�[�h���s���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
            istitle = false;
        });
    }

    // �v���C���[�h�ւ̃V�[���J�ڂ��s��
    public void PlayMode()
    {
        audioSource.PlayOneShot(soundEffect); // ���ʉ����Đ�

        // �t�F�[�h���s���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
        isplayMode = true;
        istitle = false;
    }


    IEnumerator VolumeUp()
    {
        while (audioSource.volume <= TARGET_VOLUME)
        {
            audioSource.volume += VOLUME_STEP;
            yield return new WaitForSeconds(VOLUME_UP_INTERVAL);
        }
    }
}
