using DocTemplate.CardViews.Cards;
using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DocTemplate.CardViews.View
{
    /// <summary>
    /// Логика взаимодействия для GroupVIew.xaml
    /// </summary>
    ///
    [Serializable]
    public partial class GroupView : UserControl
    {
        public GroupView()
        {
            InitializeComponent();
        }

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(GroupView), new UIPropertyMetadata(null));

        public bool CanEditOrDelete
        {
            get => (bool)GetValue(CanEditOrDeleteProperty);
            set => SetValue(CanEditOrDeleteProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanEditOrDeleteProperty =
            DependencyProperty.Register("CanEditOrDelete", typeof(bool), typeof(GroupView), new UIPropertyMetadata(null));


        public ObservableCollection<TemplateCard> GroupedTemplates
        {
            get => (ObservableCollection<TemplateCard>)GetValue(GroupedTemplatesProperty) ?? new ObservableCollection<TemplateCard>();
            set => SetValue(GroupedTemplatesProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupedTemplatesProperty =
            DependencyProperty.Register("GroupedTemplates", typeof(ObservableCollection<TemplateCard>), typeof(GroupView), new UIPropertyMetadata(null));
        public BindableCommand ButtonCommand { get; set; }

        private void EditGroup(object sender, RoutedEventArgs e)
        {
            var editDialog = new TypeInDialog
            {
                DialogName = "Редактирование группы",
                Placeholder = "Введите имя группы",
                ButtonText = "Изменить"
            };
            if (editDialog.ShowDialog() == true)
            {
                GroupName = editDialog.WroteText;
            }
        }

        private void DeleteGroup(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new YesNoDialog
            {
                DialogName = "Удаление группы",
                Description = "Вы уверены что хотите удалить группу? Публичные шаблоны из нее будут удалены. Созданные вами шаблоны будут находится в группе \"Созданные мной\"",
            };

            if (deleteDialog.ShowDialog() == true)
                ButtonCommand.Execute(null);
        }

    }
}
