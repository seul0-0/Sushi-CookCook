using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    //JsonConvert 클래스의 SerializeObject() 함수를 이용해서
    //오브젝트를 문자열로 된 JSON 데이터로 변환하여 반환하는 처리
    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    //DeserializeObject() 함수를 이용해서 문자열로 된 JSON 데이터를 받아서
    //원하는 타입의 객체로 반환하는 처리
    T JsonToOject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    private void Start()
    {
        JTestClass jtc = new JTestClass(true);
        string jsonData = ObjectToJson(jtc);
        Debug.Log(jsonData);
        // 저장할 내용
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
