using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PieLineFunc.Controls
{
    public class TabControlAdvanced : TabControl
    {
        public TabControlAdvanced()
        {
            #region Initialization Commands

            #endregion
        }


        static TabControlAdvanced()
        {
            #region Override DependencyProperties

            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControlAdvanced),
                new FrameworkPropertyMetadata(typeof(TabControlAdvanced)));

            #endregion

            #region Initialization DependencyProperties

            AddItemCommandProperty = DependencyProperty.Register("AddItemCommand",
                typeof(ICommand), typeof(TabControlAdvanced),
                new PropertyMetadata(null));

            #endregion
        }


        #region Commands

        #region Methods Commands

        public ICommand AddItemCommand
        {
            get { return (ICommand) GetValue(AddItemCommandProperty); }
            set { SetValue(AddItemCommandProperty, value); }
        }

        #endregion

        #region Fields Commands

        public static readonly DependencyProperty AddItemCommandProperty;

        #endregion

        #endregion

        #region DependencyProperties

        #region Fields DependencyProperties

        #endregion

        #region Properties DependencyProperties

        #endregion

        #region Methods DependencyProperties

        #endregion

        #endregion

        //https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/how-to-create-a-custom-routed-event

        #region RoutedEvents

        #region Fields RoutedEvent

        #endregion

        #region Properties RoutedEvent

        #endregion

        #region Methods RoutedEvent

        #endregion

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}