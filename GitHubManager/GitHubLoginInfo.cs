﻿using System;

namespace GitHubManager
{
    /// <summary>
    /// Contains information needed during the process of logging in to GitHub.
    /// </summary>
    public class GitHubLoginInfo
    {
        /// <summary>
        /// Gets or sets a string containing the ID of the client.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets a string containing a comma-separated list of scopes.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets a string containing a state value.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Parses the query string of the specified <paramref name="url" /> and returns a
        /// new instance of <see cref="T:GitHubManager.GitHubLoginInfo" /> whose properties
        /// are initialized with the results.
        /// </summary>
        /// <param name="url">(Required.) String that contains a URL that begins with the </param>
        /// <returns>
        /// Reference to an instance of
        /// <see cref="T:GitHubManager.GitHubLoginInfo" /> whose properties have been
        /// initialized with the query string values.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// Thrown if the required parameter,
        /// <paramref name="url" />, is passed a blank or <see langword="null" /> string
        /// for a value.
        /// </exception>
        public static GitHubLoginInfo FromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(
                    "Value cannot be null or whitespace.", nameof(url)
                );

            if (!url.StartsWith(GitHubUrls.GitHubLoginAuthorizeUrlStart))
                return default;

            var values = new Uri(url).ParseQueryString();
            if (values == null || values.Count == 0)
                return default;

            return new GitHubLoginInfo
            {
                ClientId = values["client_id"],
                Scope = values["scope"],
                State = values["state"]
            };
        }
    }
}