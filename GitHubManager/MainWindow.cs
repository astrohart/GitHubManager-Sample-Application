﻿using PostSharp.Patterns.Diagnostics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GitHubManager
{
    /// <summary>
    /// Main window of the application.
    /// </summary>
    public partial class MainWindow : Form, IMainWindow
    {
        /// <summary>
        /// Reference to an instance of an object that implements the
        /// <see cref="T:GitHubManager.IMainWindowPresenter" /> interface.
        /// </summary>
        private readonly IMainWindowPresenter Presenter;

        /// <summary>
        /// Empty, static constructor to prohibit direct allocation of this class.
        /// </summary>
        static MainWindow() { }

        /// <summary>
        /// Empty, protected constructor to prohibit direct allocation of this class.
        /// </summary>
        protected MainWindow()
        {
            InitializeComponent();

            Presenter = new MainWindowPresenter(this);

            Session.GitHubAuthenticated += OnGitHubAuthenticated;

            Application.Idle += OnUpdateCmdUI;
        }

        /// <summary>
        /// Gets a reference to the one and only instance of
        /// <see cref="T:GitHubManager.MainWindow" />.
        /// </summary>
        public static IMainWindow Instance { get; } = new MainWindow();

        /// <summary>
        /// Gets a reference to an instance of an object that implements the
        /// <see cref="T:GitHubManager.IGitHubManagerConfigurationProvider" /> interface.
        /// </summary>
        private static IGitHubManagerConfigurationProvider
            GitHubManagerConfigurationProvider
            => GetGitHubManagerConfigurationProvider.SoleInstance();

        /// <summary>
        /// Gets a reference to an instance of an object that implements the
        /// <see cref="T:GitHubManager.IGitHubSession" /> interface.
        /// </summary>
        private static IGitHubSession Session
            => GetGitHubSession.SoleInstance();

        /// <summary>
        /// Gets or sets a value indicating whether the user is signed in.
        /// </summary>
        public bool IsSignedIn { get; set; }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.</summary>
        /// <param name="e">
        /// A <see cref="T:System.EventArgs" /> that contains the event
        /// data.
        /// </param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (GitHubManagerConfigurationProvider.CurrentConfiguration
                                                  .LoginOnStartup)
                fileLogin.PerformClick();
        }

        private void OnDropDownOpeningViewMenu(object sender, EventArgs e)
            => viewStatusBar.Checked = statusBar.Visible;

        private void OnDropDownOpeningViewToolbarsMenu(object sender,
            EventArgs e)
            => viewNavigateToolbar.Checked = navigateToolBar.Visible;

        private void OnFileExit(object sender, EventArgs e)
            => Close();

        private void OnFileLogin(object sender, EventArgs e)
        {
            using (var dialogBox = MakeNewLoginDialogBox.FromScratch())
            {
                dialogBox.GitHubLoginInfoReceived += OnGitHubLoginInfoReceived;

                IsSignedIn = DialogResult.OK == dialogBox.ShowDialog(this);
            }
        }

        private void OnGitHubAuthenticated(object sender,
            GitHubAuthenticatedEventArgs e)
            => this.InvokeIfRequired(
                new MethodInvoker(
                    async () =>
                    {
                        reposListBindingSource.DataSource = null;
                        reposListBindingSource.DataSource =
                            new BindingList<Repo>(await Presenter.GetRepos());
                    }
                )
            );

        private void OnGitHubLoginInfoReceived(object sender,
            GitHubLoginInfoReceivedEventArgs e) { }

        private void OnToolsOptions(object sender, EventArgs e)
            => Presenter.ConfigureOptions();

        /// <summary>
        /// Handles the <see cref="E:System.Windows.Forms.Application.Idle" /> event raised
        /// by the application.
        /// </summary>
        /// <param name="sender">
        /// Reference to an instance of the object that raised the
        /// event.
        /// </param>
        /// <param name="e">
        /// A <see cref="T:System.EventArgs" /> that contains the event
        /// data.
        /// </param>
        /// <remarks>
        /// This method responds by ensuring that the enabled/disabled state of
        /// controls corresponds to the internal state of the application.
        /// </remarks>
        [Log(AttributeExclude = true)]
        private void OnUpdateCmdUI(object sender, EventArgs e)
            => navigateToolBar.Enabled = false;

        private void OnViewNavigateToolbar(object sender, EventArgs e)
            => navigateToolBar.Visible = !navigateToolBar.Visible;

        private void OnViewStatusBar(object sender, EventArgs e)
            => statusBar.Visible = !statusBar.Visible;
    }
}