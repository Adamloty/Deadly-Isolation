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
    void Start()
    {
        
    }
    void Update()
    {
        if (lang.value == 0)
        {
            Arb.SetActive(false);
           English.SetActive(true);




        }
        else if (lang.value == 1)
        {
            Arb.SetActive(true);
            English.SetActive(false);

        }
    }
}
