using System;
using System.IO;
using UnityEngine;
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool encryptData = false;//是否加密判断
    private string codeWord = "LRH";//加密密码

    public FileDataHandler(string _dataDirPath, string _dataFilePath, bool _encryptData)//构造函数拿到需要保存的位置和文件名称
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFilePath;
        encryptData = _encryptData;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);//合成路径函数 将位置和文件合并成实际的可以读取的路径

        try//用try防止其报错
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));//通过路径创建出需要的文件，存在就不创建了
            string dataToStore = JsonUtility.ToJson(_data, true);//将传过来的gameData转换成文本形式并且使其可读

            if (encryptData)
                dataToStore = EncryptDecrypt(dataToStore);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))//两个using 第一个进入文件使其变为可编写模式
            {
                using (StreamWriter writer = new StreamWriter(stream))//第二个拿到文件对其进行编辑
                {
                    writer.Write(dataToStore);//写入函数
                }
            }

        }

        catch (Exception e)
        {
            Debug.LogError("Error on trying to save data to file " + fullPath + "\n" + e);
        }
    }
    public GameData Load()//同上
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))//存在才能操作
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);//转换为游戏需要的类型
            }
            catch (Exception e)
            {
                Debug.LogError(e);

            }
        }
        return loadData;
    }

    public void Delete()//删除对应文件函数
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))//存在才能删除 File操作
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string _data)//数据加密函数
    {
        string modifiedData = "";//返回的加密文件
        for (int i = 0; i < _data.Length; i++)
        {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);//怎么把加密文件回退到没加密，搞不懂
        }

        return modifiedData;
    }
}