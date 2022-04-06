using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    public void Start()
    {
        // Create an array of Vector2 structures.
        Vector2[] points = { new Vector2(100, 200),
                         new Vector2(150, 250), new Vector2(250, 375),
                         new Vector2(275, 395), new Vector2(295, 450) };

        // Define the Predicate<T> delegate.
        Predicate<Vector2> predicate = FindPoint;

        // Find the first Vector2 structure for which X times Y
        // is greater than 100000.
        Vector2 first = Array.Find(points, predicate);

        // Display the first structure found.
        Debug.Log(string.Format("Found: X = {0}, Y = {1}", first.x, first.y));
    }

    private static bool FindPoint(Vector2 obj)
    {
        return obj.x * obj.y > 100000;
    }
}
