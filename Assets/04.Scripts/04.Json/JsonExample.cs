using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    //JsonConvert Ŭ������ SerializeObject() �Լ��� �̿��ؼ�
    //������Ʈ�� ���ڿ��� �� JSON �����ͷ� ��ȯ�Ͽ� ��ȯ�ϴ� ó��
    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    //DeserializeObject() �Լ��� �̿��ؼ� ���ڿ��� �� JSON �����͸� �޾Ƽ�
    //���ϴ� Ÿ���� ��ü�� ��ȯ�ϴ� ó��
    T JsonToOject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    private void Start()
    {
        JTestClass jtc = new JTestClass(true);
        string jsonData = ObjectToJson(jtc);
        Debug.Log(jsonData);
        // ������ ����
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream
            (string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

}
