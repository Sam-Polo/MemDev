using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_3Script : MonoBehaviour
{
    public GameObject Ticket;
    public GameObject StartButton;
    public GameObject DefaultCardsImage;
    public GameObject EasyCardsParentObject;
    public GameObject MediumCardsParentObject;
    public GameObject HardCardsParentObject;
    public GameObject CardsImagesParentObject;

    private Sprite defaultSprite;
    private Button pressedButton;
    private GameObject cardsParrent;
    private List<int> randomSeq = new List<int>();
    private List<Button> cards = new List<Button>();
    private List<Sprite> cardsSprites = new List<Sprite>();

    private bool anyoneTurned;
    private int notTurnedCardsCounter;
    private static float showingTime;
    private static readonly float wrongChoiceShowingTime = 1;
    private static readonly float ticketShowingTime = 1;
    private static Complexity complexityLevel;
    private int startScore;

    void Start()
    {
        SetComplexity();
        defaultSprite = DefaultCardsImage.GetComponent<SpriteRenderer>().sprite;
        var buttonsCount = cardsParrent.transform.childCount;
        notTurnedCardsCounter = buttonsCount;

        for (int i = 0; i < buttonsCount; i++)
        {
            var currentButton = cardsParrent.transform.GetChild(i).GetComponent<Button>();
            currentButton.onClick.AddListener(() => OnClickCard(currentButton));
            cards.Add(currentButton);
        }

        DisableCards();

        for (int i = 0; i < buttonsCount/2; i++)
        {
            cardsSprites.Add(CardsImagesParentObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite);
            cardsSprites.Add(CardsImagesParentObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite);
        }
    }

    public IEnumerator ShowAllCards()
    {
        DisableCards();
        Sequencer sequncer = new Sequencer(cards.Count);
        randomSeq = sequncer.GetSequence();
        yield return new WaitForSeconds(0.5f + ticketShowingTime);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].image.sprite = cardsSprites[randomSeq[i]];
        }

        yield return new WaitForSeconds(showingTime);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].image.sprite = defaultSprite;
        }

        EnableCards();
    }

    public IEnumerator OnSecondCard(Button button)
    {
        int currentCardImageIndex = randomSeq[cards.FindIndex(but => but.name == button.name)];
        var pressedCardImageIndex = randomSeq[cards.FindIndex(but => but.name == pressedButton.name)];

        SaveResults(pressedCardImageIndex == currentCardImageIndex + 1 - (currentCardImageIndex % 2) * 2);
        if (pressedCardImageIndex != currentCardImageIndex + 1 - (currentCardImageIndex % 2) * 2)
        {   // Определяем массив изображений для карт, используемых в переменной cardsImages.
            notTurnedCardsCounter += 2;
            DisableCards();
            yield return new WaitForSeconds(wrongChoiceShowingTime);
            pressedButton.image.sprite = defaultSprite;
            button.image.sprite = defaultSprite;
            EnableCards();
        }
        else
        {
            pressedButton.interactable = false;
            button.interactable = false;
        }

        pressedButton = null;
        anyoneTurned = false;
    }

    public IEnumerator ShowTicket()
    {
        Ticket.SetActive(true);
        yield return new WaitForSeconds(1);
        Ticket.SetActive(false);
    }

    public void GameIteration(Button button)
    {
        notTurnedCardsCounter--;
        button.interactable = false;
        button.image.sprite = cardsSprites[randomSeq[cards.FindIndex(but => but.name == button.name)]];

        if (anyoneTurned)
        {
            StartCoroutine(OnSecondCard(button));
        }
        else
        {
            anyoneTurned = true;
            pressedButton = button;
        }

        if (notTurnedCardsCounter == 0)
        {
            startScore = (cards.Count / 2) * 10;
            StartCoroutine(ShowTicket());
            ResetCards();
            StartCoroutine(ShowAllCards());
        }
    }

    private void SaveResults(bool isRightMove)
    {
        int score = 0;
        switch (complexityLevel)
        {
            case Complexity.Easy:
                score = isRightMove ? 20 : -10;
                if (GamesData.EasyComplexityGamesData.CardsLast == 0 && !isRightMove) break;
                GamesData.EasyComplexityGamesData.CardsLast += score;
                if (GamesData.EasyComplexityGamesData.CardsBest < GamesData.EasyComplexityGamesData.CardsLast)
                    GamesData.EasyComplexityGamesData.CardsBest = GamesData.EasyComplexityGamesData.CardsLast;
                break;
            case Complexity.Medium:
                score = isRightMove ? 25 : -10;
                if (GamesData.MediumComplexityGamesData.CardsLast == 0 && !isRightMove) break;
                GamesData.MediumComplexityGamesData.CardsLast += score;
                if (GamesData.MediumComplexityGamesData.CardsBest < GamesData.MediumComplexityGamesData.CardsLast)
                    GamesData.MediumComplexityGamesData.CardsBest = GamesData.MediumComplexityGamesData.CardsLast;
                break;
            case Complexity.Hard:
                score = isRightMove ? 30 : -10;
                if (GamesData.HardComplexityGamesData.CardsLast == 0 && !isRightMove) break;
                GamesData.HardComplexityGamesData.CardsLast += score;
                if (GamesData.HardComplexityGamesData.CardsBest < GamesData.HardComplexityGamesData.CardsLast)
                    GamesData.HardComplexityGamesData.CardsBest = GamesData.HardComplexityGamesData.CardsLast;
                break;
        }
        GamesData.SaveStatistics();
    }

    private void SetComplexity()
    {
        complexityLevel = ComplexityClass.GetComplexity(PlayerPrefs.GetFloat("Complexity", 0));
        if (complexityLevel == Complexity.Easy)
        {
            cardsParrent = EasyCardsParentObject;
            showingTime = 4;
            startScore = (cardsParrent.transform.childCount / 2) * 10;
            GamesData.EasyComplexityGamesData.CardsLast = startScore;
            if (GamesData.EasyComplexityGamesData.CardsBest < startScore)
                GamesData.EasyComplexityGamesData.CardsBest = startScore;
        }
        else if (complexityLevel == Complexity.Medium)
        {
            cardsParrent = MediumCardsParentObject;
            showingTime = 6;
            startScore = (cardsParrent.transform.childCount / 2) * 10;
            GamesData.MediumComplexityGamesData.CardsLast = startScore;
            if (GamesData.MediumComplexityGamesData.CardsBest < startScore)
                GamesData.MediumComplexityGamesData.CardsBest = startScore;
        }
        else
        {
            cardsParrent = HardCardsParentObject;
            showingTime = 8;
            startScore = (cardsParrent.transform.childCount / 2) * 10;
            GamesData.HardComplexityGamesData.CardsLast = startScore;
            if (GamesData.HardComplexityGamesData.CardsBest < startScore)
                GamesData.HardComplexityGamesData.CardsBest = startScore;
        }
        cardsParrent.SetActive(true);
    }

    public void OnClickCard(Button button)
    {
        GameIteration(button);
    }

    public void ResetCards()
    {
        notTurnedCardsCounter = cards.Count;
        foreach (var card in cards)
        {
            card.image.sprite = defaultSprite;
        }
    }

    public void EnableCards()
    {
        foreach (var card in cards)
        {
            card.interactable = true;
        }
    }

    public void DisableCards()
    {
        foreach (var card in cards)
        {
            card.interactable = false;
        }
    }

    public void OnClickStartButton()
    {
        StartButton.SetActive(false);
        StartCoroutine(ShowAllCards());
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
