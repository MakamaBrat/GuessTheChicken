using UnityEngine;
using TMPro;

public class EggsNum : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score;

    void OnEnable()
    {
        score = 0;
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = "Score:"+score.ToString();
    }

    // ➕ Вызывать при подборе снежинки
    public void Add(int amount = 1)
    {
        score += amount;
        UpdateText();
    }

    // (опционально) получить текущее количество
    public int GetSnowflakes()
    {
        return score;
    }
}
