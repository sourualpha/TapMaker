using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TitleScene : MonoBehaviour
{
    [SerializeField]
    GameObject title; //タイトルのテキスト

    [SerializeField]
    GameObject playButton; //プレイボタン

    [SerializeField]
    GameObject createButton; //つくるボタン

    private static TitleScene instance;
    public bool playMode;
    // Start is called before the first frame update
    void Start()
    {
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
        Initiate.Fade("StageSelect", Color.white, 2.0f);
    }

    public void PlayMode()
    {
        Initiate.Fade("StageSelect", Color.white, 2.0f);
        playMode= true;
    }
}
