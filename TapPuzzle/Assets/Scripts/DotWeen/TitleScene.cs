using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    GameObject title; //�^�C�g���̃e�L�X�g

    [SerializeField]
    GameObject playButton; //�v���C�{�^��

    [SerializeField]
    GameObject createButton; //����{�^��

    [SerializeField]
    private Fade fade; //FadeCanvas�擾


    [SerializeField]
    private float fadeTime;  //�t�F�[�h���ԁi�b�j

    private static TitleScene instance;
    public bool playMode;
    // Start is called before the first frame update
    void Start()
    { 
        //�V�[���J�n���Ƀt�F�[�h���|����
        fade.FadeOut(fadeTime);
        playMode = false;
        title.transform.DOMoveY(540f, 1f);
        
        playButton.transform.DOMoveY(340f, 2f).SetDelay(1f).SetEase(Ease.OutBounce);

        createButton.transform.DOMoveY(340f, 2f).SetDelay(1.5f).SetEase(Ease.OutBounce);

        


        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
    }

    public void PlayMode()
    {
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
        playMode = true;
    }
}
