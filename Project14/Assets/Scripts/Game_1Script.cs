using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Game_1Script : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject BlocksParent;
    public GameObject BlocksParentBigField;
    public GameObject Ticket;

    private static readonly int startCountBlinkingPanel = 2;
    private static readonly float timeReactionOnClick = 0.1f;

    Sequencer sequencer;
    private Color32 blinkingColor = new Color32(255, 255, 0, 255);
    private GameObject gameBlocksParent;
    private List<Button> gameBlocks = new List<Button>();
    private List<int> rightSequence = new List<int>();
    private int blinkingPanelCount = startCountBlinkingPanel;
    private int rightPressCounter = 0;
    private static Complexity complexityLevel;

    void Start()
    {
        SetComplexity();

        int blocksCount = gameBlocksParent.transform.childCount;
        for (int i = 0; i < blocksCount; i++)
        {
            var currentButton = gameBlocksParent.transform.GetChild(i).GetComponent<Button>();
            currentButton.onClick.AddListener(() => OnClickGamePanel(currentButton));
            gameBlocks.Add(currentButton);
        }
        sequencer = new Sequencer(blocksCount);
    }

    public void OnClickGamePanel(Button pressedButton)
    {
        int buttonsNumber = int.Parse(pressedButton.name.Split(new char[] { '(', ')' },     // Получение номера нажатой кнопки.
            StringSplitOptions.RemoveEmptyEntries)[1].ToString());

        if (buttonsNumber == rightSequence[rightPressCounter])  // Правильная ли кнопка нажата.
        {
            StartCoroutine(VisualReactOnClickBlock(pressedButton, true));
            rightPressCounter++;

            if (rightPressCounter == rightSequence.Count)
            {
                StartCoroutine(ShowTicket());
                SaveResults(blinkingPanelCount);
                GoNextIteration(rightPressCounter == gameBlocks.Count); // если была ошибка, то новая итерация
            }
        }
        else
        {   // Нажата ли кнопка?
            StartCoroutine(VisualReactOnClickBlock(pressedButton, false));
            GoNextIteration(true);
        }
    }

    private void GoNextIteration(bool isFirstIteration)
    {
        if (complexityLevel == Complexity.Hard || isFirstIteration)
            sequencer = new Sequencer(gameBlocks.Count);

        rightPressCounter = 0;
        blinkingPanelCount = isFirstIteration? startCountBlinkingPanel : blinkingPanelCount + 1;
        StartCoroutine(StartBlinking(0.75f, blinkingPanelCount));
    }

    private IEnumerator StartBlinking(float interval, int seqSize)
    {
        DisableBlocks();

        var seq = sequencer.GetSequence(seqSize); // получаем последовательность индексов для мигания
        rightSequence = seq.Select(number => number + 1).ToList();  // записываем последовательность в поле, для проверки далее. Индекс смещён, т.к. в Unity нумерация в названии блоков начинается с 1.

        yield return new WaitForSeconds(2); // задержка перед началом игры.

        for (int i = 0; i < seqSize; i++)   // seqSize кнопок, загораются с промежутком в (2 * interval) секунд.
        {
            ColorBlock old = gameBlocks[seq[i]].colors;
            ColorBlock ne = gameBlocks[seq[i]].colors;
            Color32 defaultColor = gameBlocks[seq[i]].GetComponent<Graphic>().color;

            ne.disabledColor = new Color32(255, 255, 255, 255);
            gameBlocks[seq[i]].colors = ne;
            gameBlocks[seq[i]].GetComponent<Graphic>().color = blinkingColor;

            yield return new WaitForSeconds(interval);

            gameBlocks[seq[i]].colors = old;
            gameBlocks[seq[i]].GetComponent<Graphic>().color = defaultColor;
            yield return new WaitForSeconds(interval);
        }

        EnableBlocks();
    }

    private IEnumerator VisualReactOnClickBlock(Button button, bool isRight)
    {
        Color defaultColor = button.GetComponent<Graphic>().color;
        button.GetComponent<Graphic>().color = isRight ? new Color32(25, 200, 25, 255) : new Color32(127, 0, 51, 255);
        yield return new WaitForSeconds(timeReactionOnClick);
        button.GetComponent<Graphic>().color = defaultColor;
        yield return new WaitForSeconds(timeReactionOnClick);
    }

    private IEnumerator ShowTicket()
    {
        Ticket.SetActive(true);
        yield return new WaitForSeconds(1);
        Ticket.SetActive(false);
    }

    public void OnClickStartButton()
    {
        StartButton.SetActive(false);
        StartCoroutine(StartBlinking(0.75f, blinkingPanelCount));
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void DisableBlocks()
    {
        foreach (var block in gameBlocks)
        {
            block.interactable = false;
        }   
    }

    public void EnableBlocks()
    {
        foreach (var block in gameBlocks)
        {
            block.interactable = true;
        }
    }

    public void SaveResults(int score)
    {
        switch (complexityLevel)
        {
            case Complexity.Easy:
                GamesData.EasyComplexityGamesData.PanelsLast = score * 10;
                if (GamesData.EasyComplexityGamesData.PanelsBest < score * 10)
                    GamesData.EasyComplexityGamesData.PanelsBest = score * 10;
                break;
            case Complexity.Medium:
                GamesData.MediumComplexityGamesData.PanelsLast = score * 13;
                if (GamesData.MediumComplexityGamesData.PanelsBest < score * 13)
                    GamesData.MediumComplexityGamesData.PanelsBest = score * 13;
                break;
            case Complexity.Hard:
                GamesData.HardComplexityGamesData.PanelsLast = score * 20;
                if (GamesData.HardComplexityGamesData.PanelsBest < score * 20)
                    GamesData.HardComplexityGamesData.PanelsBest = score * 20;
                break;
        }
        GamesData.SaveStatistics();
    }

    private void SetComplexity()
    {
        complexityLevel = ComplexityClass.GetComplexity(PlayerPrefs.GetFloat("Complexity", 0));
        if (complexityLevel == Complexity.Easy || complexityLevel == Complexity.Hard)
        {
            gameBlocksParent = BlocksParent;
        }
        else
        {
            gameBlocksParent = BlocksParentBigField;
        }
        gameBlocksParent.SetActive(true);
    }
}