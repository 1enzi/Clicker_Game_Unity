using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatScript : MonoBehaviour
{
    int CatIsSleep = 0;
    int AlreadySleep = 0;
    public Animator CatAnimator;
    int IsLicking = 0;
    int CanPlayPassive = 1;
    public Text ActivityText;

    void Start()
    {
        CatAnimator = GetComponent<Animator>();
        StartCoroutine(PassiveAnimation());
        ActivityText.text = "Активность: \n\n Бодровствует";

        if (PlayerPrefs.GetInt("AlreadySleep") == 1)
        {
            CatAnimator.SetBool("AlreadySleep", true);
            CatAnimator.SetBool("IsSleep", true);
            CatIsSleep = 1;
            PlayerPrefs.SetInt("CatIsSleep", CatIsSleep);
        }
    }

    private void OnMouseUp()
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
            CanPlayPassive = 1;
        }

        else
        {
            if (IsLicking == 0)
            {
                ActivityText.text = "Активность: \n\n Умывается";
                StartCoroutine(WaitForMyAnim());
                CatAnimator.SetTrigger("Lick");
                CatAnimator.SetTrigger("IsNotLick");
                IsLicking = 1;
                CanPlayPassive = 0;
            }
        }
    }

    private IEnumerator WaitForMyAnim()
    {
        yield return new WaitForSeconds(4);
        ActivityText.text = "Активность: \n\n Бодровствует";
        IsLicking = 0;
        CanPlayPassive = 1;
    }

    private IEnumerator WaitForUpdateText()
    {
        yield return new WaitForSeconds(6);
        ActivityText.text = "Активность: \n\n Бодровствует";
    }

    private IEnumerator PassiveAnimation()
    {
        while (true)
        {
            int rand = Random.Range(15, 30);
            yield return new WaitForSeconds(rand);

            if (CanPlayPassive == 1 & PlayerPrefs.GetInt("CatIsSleep") == 0)
            {
                int randPassive = Random.Range(1, 3);

                if (randPassive == 1)
                {
                    CatAnimator.SetTrigger("GoAround");
                    if ((CanPlayPassive != 0 || PlayerPrefs.GetInt("CatIsSleep") != 1))
                    {
                        ActivityText.text = "Активность: \n\n Гуляет";
                        StartCoroutine(WaitForUpdateText());
                    }
                }

                else if (randPassive == 2)
                {
                    CatAnimator.SetTrigger("GoAroundLeft");
                    if ((CanPlayPassive != 0 || PlayerPrefs.GetInt("CatIsSleep") != 1))
                    {
                        ActivityText.text = "Активность: \n\n Гуляет";
                        StartCoroutine(WaitForUpdateText());
                    }
                }
            }

            else if (CanPlayPassive == 0 || PlayerPrefs.GetInt("CatIsSleep") == 1)
            {
                CatAnimator.ResetTrigger("GoAround");
                CatAnimator.ResetTrigger("GoAroundLeft");
            }
        }
    }
}

