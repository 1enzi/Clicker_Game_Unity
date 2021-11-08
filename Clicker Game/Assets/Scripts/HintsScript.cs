using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsScript : MonoBehaviour
{

    public Animator Hint1;
    public GameObject ObgectHint1;
    public GameObject ObgectHint2;
    public Animator Hint2;
    public GameObject ObgectHint3;
    public Animator Hint3;

    void Start()
    {
        Hint1 = ObgectHint1.GetComponent<Animator>();
        Hint2 = ObgectHint2.GetComponent<Animator>();
        Hint3 = ObgectHint3.GetComponent<Animator>();
        StartCoroutine(Hints());
    }

    private IEnumerator Hints()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            int rand = Random.Range(1, 4);

            switch (rand)
            {
                case 1:
                    Hint1.SetTrigger("IsActive");
                    break;
                case 2:
                    Hint2.SetTrigger("IsActive");
                    break;
                case 3:
                    Hint3.SetTrigger("IsActive");
                    break;
            }
        }
    }
}
