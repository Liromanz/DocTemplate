﻿using System.Windows;
using System.Windows.Controls;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;

namespace DocTemplate.CardViews.Cards
{
    /// <summary>
    /// Логика взаимодействия для TemplateCard.xaml
    /// </summary>
    public partial class TemplateCard : UserControl
    {
        public TemplateCard()
        {
            InitializeComponent();
        }
        public Template TemplateInfo
        {
            get => (Template)GetValue(TemplateInfoProperty);
            set => SetValue(TemplateInfoProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateInfoProperty =
            DependencyProperty.Register("TemplateInfo", typeof(Template), typeof(TemplateCard), new UIPropertyMetadata(null));

        public BindableCommand ClickCommand
        {
            get => (BindableCommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(BindableCommand), typeof(TemplateCard), new UIPropertyMetadata(null));
        public BindableCommand RightClickCommand
        {
            get => (BindableCommand)GetValue(RightClickCommandProperty);
            set => SetValue(RightClickCommandProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightClickCommandProperty =
            DependencyProperty.Register("RightClickCommand", typeof(BindableCommand), typeof(TemplateCard), new UIPropertyMetadata(null));
    }
}
