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
public class PoolGuard : IDisposable
{
    private static bool disposed;
    private static List<SpaceShip> _available = new List<SpaceShip>();
    private static List<SpaceShip> _inUse = new List<SpaceShip>();

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
        {
            this.Dispose();
        }

        disposed = true;
    }

    public static SpaceShip GetObject()
    {
        lock (_available)
        {
            if (_available.Count != 0)
            {
                SpaceShip po = _available[0];
                _inUse.Add(po);
                _available.RemoveAt(0);
                return po;
            }
            else
            {
                SpaceShip po = new Ship();
                _inUse.Add(po);
                return po;
            }
        }
    }

    public static void ReleaseObject(SpaceShip po)
    {
        CleanUp(po);

        lock (_available)
        {
            _available.Add(po);
            _inUse.Remove(po);
        }
    }

    protected static void CleanUp(SpaceShip po){
        po.coord=new double[]{0,0}; 
        po.speed=new double[]{0,0};
        po.fuel_count=0;
        po.consumption=0;
        po.corner=0;
        po.corner_speed=0;
    }
}