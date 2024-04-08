using System;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace RotateEffect
{
    public sealed class RotateTransform : Transform, IRotateTransform
    {
        //
        // Summary:
        //     Identifies the System.Windows.Media.RotateTransform.Angle dependency property.
        public static readonly DependencyProperty AngleProperty;

        //
        // Summary:
        //     Identifies the System.Windows.Media.RotateTransform.CenterX dependency property.
        public static readonly DependencyProperty CenterXProperty;

        //
        // Summary:
        //     Identifies the System.Windows.Media.RotateTransform.CenterY dependency property.
        public static readonly DependencyProperty CenterYProperty;

        internal DUCE.MultiChannelResource _duceResource;

        internal const double c_Angle = 0.0;

        internal const double c_CenterX = 0.0;

        internal const double c_CenterY = 0.0;

        //
        // Summary:
        //     Gets or sets the angle, in degrees, of clockwise rotation.
        //
        // Returns:
        //     The angle, in degrees, of clockwise rotation. The default is 0.
        public double Angle
        {
            get
            {
                return (double)GetValue(AngleProperty);
            }
            set
            {
                ((DependencyObject)this).SetValueInternal(AngleProperty, (object)value);
            }
        }

        //
        // Summary:
        //     Gets or sets the x-coordinate of the rotation center point.
        //
        // Returns:
        //     The x-coordinate of the center of rotation. The default is 0.
        public double CenterX
        {
            get
            {
                return (double)GetValue(CenterXProperty);
            }
            set
            {
                ((DependencyObject)this).SetValueInternal(CenterXProperty, (object)value);
            }
        }

        //
        // Summary:
        //     Gets or sets the y-coordinate of the rotation center point.
        //
        // Returns:
        //     The y-coordinate of the center of rotation. The default is 0.
        public double CenterY
        {
            get
            {
                return (double)GetValue(CenterYProperty);
            }
            set
            {
                ((DependencyObject)this).SetValueInternal(CenterYProperty, (object)value);
            }
        }

        //
        // Summary:
        //     Gets the current rotation transformation as a System.Windows.Media.Matrix object.
        //
        // Returns:
        //     The current rotation transformation as a System.Windows.Media.Matrix.
        public override Matrix Value
        {
            get
            {
                ReadPreamble();
                Matrix result = default(Matrix);
                result.RotateAt(Angle, CenterX, CenterY);
                return result;
            }
        }

        internal override bool IsIdentity
        {
            get
            {
                if (Angle == 0.0)
                {
                    return base.CanFreeze;
                }

                return false;
            }
        }

        //
        // Summary:
        //     Creates a modifiable copy of this System.Windows.Media.RotateTransform by making
        //     deep copies of its values.
        //
        // Returns:
        //     A modifiable deep copy of the current object. The System.Windows.Freezable.IsFrozen
        //     property of the cloned object returns false even if the System.Windows.Freezable.IsFrozen
        //     property of the source is true.
        public new RotateTransform Clone()
        {
            return (RotateTransform)base.Clone();
        }

        //
        // Summary:
        //     Creates a modifiable copy of this System.Windows.Media.RotateTransform object
        //     by making deep copies of its values. This method does not copy resource references,
        //     data bindings, or animations, although it does copy their current values.
        //
        // Returns:
        //     A modifiable deep copy of the current object. The System.Windows.Freezable.IsFrozen
        //     property of the cloned object is false even if the System.Windows.Freezable.IsFrozen
        //     property of the source is true.
        public new RotateTransform CloneCurrentValue()
        {
            return (RotateTransform)base.CloneCurrentValue();
        }

        private static void AnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RotateTransform rotateTransform = (RotateTransform)d;
            rotateTransform.PropertyChanged(AngleProperty);
        }

        private static void CenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RotateTransform rotateTransform = (RotateTransform)d;
            rotateTransform.PropertyChanged(CenterXProperty);
        }

        private static void CenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RotateTransform rotateTransform = (RotateTransform)d;
            rotateTransform.PropertyChanged(CenterYProperty);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new RotateTransform();
        }

        [SecurityCritical]
        [SecurityTreatAsSafe]
        internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
        {
            if (skipOnChannelCheck || _duceResource.IsOnChannel(channel))
            {
                base.UpdateResource(channel, skipOnChannelCheck);
                DUCE.ResourceHandle animationResourceHandle = GetAnimationResourceHandle(AngleProperty, channel);
                DUCE.ResourceHandle animationResourceHandle2 = GetAnimationResourceHandle(CenterXProperty, channel);
                DUCE.ResourceHandle animationResourceHandle3 = GetAnimationResourceHandle(CenterYProperty, channel);
                DUCE.MILCMD_ROTATETRANSFORM mILCMD_ROTATETRANSFORM = default(DUCE.MILCMD_ROTATETRANSFORM);
                mILCMD_ROTATETRANSFORM.Type = MILCMD.MilCmdRotateTransform;
                mILCMD_ROTATETRANSFORM.Handle = _duceResource.GetHandle(channel);
                if (animationResourceHandle.IsNull)
                {
                    mILCMD_ROTATETRANSFORM.Angle = Angle;
                }

                mILCMD_ROTATETRANSFORM.hAngleAnimations = animationResourceHandle;
                if (animationResourceHandle2.IsNull)
                {
                    mILCMD_ROTATETRANSFORM.CenterX = CenterX;
                }

                mILCMD_ROTATETRANSFORM.hCenterXAnimations = animationResourceHandle2;
                if (animationResourceHandle3.IsNull)
                {
                    mILCMD_ROTATETRANSFORM.CenterY = CenterY;
                }

                mILCMD_ROTATETRANSFORM.hCenterYAnimations = animationResourceHandle3;
                channel.SendCommand((byte*)(&mILCMD_ROTATETRANSFORM), sizeof(DUCE.MILCMD_ROTATETRANSFORM));
            }
        }

        internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
        {
            if (_duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_ROTATETRANSFORM))
            {
                AddRefOnChannelAnimations(channel);
                UpdateResource(channel, skipOnChannelCheck: true);
            }

            return _duceResource.GetHandle(channel);
        }

        internal override void ReleaseOnChannelCore(DUCE.Channel channel)
        {
            if (_duceResource.ReleaseOnChannel(channel))
            {
                ReleaseOnChannelAnimations(channel);
            }
        }

        internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
        {
            return _duceResource.GetHandle(channel);
        }

        internal override int GetChannelCountCore()
        {
            return _duceResource.GetChannelCount();
        }

        internal override DUCE.Channel GetChannelCore(int index)
        {
            return _duceResource.GetChannel(index);
        }

        static RotateTransform()
        {
            Type typeFromHandle = typeof(RotateTransform);
            AngleProperty = Animatable.RegisterProperty("Angle", typeof(double), typeFromHandle, 0.0, AnglePropertyChanged, null, isIndependentlyAnimated: true, null);
            CenterXProperty = Animatable.RegisterProperty("CenterX", typeof(double), typeFromHandle, 0.0, CenterXPropertyChanged, null, isIndependentlyAnimated: true, null);
            CenterYProperty = Animatable.RegisterProperty("CenterY", typeof(double), typeFromHandle, 0.0, CenterYPropertyChanged, null, isIndependentlyAnimated: true, null);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Media.RotateTransform class.
        public RotateTransform()
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Media.RotateTransform class
        //     that has the specified angle, in degrees, of clockwise rotation. The rotation
        //     is centered on the origin, (0,0).
        //
        // Parameters:
        //   angle:
        //     The clockwise rotation angle, in degrees.
        public RotateTransform(double angle)
        {
            Angle = angle;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Media.RotateTransform class
        //     that has the specified angle and center point.
        //
        // Parameters:
        //   angle:
        //     The clockwise rotation angle, in degrees. For more information, see the System.Windows.Media.RotateTransform.Angle
        //     property.
        //
        //   centerX:
        //     The x-coordinate of the center point for the System.Windows.Media.RotateTransform.
        //     For more information, see the System.Windows.Media.RotateTransform.CenterX property.
        //
        //   centerY:
        //     The y-coordinate of the center point for the System.Windows.Media.RotateTransform.
        //     For more information, see the System.Windows.Media.RotateTransform.CenterY property.
        public RotateTransform(double angle, double centerX, double centerY)
            : this(angle)
        {
            CenterX = centerX;
            CenterY = centerY;
        }
    }
}
