using System;
using Realms.Sync;
 

namespace FireSignage.Viewmodels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string name;

        private string password;

        private string confirmPassword;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
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

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
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

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
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



            return;

        }

        private void HandleFailure()
        {
            App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
            //throw new NotImplementedException();
        }

    }
}