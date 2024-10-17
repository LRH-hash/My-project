using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;//关于场景的操作

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
    public void GameAgain()//场景重开函数
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();//获得初始场景
        SceneManager.LoadScene(scene.name);//获取的场景必须通过字符串载入
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine("Load",_data);
//通过延迟释放，使closestCheckpointId能够正确的被存储调用
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
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)//好像这玩意没有用啊
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

    private void PlacePlayerAtClosestCheckpoint(GameData _data)//传送至最近检查点函数
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

    private Checkpoint FindClosestCheckpoint()//寻找最近检查点的函数
    {
        float closetDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)//遍历检查点比较距离寻找最近的检查点
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