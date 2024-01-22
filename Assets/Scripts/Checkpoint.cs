using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Vector3 _playerPosition;
    void Start()
    {
        LoadData();
    }

    
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            Debug.Log("Player entered checkpoint");
            PlayerPrefs.SetFloat("positionX", transform.position.x);
            PlayerPrefs.SetFloat("positionZ", transform.position.z);
            PlayerPrefs.SetFloat("positionY", transform.position.y);

        }
    
    }

    public void LoadData()
    {
        Debug.Log("Loading player data");
        float x = PlayerPrefs.GetFloat("positionX", 0f);
        float y = PlayerPrefs.GetFloat("positionY", 1f);
        float z = PlayerPrefs.GetFloat("positionZ", 0f);
        _playerPosition = new Vector3(x, y, z);

        
        transform.position = _playerPosition;
    }
}
