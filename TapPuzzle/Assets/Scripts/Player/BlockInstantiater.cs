using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
public class BlockInstantiater : MonoBehaviour
{

    Vector2 displayCenter;

    // ブロックを設置する位置を一応リアルタイムで格納
    private Vector3 pos;


    #region SerializeField
    [SerializeField]
    private List<GameObject> blockPrefab; //ブロックのリスト

    [SerializeField]
    private Camera createcam; //クリエイトモードのカメラ

    [SerializeField]
    private Camera playcam; //クリエイトモードのカメラ


    [SerializeField]
    private GameObject optionpanel; //設定のパネル

    [SerializeField]
    private GameObject player; //プレイヤー

    [SerializeField]
    private GameObject playercanvas; //プレイヤーのキャンバス

    [SerializeField]
    GameObject ClearPanel; //クリア画面のパネル

    [SerializeField]
    GameObject SavePanel;//セーブのためのパネル

    [SerializeField]
    private AudioClip soundEffect;//効果音

    [SerializeField]
    private AudioClip switchBlock;//効果音

    #endregion

    AudioSource audioSource; //BGM

    // Use this for initialization
    int blocknumber; //ブロックリストの番号
    int blockCount;
    public GameObject guideBlock; // ガイドブロック
    private Vector3 blockSize; // ブロックのサイズ
    private JsonDataLoad dataLoad;//データのロード

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dataLoad = GetComponent<JsonDataLoad>();

        // ↓ 画面中央の平面座標を取得する
        displayCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        blocknumber= 0;
        blockCount = dataLoad.blockCount;
        Debug.Log(blockCount);

        optionpanel.SetActive(false);
        SavePanel.SetActive(false);
        
        // ガイドブロックの生成
        guideBlock = Instantiate(guideBlock, Vector3.zero, Quaternion.identity);
        // 最初のブロックを使用してサイズを取得
        blockSize = blockPrefab[blocknumber].transform.localScale;
        guideBlock.SetActive(false); // 最初は非表示にする
    }


    void Update()
    {
        // ↓ 「カメラからのレイ」を画面中央の平面座標から飛ばす
        Ray ray = createcam.ScreenPointToRay(displayCenter);

        // ↓ 当たったオブジェクト情報を格納する変数
        RaycastHit hit;

        // ↓ Physics.Raycast() でレイを飛ばす
        if (Physics.Raycast(ray, out hit))
        {
            // ↓ 生成位置の変数の値を「ブロックの向き + ブロックの位置」
            pos = hit.normal + hit.collider.transform.position;
            if(hit.collider.CompareTag("Cube"))
            {
                UpdateGuideBlockPosition(pos); 
                // 右クリック
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    audioSource.PlayOneShot(soundEffect);
                    // 生成位置の変数の座標にブロックを生成
                    Instantiate(blockPrefab[blocknumber], pos, Quaternion.identity);

                    float distance = 100; // 飛ばす&表示するRayの長さ
                    float duration = 3;   // 表示期間（秒）

                    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
                    blockCount++;
                }   

                // 左クリック
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // ↓ 生成位置の変数の値を「ブロックの向き + ブロックの位置」
                    pos = hit.normal + hit.collider.transform.position;


                    // ↓ レイが当たっているオブジェクトを削除
                    Destroy(hit.collider.gameObject);
                    blockCount--;
                }
            }
            else
            {
                // 何もないときにガイドブロックを非表示にする
                guideBlock.SetActive(false);
            }

            DrawBlockOutline(pos, blockPrefab[blocknumber].transform.localScale);



        }
        else
        {
            //ブロックが何もない場合に真ん中に一つ目を作る
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (blockCount == 0)
                {
                    Instantiate(blockPrefab[blocknumber], Vector3.zero, Quaternion.identity);
                    blockCount++;
                    Debug.Log("aaa");
                }
            }
            // レイが何もヒットしないときにもガイドブロックを非表示にする
            guideBlock.SetActive(false);
        }


        
    }

    #region ガイドブロック
    // ガイドブロックの位置に枠線を描画するメソッド
    void DrawBlockOutline(Vector3 position, Vector3 blockSize)
    {
        float halfX = blockSize.x / 2f;
        float halfY = blockSize.y / 2f;
        float halfZ = blockSize.z / 2f;

        //Vector3[] corners = new Vector3[]
        //{
        //new Vector3(position.x - halfX, position.y - halfY, position.z - halfZ),
        //new Vector3(position.x + halfX, position.y - halfY, position.z - halfZ),
        //new Vector3(position.x - halfX, position.y + halfY, position.z - halfZ),
        //new Vector3(position.x + halfX, position.y + halfY, position.z - halfZ),
        //new Vector3(position.x - halfX, position.y - halfY, position.z + halfZ),
        //new Vector3(position.x + halfX, position.y - halfY, position.z + halfZ),
        //new Vector3(position.x - halfX, position.y + halfY, position.z + halfZ),
        //new Vector3(position.x + halfX, position.y + halfY, position.z + halfZ)
        //};
    }

    // ガイドブロックの位置を更新するメソッド
    void UpdateGuideBlockPosition(Vector3 position)
    {
            guideBlock.SetActive(true);
            guideBlock.transform.position = position;
    }

    void OnDestroy()
    {
        // シーンが終了するときにガイドブロックも破棄する
        Destroy(guideBlock);
    }

    #endregion

    #region ブロックの設定

    public void UpBlockButton()
    {
        blocknumber = 0;
        audioSource.PlayOneShot(switchBlock);
    }

    public void DownBlockButton()
    {
        blocknumber = 1;
        audioSource.PlayOneShot(switchBlock);
    }

    public void RightBlockButton()
    {
        blocknumber = 2;
        audioSource.PlayOneShot(switchBlock);
    }

    public void LeftBlockButton()
    {
        blocknumber = 3;
        audioSource.PlayOneShot(switchBlock);
    }

    public void ForwardBlockButton()
    {
        blocknumber = 4;
        audioSource.PlayOneShot(switchBlock);
    }

    public void BackBlockButton()
    {
        blocknumber = 5;
        audioSource.PlayOneShot(switchBlock);
    }

    public void CreateBlock()
    {

        // ↓ 「カメラからのレイ」を画面中央の平面座標から飛ばす
        Ray ray = createcam.ScreenPointToRay(displayCenter);
        // ↓ 当たったオブジェクト情報を格納する変数
        RaycastHit hit;
        if (blockCount == 0)
        {
            Instantiate(blockPrefab[blocknumber], Vector3.zero, Quaternion.identity);
            blockCount++;
 
        }        
        if (Physics.Raycast(ray, out hit))
        {
            audioSource.PlayOneShot(soundEffect);
            // ↓ 生成位置の変数の値を「ブロックの向き + ブロックの位置」
            pos = hit.normal + hit.collider.transform.position;
            // 生成位置の変数の座標にブロックを生成
            Instantiate(blockPrefab[blocknumber], pos, Quaternion.identity);
            float distance = 100; // 飛ばす&表示するRayの長さ
            float duration = 3;   // 表示期間（秒）
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
            blockCount++;

        }
    }

    public void DeleteBlock()
    {
        // ↓ 「カメラからのレイ」を画面中央の平面座標から飛ばす
        Ray ray = createcam.ScreenPointToRay(displayCenter);
        // ↓ 当たったオブジェクト情報を格納する変数
        RaycastHit hit;

        // ↓ Physics.Raycast() でレイを飛ばす
        if (Physics.Raycast(ray, out hit))
        {
            // ↓ 生成位置の変数の値を「ブロックの向き + ブロックの位置」
            pos = hit.normal + hit.collider.transform.position;


            // ↓ レイが当たっているオブジェクトを削除
            Destroy(hit.collider.gameObject);
            blockCount--;
        }
    }
    #endregion

}