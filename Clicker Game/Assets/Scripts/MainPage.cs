using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
    [SerializeField] int points;
    public Button MainButton;
    public Text PointsText;
    int passivePoints;
    int passivePointsTime;
    int DoublePointsTime;
    public Text DoublePointsTimerText;
    public GameObject Fire;
    int BonusIsActive = 0;
    public Text CooldownText;
    int CoolDownTime;
    public Animator ButtonAnimator;
    public GameObject BonusButton;
    public GameObject SandClocks;

    void Start()
    {
        ButtonAnimator = BonusButton.GetComponent<Animator>();
        passivePointsTime = 3;
        passivePoints = 1;
        points = PlayerPrefs.GetInt("Points");
        StartCoroutine(GetPassivePoints());
    }

    void Update()
    {
        points = PlayerPrefs.GetInt("Points");
        PointsText.text = points.ToString();
    }

    private IEnumerator GetPassivePoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(passivePointsTime);
            points = PlayerPrefs.GetInt("Points");
            points += passivePoints;
            PointsText.text = points.ToString();
            PlayerPrefs.SetInt("Points", points);
        }
    }

    public void GetDoublePoints()
    {
        if (BonusIsActive == 0)
        {
            passivePoints = 3;
            passivePointsTime = 1;
            BonusIsActive = 1;
            DoublePointsTime = 60;
            StartCoroutine(DoublePoints());
            StartCoroutine(DoublePointsTimer());
        }
    }

    private IEnumerator DoublePoints()
    {
        yield return new WaitForSeconds(60);
        passivePoints = 1;
        passivePointsTime = 3;
        StopCoroutine(DoublePointsTimer());
    }

    private IEnumerator DoublePointsTimer()
    {
        while (DoublePointsTime >= 0)
        {
            if (DoublePointsTime == 60)
            {
                Fire.SetActive(true);
                DoublePointsTimerText.text = DoublePointsTime.ToString();
                DoublePointsTime--;
            }
            yield return new WaitForSeconds(1);
            DoublePointsTimerText.text = DoublePointsTime.ToString();
            DoublePointsTime--;
        }

        if (DoublePointsTime < 0)
        {
            DoublePointsTimerText.text = " ";
            Fire.SetActive(false);
            CoolDownTime = 180;
            StartCoroutine(SkillCooldown());
        }
    }

    private IEnumerator SkillCooldown()
    {
        while (CoolDownTime >= 0)
        {
            if (CoolDownTime == 180)
            {
                SandClocks.SetActive(true);
                ButtonAnimator.SetBool("IsUnactive", true);
                ButtonAnimator.SetTrigger("UnActive");
                CooldownText.text = "\n\n" + CoolDownTime.ToString();
                CoolDownTime--;
            }
            yield return new WaitForSeconds(1);
            CooldownText.text = "\n\n" + CoolDownTime.ToString();
            CoolDownTime--;
        }

        if (CoolDownTime < 0)
        {
            SandClocks.SetActive(false);
            CooldownText.text = " ";
            BonusIsActive = 0;
            ButtonAnimator.SetBool("IsUnactive", false);
        }
    }
}
