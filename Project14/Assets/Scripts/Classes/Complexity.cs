using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Complexity
{
    Easy,
    Medium,
    Hard
}

public static class ComplexityClass     // получение уровня сложности на основе положения scrollbar
{
    private static Complexity complexity;

    public static Complexity GetComplexity(float complexityFloat)   
    {
        if (complexityFloat >= 0.75)
        {
            return Complexity.Hard;
        }
        else if (complexityFloat > 0.25)
        {
            return Complexity.Medium;
        }

        return Complexity.Easy;
    }

}
