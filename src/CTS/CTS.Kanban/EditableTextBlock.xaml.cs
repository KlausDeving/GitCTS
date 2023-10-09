using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CTS.Kanban;
/// <summary>
/// Interaction logic for EditableTextBlock.xaml
/// </summary>
public partial class EditableTextBlock : UserControl
{
    #region Constants

    #endregion

    #region Member Fields

    #endregion

    #region Dependency Properties

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBlock), new UIPropertyMetadata(string.Empty, OnTextChanged));

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        return;
    }

    public Brush TextBlockForegroundColor
    {
        get { return (Brush)GetValue(TextBlockForegroundColorProperty); }
        set { SetValue(TextBlockForegroundColorProperty, value); }
    }

    public static readonly DependencyProperty TextBlockForegroundColorProperty =
        DependencyProperty.Register("TextBlockForegroundColor", typeof(Brush), typeof(EditableTextBlock), new UIPropertyMetadata(null));

    public Brush TextBlockBackgroundColor
    {
        get { return (Brush)GetValue(TextBlockBackgroundColorProperty); }
        set { SetValue(TextBlockBackgroundColorProperty, value); }
    }

    public static readonly DependencyProperty TextBlockBackgroundColorProperty =
        DependencyProperty.Register("TextBlockBackgroundColor", typeof(Brush), typeof(EditableTextBlock), new UIPropertyMetadata(null));

    public Brush TextBoxForegroundColor
    {
        get { return (Brush)GetValue(TextBoxForegroundColorProperty); }
        set { SetValue(TextBoxForegroundColorProperty, value); }
    }

    public static readonly DependencyProperty TextBoxForegroundColorProperty =
        DependencyProperty.Register("TextBoxForegroundColor", typeof(Brush), typeof(EditableTextBlock), new UIPropertyMetadata(null));

    public Brush TextBoxBackgroundColor
    {
        get { return (Brush)GetValue(TextBoxBackgroundColorProperty); }
        set { SetValue(TextBoxBackgroundColorProperty, value); }
    }

    public static readonly DependencyProperty TextBoxBackgroundColorProperty =
        DependencyProperty.Register("TextBoxBackgroundColor", typeof(Brush), typeof(EditableTextBlock), new UIPropertyMetadata(null));

    #endregion

    #region Constructor
    public EditableTextBlock()
    {
        InitializeComponent();
        PART_GridContainer.DataContext=this;
    }
    #endregion

    #region Overrides Methods
    protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        this.PART_TbDisplayText.Visibility=Visibility.Hidden;
        this.PART_TbEditText.Visibility=Visibility.Visible;
    }

    #endregion

    #region Event Handlers

    private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
    {
        this.PART_TbDisplayText.Visibility=Visibility.Visible;
        this.PART_TbEditText.Visibility=Visibility.Hidden;
    }
    #endregion
}
