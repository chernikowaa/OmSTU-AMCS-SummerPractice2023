﻿﻿﻿namespace spacebattle;

public class SpaceShip
{
    public double[] coord = new double[2];
    public double[] speed = new double[2];
    public double fuel_count;
    public double consumption;
    public double corner;
    public double corner_speed;

    public static double[] Move(double[] coord, double[] speed)
    {
        if
        (
            (double.IsNaN(coord[0]))||(double.IsNaN(coord[1]))||
            (double.IsNaN(speed[0]))||(double.IsNaN(speed[1]))
        )
        throw new System.ArgumentException();
        else
        {
            coord[0]+=speed[0];
            coord[1]+=speed[1];
        }
        return coord;
    }

    public static double Fuel(double fuel_count ,double consumption)
    {
        if (fuel_count > consumption)
        {
            fuel_count-=consumption;
        }
        else throw new System.ArgumentException();
        return fuel_count;
    }

    public static double Corner_speed(double corner,double corner_speed)
    {
        if ((double.IsNaN(corner))||(double.IsNaN(corner_speed)))  
        throw new System.ArgumentException();
        else corner+=corner_speed;
        return corner;
    }
}