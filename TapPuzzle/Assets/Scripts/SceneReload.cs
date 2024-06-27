using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReload : MonoBehaviour
{
    //FadeCanvas取得
    [SerializeField]
    private Fade fade;

    //フェード時間（秒）
    [SerializeField]
    private float fadeTime;

    AudioSource audioSource; //BGM

    // Start is called before the first frame update
    void Start()
    {
        //シーン開始時にフェードを掛ける
        fade.FadeOut(fadeTime);

        audioSource = GetComponent<AudioSource>();

        StartCoroutine("VolumeUp");
    }

    //各ボタンを押した時の処理
    public void SceneTransition()
    {
        StartCoroutine("VolumeDown");
        //フェードを掛けてからシーン遷移する
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
