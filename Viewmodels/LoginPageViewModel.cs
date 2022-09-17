using System;
using System.Threading.Tasks;
using Realms.Sync;
using FireSignage.Views;
using FireSignage.Viewmodels;
using Android.Runtime;

namespace FireSignage.Viewmodels
{
   
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private string password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            DoLogin();
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            RegisterUser();
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion


        public event EventHandler<EventArgs> OperationCompeleted;

        private async void DoLogin()
        {
            try
            {
                var user = await App.realmApp.LogInAsync(Credentials.EmailPassword(Email, password));
                if (user != null)
                {


                    OperationCompeleted?.Invoke(this, EventArgs.Empty);
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");


                }
                else
                {
                    HandleFailure();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
            }



            return;

        }


        private async void RegisterUser()
        {
            try
            {
                await App.realmApp.EmailPasswordAuth.RegisterUserAsync(Email, password);

                DoRegisterLogin();


                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }

        private async void DoRegisterLogin()
        {
            try
            {
                var user = await App.realmApp.LogInAsync(Credentials.EmailPassword(Email, password));
                if (user != null)
                {


                    OperationCompeleted?.Invoke(this, EventArgs.Empty);



                }
                else
                {
                    HandleFailure();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
            }

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

            return;

        }

        private void HandleFailure()
        {
            App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
            //throw new NotImplementedException();
        }

        


    }
}