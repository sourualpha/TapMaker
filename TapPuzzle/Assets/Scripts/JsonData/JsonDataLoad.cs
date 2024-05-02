using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonDataLoad : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public string prefabName;
        public GameObject prefab;
    }

    public List<PrefabData> blockPrefabs; // �v���n�u�̃��X�g��Unity�C���X�y�N�^�Őݒ�
    private string currentStage; //���݂̃X�e�[�W�̖��O
    public InputField currentStageName; //���݂̃X�e�[�W�̖��O������ꏊ
    StageManager stageManagerInstance;
    public int blockCount;

    private void Awake()
    {
        
        stageManagerInstance = FindObjectOfType<StageManager>(); // StageManager�̃C���X�^���X���擾
        if (stageManagerInstance != null)
        {
            // ���Z�b�g�ƍēǂݍ���
            ResetBlocks();
            // stage�ϐ����Q��
            currentStage = stageManagerInstance.stage;
            currentStageName.text = currentStage;//�㏑���ۑ��̂��߂Ɍ��݂̃X�e�[�W�̖��O������
            LoadStage(currentStage);

            Debug.Log("Current Stage: " + currentStage);
        }
        else
        {
            Debug.LogError("StageManager�̃C���X�^���X��������܂���B");
        }
    }

    #region Json�f�[�^�̃��[�h�A�u���b�N�̔z�u
    void LoadStage(string stageName)
    {
        string filePath = Application.dataPath + "/stages.json";

        if (File.Exists(filePath))
        {
            // �t�@�C�������݂���ꍇ�̓��[�h
            string json = File.ReadAllText(filePath);
            StageManagement stageManagement = JsonUtility.FromJson<StageManagement>(json);

            // �w�肳�ꂽ�X�e�[�W���̃f�[�^������
            SaveData save = stageManagement.stages.Find(x => x.stageName == stageName);

            if (save != null)
            {
                // �ۑ����ꂽ�u���b�N�������ɍĐ���
                foreach (BlockData blockData in save.blocks)
                {
                    Vector3 position = new Vector3(blockData.position.x, blockData.position.y, blockData.position.z);

                    // �v���n�u�̖��O�����ɑΉ�����Prefab���擾
                    GameObject prefab = GetPrefabByName(blockData.prefabName);

                    if (prefab != null)
                    {
                        GameObject newBlock = Instantiate(prefab, position, Quaternion.identity);
                        newBlock.name = blockData.prefabName; // �u���b�N�̖��O��ݒ�
                        blockCount++;
                    }
                    else
                    {
                        Debug.LogWarning("Prefab��������܂���ł���: " + blockData.prefabName);
                    }
                }

                Debug.Log("�f�[�^�̃��[�h���������܂����B");
            }
            else
            {
                Debug.LogWarning("�w�肳�ꂽ�X�e�[�W���̃f�[�^��������܂���: " + stageName);
            }
        }
        else
        {
            Debug.LogError("�t�@�C����������܂���B");
        }
    }
    #endregion

    #region �v���n�u�̖��O�����ɑΉ�����Prefab���擾
    GameObject GetPrefabByName(string prefabName)
    {
        PrefabData prefabData = blockPrefabs.Find(x => x.prefabName == prefabName);
        if (prefabData != null)
        {
            return prefabData.prefab;
        }
        return null;
    }
    #endregion

    #region �z�u���Ă���u���b�N�̃��Z�b�g
    void ResetBlocks()
    {
        // �V�[�����̂��ׂĂ� GameObject ���擾
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        Debug.Log("�I�u�W�F�N�g���j������܂����B");
        currentStage = "";
        // �u���b�N�����𒊏o���ă��X�g�ɒǉ�
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Cube")) // �u���b�N�Ɋւ���^�O��ݒ肵�Ă���Ɖ���
            {
                Destroy(obj);
                Debug.Log("�I�u�W�F�N�g���j������܂����B");
            }
        }
    }
    #endregion
}
