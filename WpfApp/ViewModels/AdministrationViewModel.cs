using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DataLayer;
using WpfApp.Commands;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class AdministrationViewModel : NotifyDataErrorInfoBase
    {
        public AdministrationViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        private void Fill()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<User>(context.Users.ToList());
                NewItem = new User();
                UpdatePassword = false;
                NewPassword = null;
            }
            Mouse.OverrideCursor = null;
        }

        private ObservableCollection<User> _dataCollection;
        private User _selected;
        private bool _updatePassword;
        private string _newPassword;
        private User _newItem;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _updateCommand;

        public bool UpdatePassword
        {
            get => _updatePassword;
            set 
            { 
                _updatePassword = value;
                OnPropertyChanged(nameof(UpdatePassword));
            }
        }
        public string NewPassword
        {
            get => _newPassword;
            set 
            { 
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }



        public User Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public User NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<User> DataCollection
        {
            get => _dataCollection;
            set
            {
                _dataCollection = value;
                OnPropertyChanged(nameof(DataCollection));
            }
        }

        public RelayCommand CreateCommand => _createCommand ?? (new RelayCommand(obj =>
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                using (var context = new DataContext(cn))
                {
                    context.Users.Add(NewItem);
                    context.SaveChanges();
                    Fill();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось добавить новую запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }, (obj) => CheckItem()));

        public RelayCommand UpdateCommand => _updateCommand ?? (new RelayCommand(obj =>
        {   
            if (UpdatePassword)
            {
                if (string.IsNullOrWhiteSpace(NewPassword))
                {
                    MessageBox.Show("Укажите новый пароль", "Не заполнено обязательное поле", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Selected.password = NewPassword;
            }

            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                using (var context = new DataContext(cn))
                {
                    context.Entry(Selected).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    Fill();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось удалить запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }, (obj) => Selected != null));


        public RelayCommand DeleteCommand => _deleteCommand ?? (new RelayCommand(obj =>
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                using (var context = new DataContext(cn))
                {
                    var item = context.Users.Find(Selected.user_id);
                    if (item != null)
                    {
                        context.Users.Remove(item);
                        context.SaveChanges();
                        Fill();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось удалить запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }, (obj) => Selected != null));

        private bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(NewItem.login)) return false;
            if (string.IsNullOrWhiteSpace(NewItem.password)) return false;
            return true;
        }
    }
}
