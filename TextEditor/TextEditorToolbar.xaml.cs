﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for TextEditorToolbar.xaml
    /// </summary>
    public partial class TextEditorToolbar : UserControl
    {
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (double i = 8; i < 48; i += 2)
            {
                fontsize.Items.Add(i);
            }
        }
        public TextEditorToolbar()
        {
            InitializeComponent();
        }
        public bool IsSynchronizing { get; private set; }

        public void SynchronizeWith(TextSelection selection)
        {
            IsSynchronizing = true;
            Synchronize<double>(selection, TextBlock.FontSizeProperty, SetFontSize);
            Synchronize<FontWeight>(selection, TextBlock.FontWeightProperty, SetFontWeight);
            Synchronize<FontStyle>(selection, TextBlock.FontStyleProperty, SetFontStyle);
            Synchronize<TextDecorationCollection>(selection, TextBlock.TextDecorationsProperty, SetTextDecoration);
            Synchronize<FontFamily>(selection, TextBlock.FontFamilyProperty, SetFontFamily);
        }

        public void Synchronize<T>(TextSelection selection, DependencyProperty property, Action<T> methodToCall)
        {
            var value = selection.GetPropertyValue(property);
            if (value != DependencyProperty.UnsetValue)
                methodToCall((T)value);
        }
        private void SetFontSize(double size)
        {
            fontsize.SelectedValue = size;

        }

        private void SetFontWeight(FontWeight font)
        {
            boldButton.IsChecked = FontWeight == FontWeights.Bold;
        }

        private void SetFontStyle(FontStyle style)
        {
            italicButton.IsChecked = style == FontStyles.Italic;
        }
        private void SetTextDecoration(TextDecorationCollection decoration)
        {
            underlineButton.IsChecked = decoration == TextDecorations.Underline;
        }
        private void SetFontFamily(FontFamily family)
        {
            fonts.SelectedItem = family;
        }
    }
}
   
      

    

