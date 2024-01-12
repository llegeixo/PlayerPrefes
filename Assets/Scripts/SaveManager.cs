using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Text _userNameText;
    [SerializeField] private Text _userScoreText;
    [SerializeField] private Text _userPositionText;

    [SerializeField] private string _userName;
    [SerializeField] private int _userScore;
    [SerializeField] private Vector3 _userPosition;

    void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("name", _userName);
        PlayerPrefs.SetInt("score", _userScore);
        PlayerPrefs.SetFloat("positionX", _userPosition.x);
        PlayerPrefs.SetFloat("positionZ", _userPosition.z);
        PlayerPrefs.SetFloat("positionY", _userPosition.y);

        LoadData();
    }

    void LoadData()
    {
        _userNameText.text = "User name: " + PlayerPrefs.GetString("name", "No name");
        _userScoreText.text =  "User score: " + PlayerPrefs.GetInt("score", 0).ToString();
        _userPositionText.text = "Plyer position: " + PlayerPrefs.GetFloat("positionX", 0).ToString() + "x " +
                                PlayerPrefs.GetFloat("positionZ", 0).ToString() + "y " + 
                                PlayerPrefs.GetFloat("positionY", 0).ToString() + "z ";
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("positionX");
        PlayerPrefs.DeleteKey("positionY");
        PlayerPrefs.DeleteKey("positionZ");

        PlayerPrefs.DeleteAll();

        LoadData();
    }
}
