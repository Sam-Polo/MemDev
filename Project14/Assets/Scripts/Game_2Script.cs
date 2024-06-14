using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_2Script : MonoBehaviour
{
    public Text Timer;
    public GameObject StartButton;
    public GameObject EasyWordsBlocksParentPanel;
    public GameObject MediumWordsBlocksParentPanel;
    public GameObject HardWordsBlocksParentPanel;
    public GameObject SkipButton;

    private static Complexity complexityLevel;
    private static float timeToRemember;
    private static float timeToWrite;
    private static readonly string textWords = @"информация
управление
количество
литература
автомобиль
содержание
фотография
настроение
заключение
достижение
инструмент
творчество
воспитание
расстояние
библиотека
результат
президент
население
искусство
спектакль
поддержка
сожаление
журналист
стоимость
поведение
понимание
телевизор
опасность
компьютер
транспорт
мгновение
мальчишка
удивление
проблема
компания
внимание
разговор
движение
документ
директор
создание
значение
характер
писатель
половина
родитель
участник
название
художник
работник
праздник
читатель
собрание
здоровье
скорость
режиссер
растение
существо
сентябрь
больница
животное
выставка
середина
страница
лестница
ресторан
принятие
кандидат
километр
водитель
академия
проверка
обучение
памятник
ожидание
описание
интернет
приятель
введение
согласие
человек
сторона
ребенок
женщина
решение
история
уровень
порядок
комната
интерес
мужчина
чувство
правило
встреча
товарищ
очередь
девушка
течение
девочка
мальчик
свобода
команда
природа
картина
процент
вариант
самолет
телефон
предмет
миллион
магазин
участок
желание
возраст
кабинет
будущее
продукт
радость
капитан
деревня
рассказ
учитель
прошлое
фамилия
студент
попытка
болезнь
детство
коридор
глубина
энергия
станция
реакция
секунда
явление
бабушка
декабрь
переход
красота
октябрь
счастие
корабль
концерт
зритель
кровать
конкурс
реклама
подруга
просьба
подарок
новость
инженер
эксперт
февраль
разница
зеркало
формула
поездка
общение
портрет
темнота
вершина
автобус
постель
автомат
старуха
подъезд
потолок
совесть
поворот
поселок
дыхание
счастье
планета
таблица
вопрос
страна
голова
машина
группа
начало
дорога
взгляд
момент
минута
помощь
партия
задача
письмо
сердце
неделя
газета
пример
данные
судьба
размер
воздух
музыка
хозяин
память
дерево
оценка
зрение
солнце
журнал
защита
знание
солдат
бумага
оружие
ребята
парень
доктор
камень
здание
собака
звезда
победа
цветок
сестра
карман
клетка
мастер
ошибка
список
колено
житель
высота
усилие
остров
фигура
стекло
январь
трубка
артист
одежда
охрана
клиент
улыбка
родина
апрель
кресло
ладонь
беседа
лагерь
ученик
польза
лошадь
костюм
восток
потеря
звонок
ноябрь
тишина
книжка
деталь
палата
эффект
ракета
камера
запись
колесо
стакан
корень
ремонт
платье
кольцо
дворец
печать
забота
металл
дружба
талант
золото
яблоко
чтение
ворота
молоко
сказка
краска
время
жизнь
слово
место
конец
город
часть
земля
дверь
народ
число
голос
книга
связь
форма
школа
месяц
мысль
вечер
улица
театр
центр
семья
автор
ответ
совет
точка
глава
наука
стена
плечо
палец
номер
район
класс
кровь
герой
гость
фильм
выбор
завод
спина
объем
сцена
берег
песня
линия
текст
выход
успех
среда
сфера
сумма
ветер
грудь
огонь
волос
масса
товар
целое
рамка
налог
отдел
вывод
норма
кухня
пункт
доход
вирус
режим
актер
поиск
карта
сосед
слеза
волна
птица
запах
весна
тайна
запад
поезд
музей
судья
поток
трава
сотня
честь
крыша
труба
канал
осень
дождь
сутки
нефть
кусок
масло
билет
сезон
запас
экран
пакет
вагон
длина
крыло
цифра
полет
живот
серия
кулак
доска
отказ
буква
порог
шутка
мешок
лодка
спорт
покой
взрыв
сапог
диван
почва
замок
мечта
отдых
сюжет
озеро
новое
дочка
ручка
хвост
нация
набор
сумка
песок
танец
горло
кость
дело
день
рука
лицо
глаз
друг
отец
нога
вода
жена
ночь
стол
путь
душа
язык
цель
роль
мама
мера
труд
тело
утро
идея
окно
счет
цена
план
вещь
срок
опыт
брат
игра
край
банк
врач
небо
факт
союз
поле
угол
двор
стих
море
удар
поэт
пара
дочь
папа
курс
губа
река
лето
итог
цвет
лист
гора
воля
зона
метр
снег
этап
враг
клуб
сеть
семь
боль
кожа
знак
звук
зима
хлеб
суть
этаж
дядя
чудо
ужас
тень
рыба
дама
июнь
мозг
беда
дача
стул
июль
спор
вино
ключ
царь
вкус
слой
слух
очко
мясо
урок
крик
ящик
щека
кадр
мост
вход
обед
смех
сбор
риск
штаб
брак
штат
куст
шанс
кофе
конь
вина
тетя
полк
село
темп
уход
мина
год
дом
вид
час
имя
муж
сын
век
ряд
ход
лес
тип
шаг
зал
сон
нос
ухо
сад
рис
рот
бой
зуб
газ
дед
шея
фон
лоб
бок
вес
дно
тон
нож
шум
дым
лед
миг
еда
мэр
ум";
    private bool gameIsStarted = false;
    private float timer;

    private GameObject wordsBlocksParentPanel;
    private List<InputField> inputFields = new List<InputField>();
    private List<string> wordsList = new List<string>();
    private string[] rightWords;
    private bool[] answers;

    void Start()
    {
        SetComplexity();

        wordsList = GetWordsFromText(textWords);
        var size = wordsBlocksParentPanel.transform.childCount;

        rightWords = new string[size];
        answers = new bool[size];

        for (int i = 0; i < size; i++)
        {
            inputFields.Add(wordsBlocksParentPanel.transform.GetChild(i).GetComponent<InputField>());
        }
    }

    void Update()
    {
        if (timer > 0 && gameIsStarted)
        {
            Timer.text = timer.ToString("F0");
            timer -= Time.deltaTime;
        }
    }

    private void SetComplexity()
    {
        complexityLevel = ComplexityClass.GetComplexity(PlayerPrefs.GetFloat("Complexity", 0));
        if (complexityLevel == Complexity.Easy)
        {
            wordsBlocksParentPanel = EasyWordsBlocksParentPanel;
            timeToRemember = 15;
            timeToWrite = 30;
        }
        else if (complexityLevel == Complexity.Medium)
        {
            wordsBlocksParentPanel = MediumWordsBlocksParentPanel;
            timeToRemember = 32;
            timeToWrite = 50;
        }
        else
        {
            wordsBlocksParentPanel = HardWordsBlocksParentPanel;
            timeToRemember = 60;
            timeToWrite = 80;
        }
        wordsBlocksParentPanel.SetActive(true);
    }

    public IEnumerator ShowingIteration()   // показ слов
    {
        DisableFields();

        Sequencer sequencer = new Sequencer(wordsList.Count);
        var sequence = sequencer.GetSequence();

        for (int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].text = wordsList[sequence[i]];
            rightWords[i] = wordsList[sequence[i]];
        }

        timer = timeToRemember;
        gameIsStarted = true;

        yield return new WaitForSeconds(timeToRemember);

        ResetFields();
        EnableFields();
        StartCoroutine(GettingIteration(true));
    }

    public IEnumerator GettingIteration(bool withTimer)     // проверка введенных слов
    {
        SkipButton.SetActive(withTimer);
        if (withTimer)
        {
            timer = timeToWrite;
            yield return new WaitForSeconds(timeToWrite);
        }
        else Timer.text = "0";

        for (int i = 0; i < inputFields.Count; i++)
        {
            answers[i] = inputFields[i].text.ToLower() == rightWords[i];
            inputFields[i].text = rightWords[i];
            inputFields[i].GetComponent<Graphic>().color = answers[i] ? new Color32(115, 255, 115, 255)
            : new Color32(255, 115, 115, 255);
        }

        SaveResults(answers.Where(answer => answer).Count());

        yield return new WaitForSeconds(5f);

        ResetFields();
        StartCoroutine(ShowingIteration());
    }

    public void ResetFields()
    {
        foreach (var inputField in inputFields)
        {
            inputField.GetComponent<Graphic>().color = new Color32(255, 255, 255, 255);
            inputField.text = "";
        }
    }

    public void OnClickSkipTimer()
    {
        StopAllCoroutines();
        StartCoroutine(GettingIteration(false));
    }

    public void SaveResults(int score)
    {
        switch (complexityLevel)
        {
            case Complexity.Easy:
                GamesData.EasyComplexityGamesData.WordsLast = score * 10;
                if (GamesData.EasyComplexityGamesData.WordsBest < score * 10)
                    GamesData.EasyComplexityGamesData.WordsBest = score * 10;
                break;
            case Complexity.Medium:
                GamesData.MediumComplexityGamesData.WordsLast = score * 12;
                if (GamesData.MediumComplexityGamesData.WordsBest < score * 12)
                    GamesData.MediumComplexityGamesData.WordsBest = score * 12;
                break;
            case Complexity.Hard:
                GamesData.HardComplexityGamesData.WordsLast = score * 15;
                if (GamesData.HardComplexityGamesData.WordsBest < score * 15)
                    GamesData.HardComplexityGamesData.WordsBest = score * 15;
                break;
        }
        GamesData.SaveStatistics();
    }

    public void EnableFields()
    {
        foreach (var inputField in inputFields)
        {
            inputField.interactable = true;
        }
    }

    public void DisableFields()
    {
        foreach (var inputField in inputFields)
        {
            inputField.interactable = false;
        }
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickStartButton()
    {
        StartCoroutine(ShowingIteration());
        StartButton.SetActive(false);
    }

    private List<string> GetWordsFromText(string text)
    {
        List<string> result = new List<string>();
        result = text.Split(new char[] { '\n', '\r', ' ', }, StringSplitOptions.RemoveEmptyEntries).ToList();
        return result;
    }
}
