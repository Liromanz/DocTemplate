using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocTemplate.View.ControlPanels.Settings
{
    /// <summary>
    /// Логика взаимодействия для HelpControlPanel.xaml
    /// </summary>
    public partial class HelpControlPanel : UserControl
    {
        public HelpControlPanel()
        {
            InitializeComponent();
        }

        private void SetAllToCollapsed()
        {
            MeetingPanel.Visibility = Visibility.Collapsed;
            PublicPanel.Visibility = Visibility.Collapsed;
            GroupPanel.Visibility = Visibility.Collapsed;
            DocumentEditPanel.Visibility = Visibility.Collapsed;
            TemplateCreationPanel.Visibility = Visibility.Collapsed;
            WithoutInternetPanel.Visibility = Visibility.Collapsed;
            SettingsPanel.Visibility = Visibility.Collapsed;
        }

        private void GreetingPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            MeetingPanel.Visibility = Visibility.Visible;
        }

        private void PublicPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            PublicPanel.Visibility = Visibility.Visible;
        }

        private void GroupPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            GroupPanel.Visibility = Visibility.Visible;
        }

        private void DocumentPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            DocumentEditPanel.Visibility = Visibility.Visible;
        }

        private void TemplatePanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            TemplateCreationPanel.Visibility = Visibility.Visible;
        }

        private void InternetPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            WithoutInternetPanel.Visibility = Visibility.Visible;
        }

        private void SettingPanelClick(object sender, MouseButtonEventArgs e)
        {
            SetAllToCollapsed();
            SettingsPanel.Visibility = Visibility.Visible;
        }
    }
}
