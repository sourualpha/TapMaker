using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // 定数の定義
    private const float TARGET_VOLUME = 0.4f;
    private const float VOLUME_STEP = 0.05f;
    private const float VOLUME_UP_INTERVAL = 0.1f;

    [SerializeField]
    private Fade fade; // FadeCanvasの取得

    [SerializeField]
    private float fadeTime; // フェード時間（秒）

    private AudioSource audioSource; // BGM用のAudioSource

    [SerializeField]
    private AudioClip soundEffect; // 効果音

    [SerializeField]
    private GameObject optionPanel; //オプションパネル

    private static TitleScene instance; // シングルトンインスタンス

    public bool isplayMode; // プレイモードフラグ
    public bool istitle; // タイトルフラグ
    public bool isoption; //オプションパネルを開いてるかどうかのフラグ

    // シーン開始時に呼ばれる
    void Start()
    {
        // タイトルシーンであればフェードアウトを行う
        if (istitle)
        {
            fade.FadeOut(fadeTime);
        }

        optionPanel.SetActive(false);
        isplayMode = false;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(VolumeUp()); 
        DontDestroyOnLoad(gameObject); 
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isoption)
            {
                optionPanel.SetActive(true);
                isoption = true;
            }
            else
            {
                optionPanel.SetActive(false);
                isoption = false;
            }
        }
    }

    // プレイモードへのシーン遷移
    public void ChangePlayMode()
    {
        Initiate.Fade("GameScene", Color.white, 1.0f);
    }

    // タイトルシーンに戻る
    public void BackTitle()
    {
        Initiate.Fade("TitleScene", Color.white, 1.0f);
    }

    // クリエイトモードへのシーン遷移
    public void CreateMode()
    {
        audioSource.PlayOneShot(soundEffect); // 効果音を再生

        // フェードを行ってからシーン遷移する
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
            istitle = false;
        });
    }

    // プレイモードへのシーン遷移を行う
    public void PlayMode()
    {
        audioSource.PlayOneShot(soundEffect); // 効果音を再生

        // フェードを行ってからシーン遷移する
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
        isplayMode = true;
        istitle = false;
    }

    public void ExitButton()
    {
        Application.Quit();
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
