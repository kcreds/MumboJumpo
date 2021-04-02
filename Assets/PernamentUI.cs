using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PernamentUI : MonoBehaviour
{
    public int cherries = 0;
    public TextMeshProUGUI Licznik;


    public static PernamentUI pern;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!pern)
        {
            pern = this;
        }
        else
            Destroy(gameObject);
    }

    public void Reset()
    {
        cherries = 0;
        Licznik.text = cherries.ToString();
    }


}
