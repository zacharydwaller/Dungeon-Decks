using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{
    public static Color ParseRBG(string input)
    {
        string[] values = input.Split(',');
        Color output = new Color();

        for(int i = 0; i < 3; i++)
        {
            float tmp = float.Parse(values[i]);

            output[i] = tmp / 255;
        }
        output.a = 1.0f;

        return output;
    }
}