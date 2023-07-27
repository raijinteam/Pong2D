using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveData : MonoBehaviour
{
    [SerializeField] private TMP_InputField field_Username;
    [SerializeField] private TMP_InputField field_Password;





    public async void OnClick_SaveData()
    {
        string username = field_Username.text;
        string password = field_Password.text;

        Debug.Log($"Username  { username} password {password} ");

        InitializationExample.instance.SaveDataInCloud(username , password);
    }
}
