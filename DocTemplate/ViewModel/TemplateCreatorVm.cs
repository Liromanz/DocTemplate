using DocTemplate.Global;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.ServerHandler.API;
using DocTemplate.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace DocTemplate.ViewModel
{
    public class TemplateCreatorVm : ObservableObject
    {
        #region Команды
        public BindableCommand AddEditorCommand { get; set; }
        public BindableCommand AddUserCommand { get; set; }
        public BindableCommand OpenEditorCommand { get; set; }
        public BindableCommand SaveAndExitCommand { get; set; }
        public BindableCommand ReturnCommand { get; set; }

        #endregion

        #region Переменные
        public Window ThisWindow { get; set; }

        private List<string> _usernames;
        public List<string> Usernames
        {
            get => _usernames;
            set
            {
                _usernames = value;
                OnPropertyChanged();
            }
        }


        private Template _template;
        public Template Template
        {
            get => _template;
            set
            {
                _template = value;
                Text = value.FileText;
                Editors = value.Editors;
                Users = value.Users;
                OnPropertyChanged();
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private string _editorToAdd;
        public string EditorToAdd
        {
            get => _editorToAdd;
            set
            {
                _editorToAdd = value;
                OnPropertyChanged();
            }
        }

        private string _editors;
        public string Editors
        {
            get => _editors;
            set
            {
                _editors = value;
                OnPropertyChanged();
            }
        }

        private string _userToAdd;
        public string UserToAdd
        {
            get => _userToAdd;
            set
            {
                _userToAdd = value;
                OnPropertyChanged();
            }
        }


        private string _users;
        public string Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public TemplateCreatorVm()
        {
            Template = new Template { IdUser = Properties.Settings.Default.UserID };
            AddEditorCommand = new BindableCommand(x => AddEditor());
            AddUserCommand = new BindableCommand(x => AddUser());
            OpenEditorCommand = new BindableCommand(x => OpenEditor());
            SaveAndExitCommand = new BindableCommand(x => SaveAndExit());
            ReturnCommand = new BindableCommand(x => ReturnToWindow());

            if (InternetState.IsConnectedToInternet())
            {
                Thread thread = new Thread(x =>
                {
                    Usernames = (List<string>)JsonConvert.DeserializeObject(Requests.GetRequest("Users"), typeof(List<string>));
                });
                thread.Start();
            }
        }

        private void AddEditor()
        {
            if (Usernames.Contains(EditorToAdd))
                Editors += EditorToAdd + ", ";
            else
                MessageBox.Show("Такого пользователя нет. Если вы уверены, что он есть, перезайдите в окно создания шаблона или проверьте подключение к интернету.\nБез интернета можно выбрать только свойство \"Все\" или \"Только я\"");
        }

        private void AddUser()
        {
            if (Usernames.Contains(UserToAdd))
                Users += UserToAdd + ", ";
            else
                MessageBox.Show("Такого пользователя нет. Если вы уверены, что он есть, перезайдите в окно создания шаблона или проверьте подключение к интернету.\nБез интернета можно выбрать только свойство \"Все\" или \"Только я\"");
        }

        private void OpenEditor()
        {
            var editor = new TemplateEditorWindow();
            editor.ViewModel.FieldMetadatas = JsonConvert.DeserializeObject<List<FieldMetadata>>(Template.FieldMetadata) ?? new List<FieldMetadata>();
            editor.ViewModel.RtfContent = Template.FileText;
            ThisWindow.Visibility = Visibility.Collapsed;
            if (editor.ShowDialog() == true)
            {
                Text = editor.ViewModel.RtfContent;
                Template.FileText = Text;
                Template.FieldMetadata = editor.ViewModel.JsonMetadata;
            }
            ThisWindow.Visibility = Visibility.Visible;
        }

        private void SaveAndExit()
        {
            var validation = ValidationErrorMessage();
            if (validation != String.Empty)
            {
                MessageBox.Show(validation);
                return;
            }

            Template.Editors = ParceTemplate(Template.Editors, "Editors");
            Template.Users = ParceTemplate(Template.Users, "Users");
            if (InternetState.IsConnectedToInternet())
            {
                Thread thread = new Thread(async x =>
                {
                    string result;
                    result = Template.IdTemplate == null
                        ? await Requests.PostWithBodyRequest("Templates", JsonConvert.SerializeObject(Template))
                        : Requests.PutRequest("Templates", Template.IdTemplate.Value,
                            JsonConvert.SerializeObject(Template));
                    if (result != GlobalConstants.SuccessMessage)
                        MessageBox.Show(result);
                });
                thread.Start();
            }
            else
            {
                Template.NeedToUpdate = true;
                foreach (var group in DataContainers.UserGroupsModel.Where(x => x.GroupedTemplates.Any(t => t.IdTemplate == Template.IdTemplate)))
                {
                    var index = group.GroupedTemplates.FindIndex(x => x.IdTemplate == Template.IdTemplate);
                    DataContainers.UserGroupsModel.First(x => x == group).GroupedTemplates[index] = Template;
                }
            }
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            ThisWindow.Close();
        }

        private void ReturnToWindow()
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            ThisWindow.Close();
        }

        private string ValidationErrorMessage()
        {
            if (Template.Name.Length < 3 || Template.Name.Length > 255) return "Поле \"Имя\" должно иметь длину не менее 3 символов и не более 100";
            if (Template.Editors == "Couple" && Editors == null) return "При выборе \"Некоторые пользователи\" в редакторах необходимо выбрать хотя бы одного пользователя";
            if (Template.Users == "Couple" && Users == null) return "При выборе \"Некоторые пользователи\" в просмотрщиках необходимо выбрать хотя бы одного пользователя";

            return String.Empty;
        }

        private string ParceTemplate(string permission, string typePermission)
        {
            switch (permission)
            {
                case "Me":
                    return Properties.Settings.Default.Username;
                case "Couple":
                    return typePermission == "Editors" ? Editors : Users;
                default:
                    return "All";
            }
        }
    }
}
