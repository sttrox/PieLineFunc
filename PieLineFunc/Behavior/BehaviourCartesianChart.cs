using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Xaml.Behaviors;

namespace PieLineFunc.Behavior
{
    public class BehaviourCartesianChart : Behavior<CartesianChart>
    {
        private const double COEFF_STEEP = 0.64f;

        #region DependencyProperties

        #region Fields DependencyProperties

        public static readonly DependencyProperty SteepYProperty;
        public static readonly DependencyProperty SteepXProperty;
        public static readonly DependencyProperty LockYProperty;
        public static readonly DependencyProperty LockXProperty;

        #endregion

        #region Properties DependencyProperties

        public double SteepY
        {
            get { return (double) GetValue(SteepYProperty); }
            set { SetValue(SteepYProperty, value); }
        }

        public double SteepX
        {
            get { return (double) GetValue(SteepXProperty); }
            set { SetValue(SteepXProperty, value); }
        }

        public bool LockY
        {
            get { return (bool) GetValue(LockYProperty); }
            set { SetValue(LockYProperty, value); }
        }

        public bool LockX
        {
            get { return (bool) GetValue(LockXProperty); }
            set { SetValue(LockXProperty, value); }
        }

        #endregion

        #region Methods DependencyProperties

        private static void SteepY_DependencyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BehaviourCartesianChart;
            var value = (double) e.NewValue;
            //if (value != null)
            //    control.SteepY = value;
        }

        private static void SteepX_DependencyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BehaviourCartesianChart;
            var value = (double) e.NewValue;
            //if (value != null)
            //    control.SteepX = value;
        }

        private static void LockY_DependencyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BehaviourCartesianChart;
            var value = (bool) e.NewValue;
            //if (value != null)
            //    control.LockY = value;
        }

        private static void LockX_DependencyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BehaviourCartesianChart;
            var value = (bool) e.NewValue;
            //if (value != null)
            //    control.LockX = value;
        }

        #endregion

        #endregion

        static BehaviourCartesianChart()
        {
            #region Initialization DependencyProperties

            SteepYProperty = DependencyProperty.Register("SteepY",
                typeof(double), typeof(BehaviourCartesianChart),
                new PropertyMetadata(0d, SteepY_DependencyChange));
            SteepXProperty = DependencyProperty.Register("SteepX",
                typeof(double), typeof(BehaviourCartesianChart),
                new PropertyMetadata(0d, SteepX_DependencyChange));

            LockYProperty = DependencyProperty.Register("LockY",
                typeof(bool), typeof(BehaviourCartesianChart),
                new PropertyMetadata(false, LockY_DependencyChange));

            LockXProperty = DependencyProperty.Register("LockX",
                typeof(bool), typeof(BehaviourCartesianChart),
                new PropertyMetadata(false, LockX_DependencyChange));

            #endregion
        }


        private ChartPoint point;

        protected override void OnAttached()
        {
            this.AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
            this.AssociatedObject.DataClick += AssociatedObjectOnDataClick;
            this.AssociatedObject.MouseUp += AssociatedObjectOnMouseUp;
        }

        private void AssociatedObjectOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            point = null;
        }

        private void AssociatedObjectOnDataClick(object sender, ChartPoint chartpoint)
        {
            point = chartpoint;
        }

        private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs e)
        {
            if (point == null) return;
            var chart = (LiveCharts.Wpf.CartesianChart) e.Source;

            var positionMouse = e.GetPosition((IInputElement) sender);

            var pointMouse = chart.ConvertToChartValues(positionMouse);

            var currentPoint = (ObservablePoint) point.Instance;

            if (!LockY && SteepY != 0)
            {
                var p = (int) (((pointMouse.Y + (SteepY * COEFF_STEEP)) / SteepY));
                var r = p * (SteepY);
                var d = (double) (new decimal(r));
                pointMouse.Y = d;
            }

            if (!LockX && SteepX != 0)
            {
                var p = (int) (((pointMouse.X + (SteepX * COEFF_STEEP)) / SteepX));
                var r = p * SteepX;
                var d = (double) (new decimal(r));
                pointMouse.X = d;
            }

            if (LockX || LockY)
            {
                currentPoint.Y = LockY ? currentPoint.Y : pointMouse.Y;
                currentPoint.X = LockX ? currentPoint.X : pointMouse.X;
            }
            else
            {
                currentPoint.Y = pointMouse.Y;
                currentPoint.X = pointMouse.X;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}