using System.Windows.Media;

namespace RotateEffect
{
    public interface IRotateTransform
    {
        double Angle { get; set; }
        double CenterX { get; set; }
        double CenterY { get; set; }
        Matrix Value { get; }

        RotateTransform Clone();
        RotateTransform CloneCurrentValue();
    }
}