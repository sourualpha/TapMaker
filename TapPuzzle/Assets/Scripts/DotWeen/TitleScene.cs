using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    GameObject title; //タイトルのテキスト

    [SerializeField]
    GameObject playButton; //プレイボタン

    [SerializeField]
    GameObject createButton; //つくるボタン

    [SerializeField]
    private Fade fade; //FadeCanvas取得


    [SerializeField]
    private float fadeTime;  //フェード時間（秒）

    private static TitleScene instance;
    public bool playMode;
    // Start is called before the first frame update
    void Start()
    { 
        //シーン開始時にフェードを掛ける
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
        //フェードを掛けてからシーン遷移する
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
    }

    public void PlayMode()
    {
        //フェードを掛けてからシーン遷移する
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
        playMode = true;
    }
}
