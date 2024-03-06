using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private Fade fade; //FadeCanvas�擾

    [SerializeField]
    private float fadeTime;  //�t�F�[�h���ԁi�b�j

    AudioSource audioSource; //BGM

    [SerializeField]
    private AudioClip soundEffect;

    private static TitleScene instance;
    public bool isplayMode;
    public bool istitle;
    // Start is called before the first frame update
    void Start()
    { 
        //�V�[���J�n���Ƀt�F�[�h���|����
        if(istitle == true)
        {
            fade.FadeOut(fadeTime);
        }
        
        isplayMode = false;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("VolumeUp");
        DontDestroyOnLoad(gameObject);
    }

    public void ChangePlayMode()
    {
        Initiate.Fade("GameScene", Color.white, 1.0f);
    }

    public void BackTitle()
    {
        Initiate.Fade("TitleScene", Color.white, 1.0f);
    }

    public void CreateMode()
    { 
        audioSource.PlayOneShot(soundEffect);
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {

            SceneManager.LoadScene("StageSelect");
            istitle = false;
        });
    }

    public void PlayMode()
    { 
        audioSource.PlayOneShot(soundEffect);
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {

            SceneManager.LoadScene("StageSelect");
        });
        isplayMode = true;
        istitle = false;
    }

    IEnumerator VolumeUp()
    {
        while (audioSource.volume <= 0.4)
        {
            audioSource.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
