using System;
namespace FireSignage.Viewmodels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingViewModel" /> class
        /// </summary>
        public SettingsViewModel()
        {
            this.EditProfileCommand = new Command(this.EditProfileClicked);
            this.ChangePasswordCommand = new Command(this.ChangePasswordClicked);
            this.LinkAccountCommand = new Command(this.LinkAccountClicked);
            this.HelpCommand = new Command(this.HelpClicked);
            this.TermsCommand = new Command(this.TermsServiceClicked);
            this.PolicyCommand = new Command(this.PrivacyPolicyClicked);
            this.FAQCommand = new Command(this.FAQClicked);
            this.LogoutCommand = new Command(this.LogoutClicked);

            Title = "Settings";
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command is executed when the edit profile option is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the change password option is clicked.
        /// </summary>
        public Command ChangePasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the account link option is clicked.
        /// </summary>
        public Command LinkAccountCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the help option is clicked.
        /// </summary>
        public Command HelpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the terms of service option is clicked.
        /// </summary>
        public Command TermsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the privacy policy option is clicked.
        /// </summary>
        public Command PolicyCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the FAQ option is clicked.
        /// </summary>
        public Command FAQCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the logout is clicked.
        /// </summary>
        public Command LogoutCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditProfileClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the change password clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void ChangePasswordClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the account link clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LinkAccountClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the terms of service clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void TermsServiceClicked(object obj)
        {
            await Launcher.OpenAsync("https://learn.microsoft.com/dotnet/maui");
        }

        /// <summary>
        /// Invoked when the privacy and policy clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void PrivacyPolicyClicked(object obj)
        {
            await Launcher.OpenAsync("https://learn.microsoft.com/dotnet/maui");
        }

        /// <summary>
        /// Invoked when the FAQ clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void FAQClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the logout button is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void LogoutClicked(object obj)
        {
            await App.realmApp.CurrentUser.LogOutAsync();
            await Shell.Current.GoToAsync("//..");

        }

        #endregion
    }
}