using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepScript : MonoBehaviour
{
    public Animator CatAnimator;
    public GameObject CatCharacter;
    int CatIsSleep = 0;
    int AlreadySleep = 0;
    public Text ActivityText;

    public void Start()
    {
        CatAnimator = CatCharacter.GetComponent<Animator>();
    }

    private void OnMouseUp()
    {
        if (PlayerPrefs.GetInt("CatIsSleep") == 0)
        {
            ActivityText.text = "Активность: \n\n Спит";
            CatAnimator.SetTrigger("Sleep");
            CatAnimator.SetTrigger("GoSleep");
            CatAnimator.SetBool("IsSleep", true);
            CatIsSleep = 1;
            PlayerPrefs.SetInt("CatIsSleep", CatIsSleep);
            AlreadySleep = 1;
            PlayerPrefs.SetInt("AlresdySleep", AlreadySleep);
        }
    }

    public void Awake()
    {
        if (PlayerPrefs.GetInt("CatIsSleep") == 1)
        {
            ActivityText.text = "Активность: \n\n Бодровствует";
            CatAnimator.SetBool("IsSleep", false);
            CatAnimator.SetBool("AlreadySleep", false);
            CatAnimator.ResetTrigger("Sleep");
            CatIsSleep = 0;
            PlayerPrefs.SetInt("CatIsSleep", CatIsSleep);
            AlreadySleep = 0;
            PlayerPrefs.SetInt("AlresdySleep", AlreadySleep);
        }
    }
}
