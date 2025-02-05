﻿using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace GitHubManager
{
    /// <summary> Contains information about an authorization request. </summary>
    public class GitHubAuthorizationRequestInfo
    {
        /// <summary> Gets or sets the body of the request. </summary>
        public string Body { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary> Gets or sets the authorization code. </summary>
        public string code { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary> Gets or sets the authorization state. </summary>
        public string state { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        /// <summary>
        /// Gets or sets the URL of the request, as an instance of
        /// <see cref="T:System.Uri" />.
        /// </summary>
        public Uri Url { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [return: NotLogged]
        public static GitHubAuthorizationRequestInfo FromRequest(
            [NotLogged] HttpListenerRequest request
        )
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Url.AbsoluteUri))
                return default;
            if (string.IsNullOrWhiteSpace(request.Url.Query)) return default;

            var result = request.Url.Query.To<GitHubAuthorizationRequestInfo>();

            result.Url = request.Url;
            result.Body = GetBody(request);

            return result;
        }

        private static string GetBody(HttpListenerRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = string.Empty;

            if (!request.HasEntityBody) return result;

            try
            {
                using (var bodyStream = request.InputStream)
                using (var reader = new StreamReader(
                           bodyStream, request.ContentEncoding
                       ))
                    result = reader.ReadToEnd();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }
    }
}