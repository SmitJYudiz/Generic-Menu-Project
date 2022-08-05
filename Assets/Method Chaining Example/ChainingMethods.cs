using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChainingMethods : MonoBehaviour
{
    //normal way: Draw 1 shape:
    //Shape s = new Shape();
    //s.SetFillColor(Color.red);
    //s.SetStrokeWidth(5f);
    //s.Draw();

    private void Start()
    {
        Shape.Create("Default").Draw();

        Shape.Create("Circle").SetFillColor(Color.magenta).Draw();

        Shape.Create("triangle").SetFillColor(Color.cyan).Draw();
        //Vector2[] points = new Vector2[] { new Vector2(0,0), new Vector2(0,5), new Vector2(5,0), new Vector2(0,0) };

        //Shape.Create("DooDoo").SetPoints(points).Draw();

    }

    //Draw 3 shapes:

    //Shape.Create("Default").Draw;

    //Shape.Create("Rectangle").SetFillColor(Color.cyan).Draw();

    //Shape.Create("Triangle").SetFillColor(Color.yellow).SetStrokeWidth(5f).Draw();


}



//notes:
/*
 * method chaining is also known as  Fluent Interfaces
 * 
 * Fluent Interface or Method chaining ios an object oriented API
 * it's goal is to Increase CODE LEGIBILITY, it's implemented by using METHOD CHAINING
 * 
 * 
 * 
 */