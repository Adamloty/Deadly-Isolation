using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManegar : MonoBehaviourPunCallbacks
{
    [SerializeField] private SaveSystem save;
    [SerializeField] private Slider slide;
    [SerializeField] private Text slidervalue;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if(slide != null)
        {
            slidervalue.text = Convert.ToInt32(slide.value).ToString();
        }
    }
    public void Play()
    {
        if(save != null&&slide!=null)
        {
            save.SaveFloat("sense", slide.value);
        }
        SceneManager.LoadScene("Loading");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Cursorvisible()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
