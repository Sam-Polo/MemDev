using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject SettingsPanel;
    public GameObject StatisticsPanel;
    public GameObject GamesPanel;
    public Scrollbar Complexity;
    public GameObject BackButton;
    public GameObject PanelHelperGame1;
    public GameObject PanelHelperGame2;
    public GameObject PanelHelperGame3;
    public GameObject PanelHelperGame4;
    public GameObject EasyScoresObject;
    public GameObject MediumScoresObject;
    public GameObject HardScoresObject;
    public GameObject Game1Button; // плитки
    public GameObject Game2Button; // слова 
    public GameObject Game3Button; // карточки
    public GameObject Game4Button; // лабиринт
    public GameObject ComplSelectorEasy;
    public GameObject ComplSelectorMedium;
    public GameObject ComplSelectorHard;

    private Complexity complexity;
    void Start()
    {
        GetStatisticsToClass();
        if (!PlayerPrefs.HasKey("Complexity"))
        {
            PlayerPrefs.SetFloat("Complexity", 0);
        }
        Complexity.value = PlayerPrefs.GetFloat("Complexity");
        SetComplexity();
        GetSavedInforamtion();
    }

    public void ClickGameMenuButton()
    {
        GamesPanel.SetActive(true);
    }

    public void ClickSettingsButton()
    {
        SettingsPanel.SetActive(true);
    }

    public void ClickStatisticsPanelButton()
    {
        StatisticsPanel.SetActive(true);
    }

    public void ClickBackButton()
    {
        GamesPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        StatisticsPanel.SetActive(false);
    }

    public void ClickExitMenuButton()
    {
        Application.Quit();
    }

    public void OnClickQuestionGame1Button()
    {
        PanelHelperGame1.SetActive(true);
        BackButton.SetActive(false);
        MakeGameButtonsMoreTransparent();
    }

    public void OnClickQuestionGame2Button()
    {
        PanelHelperGame2.SetActive(true);
        BackButton.SetActive(false);
        MakeGameButtonsMoreTransparent();
    }

    public void OnClickQuestionGame3Button()
    {
        PanelHelperGame3.SetActive(true);
        BackButton.SetActive(false);
        MakeGameButtonsMoreTransparent();
    }

    public void OnClickQuestionGame4Button()
    {
        PanelHelperGame4.SetActive(true);
        BackButton.SetActive(false);
        MakeGameButtonsMoreTransparent();
    }

    public void OnClickCloseHelper1Button()
    {
        PanelHelperGame1.SetActive(false);
        BackButton.SetActive(true);
        MakeGameButtonsNormal();
    }

    public void OnClickCloseHelper2Button()
    {
        PanelHelperGame2.SetActive(false);
        BackButton.SetActive(true);
        MakeGameButtonsNormal();
    }

    public void OnClickCloseHelper3Button()
    {
        PanelHelperGame3.SetActive(false);
        BackButton.SetActive(true);
        MakeGameButtonsNormal();
    }

    public void OnClickCloseHelper4Button()
    {
        PanelHelperGame4.SetActive(false);
        BackButton.SetActive(true);
        MakeGameButtonsNormal();
    }

    public void OnClickComplSelectorEasy()
    {
        EasyScoresObject.SetActive(false);
        ComplSelectorEasy.SetActive(false);
        MediumScoresObject.SetActive(true);
        ComplSelectorMedium.SetActive(true);
    }

    public void OnClickComplSelectorMedium()
    {
        MediumScoresObject.SetActive(false);
        ComplSelectorMedium.SetActive(false);
        HardScoresObject.SetActive(true);
        ComplSelectorHard.SetActive(true);
    }

    public void OnClickComplSelectorHard()
    {
        HardScoresObject.SetActive(false);
        ComplSelectorHard.SetActive(false);
        EasyScoresObject.SetActive(true);
        ComplSelectorEasy.SetActive(true);
    }

    public void ClickOnGame_1Button()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickOnGame_2Button()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickOnGame_3Button()
    {
        SceneManager.LoadScene(3);
    }

    public void ClickOnGame_4Button()
    {
        SceneManager.LoadScene(4);
    }

    private void MakeGameButtonsMoreTransparent()   // прозрачные
    {
        Game1Button.GetComponent<Graphic>().color = new Color32(255, 255, 220, 50);
        Game2Button.GetComponent<Graphic>().color = new Color32(220, 220, 255, 50);
        Game3Button.GetComponent<Graphic>().color = new Color32(220, 255, 220, 50);
        Game4Button.GetComponent<Graphic>().color = new Color32(255, 220, 237, 50);
        Game1Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 50);
        Game3Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 50);
        Game4Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 50);
    }

    private void MakeGameButtonsNormal()    // обычные
    {
        Game1Button.GetComponent<Graphic>().color = new Color32(255, 255, 220, 255);
        Game2Button.GetComponent<Graphic>().color = new Color32(220, 220, 255, 255);
        Game3Button.GetComponent<Graphic>().color = new Color32(220, 255, 220, 255);
        Game4Button.GetComponent<Graphic>().color = new Color32(255, 220, 237, 255);
        Game1Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 255);
        Game3Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 255);
        Game4Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 255);
    }

    private void SetComplexity()    // выбор сложности
    {
        complexity = ComplexityClass.GetComplexity(PlayerPrefs.GetFloat("Complexity", 0));

        if (complexity == global::Complexity.Easy)
        {
            EasyScoresObject.SetActive(true);
            ComplSelectorEasy.SetActive(true);
        }
        else if (complexity == global::Complexity.Medium)
        {
            MediumScoresObject.SetActive(true);
            ComplSelectorMedium.SetActive(true);
        }
        else
        {
            HardScoresObject.SetActive(true);
            ComplSelectorHard.SetActive(true);
        }
    }

    public void SaveComplexity()    // сохранение текущего уровня сложности
    {
        PlayerPrefs.SetFloat("Complexity", Complexity.value);
    }

    public static void GetStatisticsToClass()   // загрузка результатов
    {
        //  Easy
        GamesData.EasyComplexityGamesData.PanelsBest = PlayerPrefs.GetInt("EasyPanelsBest", 0);
        GamesData.EasyComplexityGamesData.WordsBest = PlayerPrefs.GetInt("EasyWordsBest", 0);
        GamesData.EasyComplexityGamesData.CardsBest = PlayerPrefs.GetInt("EasyCardsBest", 0);
        GamesData.EasyComplexityGamesData.TreasuresBest = PlayerPrefs.GetInt("EasyTreasuresBest", 0);

        GamesData.EasyComplexityGamesData.PanelsLast = PlayerPrefs.GetInt("EasyPanelsLast", 0);
        GamesData.EasyComplexityGamesData.WordsLast = PlayerPrefs.GetInt("EasyWordsLast", 0);
        GamesData.EasyComplexityGamesData.CardsLast = PlayerPrefs.GetInt("EasyCardsLast", 0);
        GamesData.EasyComplexityGamesData.TreasuresLast = PlayerPrefs.GetInt("EasyTreasuresLast", 0);

        //  Medium
        GamesData.MediumComplexityGamesData.PanelsBest = PlayerPrefs.GetInt("MediumPanelsBest", 0);
        GamesData.MediumComplexityGamesData.WordsBest = PlayerPrefs.GetInt("MediumWordsBest", 0);
        GamesData.MediumComplexityGamesData.CardsBest = PlayerPrefs.GetInt("MediumCardsBest", 0);
        GamesData.MediumComplexityGamesData.TreasuresBest = PlayerPrefs.GetInt("MediumTreasuresBest", 0);

        GamesData.MediumComplexityGamesData.PanelsLast = PlayerPrefs.GetInt("MediumPanelsLast", 0);
        GamesData.MediumComplexityGamesData.WordsLast = PlayerPrefs.GetInt("MediumWordsLast", 0);
        GamesData.MediumComplexityGamesData.CardsLast = PlayerPrefs.GetInt("MediumCardsLast", 0);
        GamesData.MediumComplexityGamesData.TreasuresLast = PlayerPrefs.GetInt("MediumTreasuresLast", 0);

        //  Hard
        GamesData.HardComplexityGamesData.PanelsBest = PlayerPrefs.GetInt("HardPanelsBest", 0);
        GamesData.HardComplexityGamesData.WordsBest = PlayerPrefs.GetInt("HardWordsBest", 0);
        GamesData.HardComplexityGamesData.CardsBest = PlayerPrefs.GetInt("HardCardsBest", 0);
        GamesData.HardComplexityGamesData.TreasuresBest = PlayerPrefs.GetInt("HardTreasuresBest", 0);

        GamesData.HardComplexityGamesData.PanelsLast = PlayerPrefs.GetInt("HardPanelsLast", 0);
        GamesData.HardComplexityGamesData.WordsLast = PlayerPrefs.GetInt("HardWordsLast", 0);
        GamesData.HardComplexityGamesData.CardsLast = PlayerPrefs.GetInt("HardCardsLast", 0);
        GamesData.HardComplexityGamesData.TreasuresLast = PlayerPrefs.GetInt("HardTreasuresLast", 0);
    }

    public void GetSavedInforamtion()   // отображение статистики
    {
        //  Easy
        EasyScoresObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyPanelsBest", 0).ToString();
        EasyScoresObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyPanelsLast", 0).ToString();
        EasyScoresObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyWordsBest", 0).ToString();
        EasyScoresObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyWordsLast", 0).ToString();
        EasyScoresObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyCardsBest", 0).ToString();
        EasyScoresObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyCardsLast", 0).ToString();
        EasyScoresObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyTreasuresBest", 0).ToString();
        EasyScoresObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("EasyTreasuresLast", 0).ToString();

        //  Medium
        MediumScoresObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumPanelsBest", 0).ToString();
        MediumScoresObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumPanelsLast", 0).ToString();
        MediumScoresObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumWordsBest", 0).ToString();
        MediumScoresObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumWordsLast", 0).ToString();
        MediumScoresObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumCardsBest", 0).ToString();
        MediumScoresObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumCardsLast", 0).ToString();
        MediumScoresObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumTreasuresBest", 0).ToString();
        MediumScoresObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("MediumTreasuresLast", 0).ToString();

        //  Hard
        HardScoresObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardPanelsBest", 0).ToString();
        HardScoresObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardPanelsLast", 0).ToString();
        HardScoresObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardWordsBest", 0).ToString();
        HardScoresObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardWordsLast", 0).ToString();
        HardScoresObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardCardsBest", 0).ToString();
        HardScoresObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardCardsLast", 0).ToString();
        HardScoresObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardTreasuresBest", 0).ToString();
        HardScoresObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("HardTreasuresLast", 0).ToString();
    }
}
