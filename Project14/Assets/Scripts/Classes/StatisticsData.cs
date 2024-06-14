using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamesData
{
    public static void SaveStatistics()     // сохранение статистики
    {
        PlayerPrefs.SetInt("EasyPanelsBest", EasyComplexityGamesData.PanelsBest);
        PlayerPrefs.SetInt("EasyWordsBest", EasyComplexityGamesData.WordsBest);
        PlayerPrefs.SetInt("EasyCardsBest", EasyComplexityGamesData.CardsBest);
        PlayerPrefs.SetInt("EasyTreasuresBest", EasyComplexityGamesData.TreasuresBest);

        PlayerPrefs.SetInt("EasyPanelsLast", EasyComplexityGamesData.PanelsLast);
        PlayerPrefs.SetInt("EasyWordsLast", EasyComplexityGamesData.WordsLast);
        PlayerPrefs.SetInt("EasyCardsLast", EasyComplexityGamesData.CardsLast);
        PlayerPrefs.SetInt("EasyTreasuresLast", EasyComplexityGamesData.TreasuresLast);

        PlayerPrefs.SetInt("MediumPanelsBest", MediumComplexityGamesData.PanelsBest);
        PlayerPrefs.SetInt("MediumWordsBest", MediumComplexityGamesData.WordsBest);
        PlayerPrefs.SetInt("MediumCardsBest", MediumComplexityGamesData.CardsBest);
        PlayerPrefs.SetInt("MediumTreasuresBest", MediumComplexityGamesData.TreasuresBest);

        PlayerPrefs.SetInt("MediumPanelsLast", MediumComplexityGamesData.PanelsLast);
        PlayerPrefs.SetInt("MediumWordsLast", MediumComplexityGamesData.WordsLast);
        PlayerPrefs.SetInt("MediumCardsLast", MediumComplexityGamesData.CardsLast);
        PlayerPrefs.SetInt("MediumTreasuresLast", MediumComplexityGamesData.TreasuresLast);

        PlayerPrefs.SetInt("HardPanelsBest", HardComplexityGamesData.PanelsBest);
        PlayerPrefs.SetInt("HardWordsBest", HardComplexityGamesData.WordsBest);
        PlayerPrefs.SetInt("HardCardsBest", HardComplexityGamesData.CardsBest);
        PlayerPrefs.SetInt("HardTreasuresBest", HardComplexityGamesData.TreasuresBest);

        PlayerPrefs.SetInt("HardPanelsLast", HardComplexityGamesData.PanelsLast);
        PlayerPrefs.SetInt("HardWordsLast", HardComplexityGamesData.WordsLast);
        PlayerPrefs.SetInt("HardCardsLast", HardComplexityGamesData.CardsLast);
        PlayerPrefs.SetInt("HardTreasuresLast", HardComplexityGamesData.TreasuresLast);
    }

    public static class EasyComplexityGamesData
    {
        public static int PanelsBest { get; set; }
        public static int WordsBest { get; set; }
        public static int CardsBest { get; set; }
        public static int TreasuresBest { get; set; }
        public static int PanelsLast { get; set; }
        public static int WordsLast { get; set; }
        public static int CardsLast { get; set; }
        public static int TreasuresLast { get; set; }
    }

    public static class MediumComplexityGamesData
    {
        public static int PanelsBest { get; set; }
        public static int WordsBest { get; set; }
        public static int CardsBest { get; set; }
        public static int TreasuresBest { get; set; }
        public static int PanelsLast { get; set; }
        public static int WordsLast { get; set; }
        public static int CardsLast { get; set; }
        public static int TreasuresLast { get; set; }
    }

    public static class HardComplexityGamesData
    {
        public static int PanelsBest { get; set; }
        public static int WordsBest { get; set; }
        public static int CardsBest { get; set; }
        public static int TreasuresBest { get; set; }
        public static int PanelsLast { get; set; }
        public static int WordsLast { get; set; }
        public static int CardsLast { get; set; }
        public static int TreasuresLast { get; set; }
    }
}
