using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects (3 variants)")]
    public GameObject[] objects;

    [Header("UI / Animation")]
    public MenuTravel menuTravel;
    public Animator animator;
    public GameObject prefab;
    [Header("Timings")]
    public float startDelay = 1.5f;
    public float winAnimationTime = 1.2f; // длительность win-анимации

    private int activeIndex;
    private bool canChoose;
    public EggsNum eggsNum;
    public AudioSource au;
    public AudioSource au2;
    private void OnEnable()
    {
        RestartGame();
    }

    // --------------------
    // GAME FLOW
    // --------------------

    void RestartGame()
    {
        StopAllCoroutines();

        canChoose = false;

        foreach (var obj in objects)
            obj.SetActive(false);

        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        yield return new WaitForSeconds(startDelay);

        activeIndex = Random.Range(0, objects.Length);
        objects[activeIndex].SetActive(true);

        canChoose = true;
    }

    // --------------------
    // BUTTON INPUT
    // --------------------

    public void ChooseButton0() => CheckResult(0);
    public void ChooseButton1() => CheckResult(1);
    public void ChooseButton2() => CheckResult(2);

    void CheckResult(int chosenIndex)
    {
        if (!canChoose)
            return;

        canChoose = false;

        if (chosenIndex == activeIndex)
        {
            Win();
            var k = Instantiate(prefab, objects[chosenIndex].transform.position, Quaternion.identity, transform);
        }

        else
            Lose();
    }

    // --------------------
    // RESULTS
    // --------------------

    void Win()
    {
        StartCoroutine(WinRoutine());
        au.Play();
        eggsNum.Add();
      
    }

    IEnumerator WinRoutine()
    {
        // проигрываем анимацию сообщения
        if (animator != null)
            animator.Play("Play");

        // ждём пока анимация закончится
        yield return new WaitForSeconds(winAnimationTime);

        // рестарт раунда (бесконечная игра)
        RestartGame();
    }

    void Lose()
    {
        au2.Play();
        menuTravel.makeMenu(3);
    }
}
