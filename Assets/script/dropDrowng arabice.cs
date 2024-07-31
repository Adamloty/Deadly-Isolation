using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEditor.Experimental.GraphView;


public class dropDrowngarabice : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown lang;
    public GameObject Arb;
    public GameObject English;
    public GameObject Arbtitle;
    public GameObject Englishtitle;
    //[SerializeField] private GameObject SittingsArab;
    //[SerializeField] private GameObject SittingsEnglish;
    void Start()
    {
        
    }
    void Update()
    {
        if (lang.value == 0)
        {
            Arb.SetActive(false);
           English.SetActive(true);
            Arbtitle.SetActive(false);
            Englishtitle.SetActive(true);
            //SittingsEnglish.SetActive(true);
            //SittingsArab.SetActive(false);


        }
        else if (lang.value == 1)
        {
            Arb.SetActive(true);
            English.SetActive(false);
            Arbtitle.SetActive(true);
            Englishtitle.SetActive(false);
            //SittingsEnglish.SetActive(false);
            //SittingsArab.SetActive(true);
        }
    }
}
