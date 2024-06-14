using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.UI;

public class Game_4Script : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject ExitButton;
    public GameObject ResetButton;
    public GameObject LeftButton;
    public GameObject UpButton;
    public GameObject RightButton;
    public GameObject DownButton;

    public GameObject WinSprite;
    public GameObject PlayerImage;
    public GameObject ChestImage;
    public GameObject GroundImage;
    public GameObject MineImage;
    public GameObject BoomImage;
    public GameObject PlayerOnChestImage;

    Game4 myGame;
    public GameObject MapParentObject;
    public GameObject BigMapParentObject;
    private GameObject currentMapParentObject;
    private SpriteRenderer[,] MapPlaces;
    private static int MapSize;
    private static int BombCount;
    private static readonly float ShowingTime = 2;
    private static Complexity complexityLevel;
    private double multiplier;
    private int winCounter = 0;

    void Start()
    {
        SetComplexity();
        MapPlaces = new SpriteRenderer[MapSize, MapSize];

        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                MapPlaces[i, j] = currentMapParentObject.transform.GetChild(i * MapSize + j).GetComponent<SpriteRenderer>();
            }
        }

        DisableGamepad();
    }

    public IEnumerator StartGame()
    {
        myGame = new Game4(MapSize, BombCount);
        myGame.MovedToBomb += () => EndGame();
        myGame.MovedToChest += () => StartCoroutine(WinGame());
        myGame.MovedToGround += () => PaintMap(false);

        DisableGamepad();
        PaintMap(true);

        yield return new WaitForSeconds(ShowingTime);

        PaintMap(false);
        EnableGamepad();
    }

    private IEnumerator WinGame()
    {
        SaveResults();
        winCounter++;
        DisableGamepad();
        PaintMap(true);
        WinSprite.SetActive(true);

        yield return new WaitForSeconds(1);

        WinSprite.SetActive(false);
        StartCoroutine(StartGame());
    }

    private void PaintMap(bool withBombs)
    {
        for (int i = 0; i < myGame.Size; i++)
        {
            for (int j = 0; j < myGame.Size; j++)
            {
                switch (myGame.Map[i, j])
                {
                    case CellType.Bomb:
                        if (withBombs)                       
                            MapPlaces[i, j].sprite = MineImage.GetComponent<SpriteRenderer>().sprite;                       
                        else                        
                            MapPlaces[i, j].sprite = GroundImage.GetComponent<SpriteRenderer>().sprite;                       
                        break;
                    case CellType.Place:
                        MapPlaces[i, j].sprite = GroundImage.GetComponent<SpriteRenderer>().sprite;
                        break;
                    case CellType.Chest:
                        MapPlaces[i, j].sprite = ChestImage.GetComponent<SpriteRenderer>().sprite;
                        break;
                    case CellType.Player:
                        MapPlaces[i, j].sprite = PlayerImage.GetComponent<SpriteRenderer>().sprite;
                        break;
                    case CellType.DeathsPlace:
                        MapPlaces[i, j].sprite = BoomImage.GetComponent<SpriteRenderer>().sprite;
                        break;
                    case CellType.WinPlace:
                        MapPlaces[i, j].sprite = PlayerOnChestImage.GetComponent<SpriteRenderer>().sprite;
                        break;
                }
            }
        }  
    }

    private void SaveResults()
    {
        switch (complexityLevel)
        {
            case Complexity.Easy:
                GamesData.EasyComplexityGamesData.TreasuresLast = (int)(Math.Pow(multiplier, winCounter) * 10);
                if (GamesData.EasyComplexityGamesData.TreasuresBest < (int)(Math.Pow(multiplier, winCounter) * 10))
                    GamesData.EasyComplexityGamesData.TreasuresBest = (int)(Math.Pow(multiplier, winCounter) * 10);
                break;
            case Complexity.Medium:
                GamesData.MediumComplexityGamesData.TreasuresLast = (int)(Math.Pow(multiplier, winCounter) * 10);
                if (GamesData.MediumComplexityGamesData.TreasuresBest < (int)(Math.Pow(multiplier, winCounter) * 10))
                    GamesData.MediumComplexityGamesData.TreasuresBest = (int)(Math.Pow(multiplier, winCounter) * 10);
                break;
            case Complexity.Hard:
                GamesData.HardComplexityGamesData.TreasuresLast = (int)(Math.Pow(multiplier, winCounter) * 10);
                if (GamesData.HardComplexityGamesData.TreasuresBest < (int)(Math.Pow(multiplier, winCounter) * 10))
                    GamesData.HardComplexityGamesData.TreasuresBest = (int)(Math.Pow(multiplier, winCounter) * 10);
                break;
        }
        GamesData.SaveStatistics();
    }

    private void EndGame()
    {
        winCounter = 0;
        PaintMap(true);
        DisableGamepad();
        ResetButton.SetActive(true);
    }

    private void DisableGamepad()
    {
        LeftButton.GetComponent<Button>().interactable = false;
        RightButton.GetComponent<Button>().interactable = false;
        UpButton.GetComponent<Button>().interactable = false;
        DownButton.GetComponent<Button>().interactable = false;
    }

    private void EnableGamepad()
    {
        LeftButton.GetComponent<Button>().interactable = true;
        RightButton.GetComponent<Button>().interactable = true;
        UpButton.GetComponent<Button>().interactable = true;
        DownButton.GetComponent<Button>().interactable = true;
    }

    private void SetComplexity()
    {
        complexityLevel = ComplexityClass.GetComplexity(PlayerPrefs.GetFloat("Complexity"));
        if (complexityLevel == Complexity.Easy)
        {
            MapSize = 4;
            BombCount = 3;
            currentMapParentObject = MapParentObject;
            multiplier = 1.1;
        }
        else if (complexityLevel == Complexity.Medium)
        {
            MapSize = 5;
            BombCount = 6;
            currentMapParentObject = BigMapParentObject;
            multiplier = 1.15;
        }
        else
        {
            MapSize = 5;
            BombCount = 10;
            currentMapParentObject = BigMapParentObject;
            multiplier = 1.2;
        }
        currentMapParentObject.SetActive(true);
    }

    public void OnClickLeftButton()
    {
        myGame.PlayerMove(Direction.Left);
    }

    public void OnClickUpButton()
    {
        myGame.PlayerMove(Direction.Up);
    }

    public void OnClickRightButton()
    {
        myGame.PlayerMove(Direction.Right);
    }

    public void OnClickDownButton()
    {
        myGame.PlayerMove(Direction.Down);
    }

    public void OnClickStartButton()
    {
        StartButton.SetActive(false);
        StartCoroutine(StartGame());
    }

    public void OnClickResetButton()
    {
        StartCoroutine(StartGame());
        ResetButton.SetActive(false);
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene(0);
    }
}