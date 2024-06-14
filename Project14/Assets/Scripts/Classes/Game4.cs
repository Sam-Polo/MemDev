using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public enum CellType
{
    Bomb,
    Place,
    Chest,
    Player,
    DeathsPlace,
    WinPlace
}

public enum Direction
{
    Left,
    Up,
    Right,
    Down
}

public class Game4
{
    public event Action MovedToBomb;

    public event Action MovedToChest;

    public event Action MovedToGround;

    public CellType[,] Map { get; private set; }
    public Point PlayerPosition { get; private set; }
    public Point ChestPosition { get; private set; }
    public int Size { get; private set; }
    public int BombCount { get; private set; }

    private Dictionary<Direction, Size> directions;

    public Game4(int size, int bombCount)
    {
        Size = size;
        BombCount = bombCount;
        CreateDirectionsDictionary();
        CreateGame();
    }

    public void PlayerMove(Direction direction)
    {
        var newPosition = PlayerPosition + directions[direction];
        if (!InBounds(newPosition)) return;

        var newPositionElement = Map[newPosition.X, newPosition.Y];
        Map[PlayerPosition.X, PlayerPosition.Y] = CellType.Place;
        PlayerPosition += directions[direction];

        switch (newPositionElement)
        {
            case CellType.Bomb:
                Map[PlayerPosition.X, PlayerPosition.Y] = CellType.DeathsPlace;
                MovedToBomb?.Invoke();
                break;
            case CellType.Place:
                Map[PlayerPosition.X, PlayerPosition.Y] = CellType.Player;
                MovedToGround?.Invoke();
                break;
            case CellType.Chest:
                Map[PlayerPosition.X, PlayerPosition.Y] = CellType.WinPlace;
                MovedToChest?.Invoke();
                break;
        }
    }

    private void CreateEmptyMap()
    {
        Map = new CellType[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Map[i, j] = CellType.Place;
            }
        }
    }

    private void CreateGame()
    {
        CreateEmptyMap();
        SetPlayerAndChest();
        SetBombs();
    }

    private void SetBombs()
    {
        var bombsCount = BombCount + GetRandomInt(2);
        for (int i = 0; i < bombsCount; i++)
        {
            List<Point> possiblePoints = new List<Point>();
            var path = FindPath(PlayerPosition, ChestPosition);
            path.RemoveAt(path.Count - 1);  // исключаем положения сокровища из пути

            foreach (var point in path)
            {
                Map[point.X, point.Y] = CellType.Bomb;
                var pathThen = FindPath(PlayerPosition, ChestPosition);

                if (pathThen != null) possiblePoints.Add(point);

                Map[point.X, point.Y] = CellType.Place;
            }

            if (possiblePoints.Count == 0)
                SetRandomBomb(path);
            else
            {
                var randomNumber = GetRandomInt(7) % 3;
                var randomPoint = Point.Empty;

                if (i == 1 || i == 2 || randomNumber % 3 != 0)
                {
                    randomNumber = GetRandomInt(possiblePoints.Count);
                    randomPoint = possiblePoints[randomNumber];
                    Map[randomPoint.X, randomPoint.Y] = CellType.Bomb;
                }
                else
                    SetRandomBomb(path);
            }
        }
    }

    private void SetPlayerAndChest()
    {
        Point player;
        Point chest;

        while (true)
        {
            player = new Point(GetRandomInt(Size), GetRandomInt(Size));
            chest = new Point(GetRandomInt(Size), GetRandomInt(Size));

            if (GetDistance(player, chest) > 2) break;
        }

        PlayerPosition = player;
        ChestPosition = chest;
        Map[player.X, player.Y] = CellType.Player;
        Map[chest.X, chest.Y] = CellType.Chest;
    }

    private void SetRandomBomb(List<Point> forbiddenPoints)
    {
        while (true)
        {
            var randomPoint = new Point(GetRandomInt(Size), GetRandomInt(Size));
            if (Map[randomPoint.X, randomPoint.Y] == CellType.Place && !forbiddenPoints.Contains(randomPoint))
            {
                Map[randomPoint.X, randomPoint.Y] = CellType.Bomb;
                break;
            }
        }
    }

    private List<Point> FindPath(Point start, Point end)
    {
        var track = new Dictionary<Point, Point>();
        track[start] = new Point(-1, -1);
        var queue = new Queue<Point>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            foreach (var size in PossibleDirections(point))
            {
                var nextPoint = point + size;
                if (track.ContainsKey(nextPoint)) continue;
                track[nextPoint] = point;
                queue.Enqueue(nextPoint);
                if (nextPoint == ChestPosition) break;
            }
        }

        if (!track.ContainsKey(end)) return null;

        List<Point> result = new List<Point>();
        var a = end;

        while (track[a] != new Point(-1, -1))
        {
            result.Add(a);
            a = track[a];
        }

        result.Reverse();
        return result;
    }

    private List<Size> PossibleDirections(Point point)
    {
        List<Size> result = new List<Size>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Size direction = new Size(i, j);
                if (InBounds(point + direction) && (Map[(point + direction).X, (point + direction).Y]
                    == CellType.Place || Map[(point + direction).X, (point + direction).Y] == CellType.Chest)
                    && i * j == 0 && !(i == 0 && j == 0))
                {
                    result.Add(direction);
                }
            }
        }

        return result;
    }

    private int GetRandomInt(int end)
    {
        int temporalNumber = end;
        int rankCounter = 0;

        while (temporalNumber != 0)
        {
            temporalNumber /= 10;
            rankCounter++;
        }

        return (int)((UnityEngine.Random.value * Math.Pow(10, rankCounter)) % end);
    }

    private double GetDistance(Point a, Point b)
    {
        int deltaX = Math.Abs(a.X - b.X);
        int deltaY = Math.Abs(a.Y - b.Y);
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    private bool InBounds(Point point)
    {
        return point.X >= 0 && point.X < Size && point.Y >= 0 && point.Y < Size;
    }

    private void CreateDirectionsDictionary()
    {
        directions = new Dictionary<Direction, Size>()
        {
            [Direction.Left] = new Size(0, -1),
            [Direction.Up] = new Size(-1, 0),
            [Direction.Right] = new Size(0, 1),
            [Direction.Down] = new Size(1, 0)
        };
    }
}

