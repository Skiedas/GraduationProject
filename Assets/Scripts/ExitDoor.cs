using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private GameOverPanel _panel;

    public void Init(GameOverPanel panel)
    {
        _panel = panel;
    }

    public void Interact()
    {
        _panel.Show();
    }
}
