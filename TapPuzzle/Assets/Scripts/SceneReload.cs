using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReload : MonoBehaviour
{
    //FadeCanvas�擾
    [SerializeField]
    private Fade fade;

    //�t�F�[�h���ԁi�b�j
    [SerializeField]
    private float fadeTime;

    AudioSource audioSource; //BGM

    // Start is called before the first frame update
    void Start()
    {
        //�V�[���J�n���Ƀt�F�[�h���|����
        fade.FadeOut(fadeTime);

        audioSource = GetComponent<AudioSource>();

        StartCoroutine("VolumeUp");
    }

    //�e�{�^�������������̏���
    public void SceneTransition()
    {
        StartCoroutine("VolumeDown");
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }

    IEnumerator VolumeDown()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator VolumeUp()
    {
        while (audioSource.volume <= 0.3f)
        {
            audioSource.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
