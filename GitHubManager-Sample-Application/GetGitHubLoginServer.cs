﻿using PostSharp.Patterns.Diagnostics;
using System.Diagnostics;

namespace GitHubManager

{
    /// <summary>
    /// Provides access to the one and only instance of the object that
    /// implements the <see cref="T:GitHubManager.IGitHubLoginServer" /> interface.
    /// </summary>
    public static class GetGitHubLoginServer
    {
        /// <summary>
        /// Initializes static data or performs actions that need to be performed once only
        /// for the <see cref="T:GitHubManager.GetGitHubLoginServer" />
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance being
        /// created or before any static members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static GetGitHubLoginServer() { }

        /// <summary>
        /// Obtains access to the sole instance of the object that implements the
        /// <see cref="T:GitHubManager.IGitHubLoginServer" /> interface, and returns a
        /// reference to it.
        /// </summary>
        /// <returns>
        /// Reference to the one, and only, instance of the object that
        /// implements the <see cref="T:GitHubManager.IGitHubLoginServer" /> interface.
        /// </returns>
        [DebuggerStepThrough]
        [return: NotLogged]
        public static IGitHubLoginServer SoleInstance()
            => GitHubLoginServer.Instance;
    }
}