using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button MainButton;
    public Sprite onSprite;
    public Sprite offSprite;
    int Points;
    int PointsNum = 1;
    int DoubleClickPointsTime;
    public Text DoubleClickPointsTimerText;
    public GameObject GreenFire;
    int BonusIsActive = 0;
    public GameObject ClickParent, clickTextPrefab;
    private ClickObject[] clickTextPool = new ClickObject[15];

    public Text CooldownClickText;
    int ClickCoolDownTime;
    public Animator ClickButtonAnimator;
    public GameObject ClickBonusButton;
    public GameObject ClickSandClocks;

    private int ClickNum;

    private void Start()
    {
        ClickButtonAnimator = ClickBonusButton.GetComponent<Animator>();

        for (int i = 0; i < clickTextPool.Length; i++)
        {
            clickTextPool[i] = Instantiate(clickTextPrefab, ClickParent.transform).GetComponent<ClickObject>();
        }
    }

    private void OnMouseDown()
    {
        MainButton.GetComponent<SpriteRenderer>().sprite = onSprite;
        Points = PlayerPrefs.GetInt("Points");
        Points += PointsNum;
    }

    private void OnMouseUp()
    {
        PlayerPrefs.SetInt("Points", Points);
        MainButton.GetComponent<SpriteRenderer>().sprite = offSprite;

        clickTextPool[ClickNum].StartMotion(PointsNum);
        ClickNum = ClickNum == clickTextPool.Length - 1 ? 0 : ClickNum + 1;
    }

    public void GetDoubleClickPoints()
    {
        if (BonusIsActive == 0)
        {
            PointsNum = 3;
            BonusIsActive = 1;
            DoubleClickPointsTime = 60;
            StartCoroutine(DoubleClickPoints());
            StartCoroutine(DoubleClickPointsTimer());
        }
    }

    private IEnumerator DoubleClickPoints()
    {
        yield return new WaitForSeconds(60);
        PointsNum = 1;
        StopCoroutine(DoubleClickPointsTimer());
    }

    private IEnumerator DoubleClickPointsTimer()
    {
        while (DoubleClickPointsTime >= 0)
        {
            if (DoubleClickPointsTime == 60)
            {
                GreenFire.SetActive(true);
                DoubleClickPointsTimerText.text = DoubleClickPointsTime.ToString();
                DoubleClickPointsTime--;
            }
            yield return new WaitForSeconds(1);
            DoubleClickPointsTimerText.text = DoubleClickPointsTime.ToString();
            DoubleClickPointsTime--;
        }

        if (DoubleClickPointsTime < 0)
        {
            DoubleClickPointsTimerText.text = " ";
            GreenFire.SetActive(false);
            ClickCoolDownTime = 180;
            StartCoroutine(SkillCooldown());
        }
    }

    private IEnumerator SkillCooldown()
    {
        while (ClickCoolDownTime >= 0)
        {
            if (ClickCoolDownTime == 180)
            {
                ClickSandClocks.SetActive(true);
                ClickButtonAnimator.SetBool("IsUnactive", true);
                ClickButtonAnimator.SetTrigger("UnActive");
                CooldownClickText.text = "\n\n" + ClickCoolDownTime.ToString();
                ClickCoolDownTime--;
            }
            yield return new WaitForSeconds(1);
            CooldownClickText.text = "\n\n" + ClickCoolDownTime.ToString();
            ClickCoolDownTime--;
        }

        if (ClickCoolDownTime < 0)
        {
            ClickSandClocks.SetActive(false);
            CooldownClickText.text = " ";
            BonusIsActive = 0;
            ClickButtonAnimator.SetBool("IsUnactive", false);
        }
    }
}
