namespace spacebattle;

public class SpaceShip
{
    public double[] coord = new double[2];
    public double[] speed = new double[2];

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
}