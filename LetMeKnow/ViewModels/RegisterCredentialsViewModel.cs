﻿using System.Windows.Input;

using Rg.Plugins.Popup.Services;

using LetMeKnow.Interfaces;

using Xamarin.Forms;
using System;
using LetMeKnow.Views;
using LetMeKnow.Services;
using System.ComponentModel;

namespace LetMeKnow.ViewModels {
    public class RegisterCredentialsViewModel : ViewModel {

        public ICommand FinishCmd { protected set; get; }
        public string Email { get; }

        private readonly IFirebaseAuthenticator firebaseAuth;
        private readonly IFirebaseDatabaseActor database;

        public RegisterCredentialsViewModel(IFirebaseAuthenticator firebaseAuth, IFirebaseDatabaseActor database) {
            Email = firebaseAuth.GetEmail();
            this.firebaseAuth = firebaseAuth;
            this.database = database;
            FinishCmd = new Command(() => {
                // Execute
                FinRegister();
            },
            () => {
                // CanExecute
                return IsClickable;
            });
        }

        public bool IsClickable { protected set; get; }
        private string username = "";
        public string UserName {
            // not protected
            set {
                if (username != value) {
                    username = value;
                    OnPropertyChanged("UserName");

                    // Enable click
                    IsClickable = UserName.Length > 4 && Password.Length >= 6;
                    ((Command)FinishCmd).ChangeCanExecute();
                }
            }

            get => username;
        }
        private string password = "";
        public string Password {
            // not protected
            set {
                if (password != value) {
                    password = value;
                    OnPropertyChanged("Password");

                    // Enable click
                    IsClickable = UserName.Length > 4 && Password.Length >= 6;
                    ((Command)FinishCmd).ChangeCanExecute();
                }
            }

            get { return password; }
        }

        private async void FinRegister() {
            await firebaseAuth.FinishRegistration(UserName, Password);
            await database.CreateCurrentUser();
            await PopupNavigation.Instance.PushAsync(new GenericPopup("You finished registration!\nNow redirecting to your homepage..."));
            await (Application.Current as App).MainPage.Navigation.PushAsync(new HomePage());
        }
    }
}
