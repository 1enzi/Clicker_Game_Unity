using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
{

    private bool move;
    private Vector2 RandomVector;

    private void Update()
    {
        if (!move) return;
        transform.Translate(RandomVector * Time.deltaTime);
    }

    public void StartMotion(int PointsNum)
    {
        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "+" + PointsNum;
        RandomVector = new Vector2(Random.Range(-3, 3), 3);
        move = true;
        GetComponent<Animation>().Play();
    }

    public void StopMotion()
    {
        move = false;
    }    
}
