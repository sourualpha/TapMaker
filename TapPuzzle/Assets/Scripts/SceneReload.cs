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


    // Start is called before the first frame update
    void Start()
    {
        //�V�[���J�n���Ƀt�F�[�h���|����
        fade.FadeOut(fadeTime);
    }

    //�e�{�^�������������̏���
    public void SceneTransition()
    {
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }
}
