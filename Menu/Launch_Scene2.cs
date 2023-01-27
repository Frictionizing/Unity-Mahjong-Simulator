using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launch_Scene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button x = GetComponent<Button>();
        x.onClick.AddListener(TaskLaunch);
    }

    // Update is called once per frame
    void TaskLaunch()
    {
        SceneManager.LoadScene("Server_Lobby");
    }
}
