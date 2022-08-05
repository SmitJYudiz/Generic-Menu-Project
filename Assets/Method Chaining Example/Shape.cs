using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    //from Hamza Herbou
    Vector2[] points;

    //default values: in case if not set when creating a new shape.
    float strokeWidth = 0f;
    Color fillColor = Color.white;
    Color strokeColor = Color.black;

    public static Shape Create(string shapeName)
    {
        return new GameObject("Shape-" + shapeName).AddComponent<Shape>();
    }

    public Shape SetPoints(Vector2[] shapePoints)
    {
        this.points = shapePoints;
        return this;
    }
    /*what the above SetPoints() method doing is: it sets the ShapePoints
    (i.e the array of Vector2 points), and then returns the same
    shape (i.e this), for again to be used by another method (i.e chaining)*/

    public Shape SetStrokeWidth(float shapeStrokeWidth)
    {
        this.strokeWidth = shapeStrokeWidth;
        return this;
    }

    public Shape SetStrokeColor(Color strokeColor)
    {
        this.strokeColor = strokeColor;
        return this;
    }

    public Shape SetFillColor(Color fillColor)
    {
        this.fillColor = fillColor;
        return this;
    }

    //since Draw() is the last method: set the return type to VOID 
    public void Draw()
    {
        string color = ColorUtility.ToHtmlStringRGBA(this.fillColor);
        Debug.Log("<color=#"+color+">"+"Draw " + gameObject.name + "</color>");
    }
}
