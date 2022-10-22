using System;
using System.Threading.Tasks;
using Realms.Sync;
using FireSignage.Views;
using FireSignage.Viewmodels;
using Android.Runtime;
using User = FireSignage.Models.User;
using Realms;

namespace FireSignage.Viewmodels
{
   
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields
        private Realm controllerRealm;

        private string password;
        private string fname;
        private string lname;
        private string licplate;

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

        public string FName
        {
            get
            {
                return this.fname;
            }

            set
            {
                if (this.fname == value)
                {
                    return;
                }
                this.fname = value;
                this.OnPropertyChanged();
            }

        }

        public string LName
        {
            get
            {
                return this.lname;
            }

            set
            {
                if (this.lname == value)
                {
                    return;
                }
                this.lname = value;
                this.OnPropertyChanged();
            }

        }

        public string LicPlate
        {
            get
            {
                return this.licplate;
            }

            set
            {
                if (this.licplate == value)
                {
                    return;
                }
                this.licplate = value;
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

            
            AddUserInfo();
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

        private async void AddUserInfo()
        {
            

            var userinfo = controllerRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
            controllerRealm.Write(() =>
            {
                userinfo.Id = App.realmApp.CurrentUser.Id;
                userinfo.OwnerId = App.realmApp.CurrentUser.Id;
                userinfo.Firstname = fname;
                userinfo.Lastname = lname; 
                userinfo.Licenseplate = licplate; 
                userinfo.Email = App.realmApp.CurrentUser.Profile.Email;
                userinfo.Password = password;

                
            });

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        //private void ChangeName(object obj)
        //{
        //    CheckLogin();
        //    var allPages = controllerRealm.All<DisplayChanges>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
        //    controllerRealm.Write(() =>
        //    {


        //        allPages.Id = App.realmApp.CurrentUser.Id;
        //        allPages.OwnerId = App.realmApp.CurrentUser.Id;
        //        allPages.Pagename = pagename;
        //        allPages.Textcolor = "White";
                

        //    });


        //}


    }
}