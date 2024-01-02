using Microsoft.Xna.Framework;

namespace Wingstrike.ECS.Components;

public class Rectangle
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Color Color { get; set; }

    public Rectangle(int width, int height, Color color)
    {
        Width = width;
        Height = height;
        Color = color;
    }
}