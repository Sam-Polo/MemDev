using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer
{
    public int Size { get; }
    private List<int> sequence;

    public Sequencer(int size)
    {
        Size = size;
        sequence = CreateSequence();
    }

    public List<int> GetSequence(int currentSize = -1)
    {
        if (currentSize == -1) return sequence;

        List<int> result = new List<int>();
        for (int i = 0; i < currentSize; i++)
            result.Add(sequence[i]);
        return result;
    }

    private List<int> CreateSequence()  // cоздаёт последовательность уникальных случайных чисел
    {
        List<int> result = new List<int>();

        while (result.Count != Size)
        {
            var number = GetRandomInt(Size);
            if (!result.Contains(number))
                result.Add(number);
        }

        return result;
    }

    private int GetRandomInt(int end)   // генерирует случайное число
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
}
