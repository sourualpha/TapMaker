using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    #region ‰æ–Ê‘JˆÚ
    public void ChangePlayMode()
    {
        Initiate.Fade("GameScene", Color.white, 1.0f);
    }

    public void BackTitle()
    {
        Initiate.Fade("TitleScene", Color.white, 1.0f);
        
    }

    public void SelectStage()
    {
        Initiate.Fade("StageSelect",Color.white, 2.0f);
    }
    #endregion
}
