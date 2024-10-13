using System;
using System.IO;
using UnityEngine;
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool encryptData = false;//�Ƿ�����ж�
    private string codeWord = "LRH";//��������

    public FileDataHandler(string _dataDirPath, string _dataFilePath, bool _encryptData)//���캯���õ���Ҫ�����λ�ú��ļ�����
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFilePath;
        encryptData = _encryptData;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);//�ϳ�·������ ��λ�ú��ļ��ϲ���ʵ�ʵĿ��Զ�ȡ��·��

        try//��try��ֹ�䱨��
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));//ͨ��·����������Ҫ���ļ������ھͲ�������
            string dataToStore = JsonUtility.ToJson(_data, true);//����������gameDataת�����ı���ʽ����ʹ��ɶ�

            if (encryptData)
                dataToStore = EncryptDecrypt(dataToStore);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))//����using ��һ�������ļ�ʹ���Ϊ�ɱ�дģʽ
            {
                using (StreamWriter writer = new StreamWriter(stream))//�ڶ����õ��ļ�������б༭
                {
                    writer.Write(dataToStore);//д�뺯��
                }
            }

        }

        catch (Exception e)
        {
            Debug.LogError("Error on trying to save data to file " + fullPath + "\n" + e);
        }
    }
    public GameData Load()//ͬ��
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))//���ڲ��ܲ���
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

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);//ת��Ϊ��Ϸ��Ҫ������
            }
            catch (Exception e)
            {
                Debug.LogError(e);

            }
        }
        return loadData;
    }

    public void Delete()//ɾ����Ӧ�ļ�����
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))//���ڲ���ɾ�� File����
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string _data)//���ݼ��ܺ���
    {
        string modifiedData = "";//���صļ����ļ�
        for (int i = 0; i < _data.Length; i++)
        {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);//��ô�Ѽ����ļ����˵�û���ܣ��㲻��
        }

        return modifiedData;
    }
}