﻿using System.Diagnostics;

namespace GitHubManager
{
    /// <summary>
    /// Defines the publicly-exposed methods and properties of a POCO that
    /// maintains information about each Repository.
    /// </summary>
    public interface IRepo
    {
        /// <summary>
        /// Gets or sets the Clone URL (URL ending in <c>.git</c>) from which the
        /// repository can be cloned.
        /// </summary>
        string CloneUrl { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary> Gets or sets the repository's description. </summary>
        string Description { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary> Gets or sets the name of the repository. </summary>
        string Name { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }
    }

    /// <summary> POCO to encapsulate a GitHub repository. </summary>
    public class Repo : IRepo
    {
        /// <summary>
        /// Gets or sets the Clone URL (URL ending in <c>.git</c>) from which the
        /// repository can be cloned.
        /// </summary>
        public string CloneUrl { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary> Gets or sets the repository's description. </summary>
        public string Description { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary> Gets or sets the name of the repository. </summary>
        public string Name { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }
    }
}