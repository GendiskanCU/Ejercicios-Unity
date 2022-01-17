using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHitScore : MonoBehaviour
{

    private int pointsToShow;
    [SerializeField] TextMeshProUGUI hitText;

    public void ShowHScore(int points)
    {
        pointsToShow = points;
        if (pointsToShow >= 0)
        {
            hitText.text = "+" + pointsToShow;
            hitText.color = Color.green;
        }
        else
        {
            hitText.text = pointsToShow.ToString();
            hitText.color = Color.red;
        }
        hitText.gameObject.SetActive(true);
        StartCoroutine(HideHScore());
    }

    IEnumerator HideHScore()
    {
        yield return new WaitForSeconds(0.25f);
        hitText.gameObject.SetActive(false);
    }
}
