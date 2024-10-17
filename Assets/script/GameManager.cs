using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;//���ڳ����Ĳ���

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private string closestCheckpointId;
    public GameObject lostcurrency;
    public Vector2 currencyPosition;
    public float currencyAmount;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
            instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
    }
    public void GameAgain()//�����ؿ�����
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();//��ó�ʼ����
        SceneManager.LoadScene(scene.name);//��ȡ�ĳ�������ͨ���ַ�������
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine("Load",_data);
//ͨ���ӳ��ͷţ�ʹclosestCheckpointId�ܹ���ȷ�ı��洢����
    }
    public IEnumerator Load(GameData _data)
    {
        yield return new WaitForSeconds(.1f);
        PlacePlayerAtClosestCheckpoint(_data);
        CheckPoint(_data);
        currencyPosition = _data.currencyPosition;
        currencyAmount = _data.currencyAmount;
        if (currencyAmount > 0)
        {
            GameObject newcurrency = Instantiate(lostcurrency, currencyPosition, Quaternion.identity);
            newcurrency.GetComponent<LostcurrencyController>().currency = (int)currencyAmount;
        }
        PlayerManager.instance.currentSouls = 0;

    }

    private void CheckPoint(GameData _data)
    {
        if (_data.checkpoints.Count==0)
            return;
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)//����������û���ð�
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value == true)
                {
                    checkpoint.ActivateCheckpoint();
                }
            }
        }
    }

    private void PlacePlayerAtClosestCheckpoint(GameData _data)//������������㺯��
    {
        if (_data.closestCheckpointId ==null)
        {
            PlayerManager.instance.player.transform.position = new Vector3(20, 2, 0);
            return;
        }
        closestCheckpointId = _data.closestCheckpointId;
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (closestCheckpointId == checkpoint.id)
            {
                PlayerManager.instance.player.transform.position = checkpoint.transform.position + new Vector3(0, 1, 0);
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.currencyPosition = PlayerManager.instance.player.transform.position;
        _data.currencyAmount = PlayerManager.instance.currentSouls;  
        _data.closestCheckpointId = FindClosestCheckpoint().id;                 
        _data.checkpoints.Clear();
        foreach (Checkpoint checkpoint in checkpoints)
        {
                _data.checkpoints.Add(checkpoint.id, checkpoint.activationStatus);
        }

    }

    private Checkpoint FindClosestCheckpoint()//Ѱ���������ĺ���
    {
        float closetDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)//��������ȽϾ���Ѱ������ļ���
        {
            float distanceToCheckpoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkpoint.transform.position);
            if (distanceToCheckpoint < closetDistance && checkpoint.activationStatus == true)
            {
                closetDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;

    }
    public void GamePaUse(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}