#region Copyright

// <copyright file="SearchHttpRequestHelper.cs">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Sarthak M/author>
// <date>08/06/2015 06:00:00 PM </date>
// <summary>Helper class for getting the search result</summary> 

#endregion

#region Usings

using System;
using System.Net;
using Newtonsoft.Json;

#endregion

namespace GoogleAnswers.Utils
{
    #region Delegate

    internal delegate void ResultCallBack(Result result);

    internal delegate void ExecptionCallBack(Exception ex);

    #endregion

    /// <summary>
    /// SearchHttpRequestHelper
    /// </summary>
    internal class SearchHttpRequestHelper
    {
        #region Privates

        private WebClient _webClient;
        private readonly string _searchCriteria;
        public event ResultCallBack OnResult;
        public event ExecptionCallBack OnExecption;

        //Uri for Google Search
        private const string UriFormat = "https://www.googleapis.com/customsearch/v1?key={0}&cx=017576662512468239146:omuauf_lfve&q={1}&num=1";

        //Token for Google Search Api
        private const string GoogleToken = "AIzaSyAQrEUd1RaBj0ArQm0D8_hapqD3x545agY";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchCriteria"></param>
        internal SearchHttpRequestHelper(string searchCriteria)
        {
            _searchCriteria = searchCriteria;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the search query
        /// </summary>
        public void Run()
        {
            _webClient = new WebClient();

            string uriStr = String.Format(UriFormat, GoogleToken, _searchCriteria);

            _webClient.DownloadStringCompleted += _webClient_DownloadStringCompleted;
            _webClient.DownloadStringAsync(new Uri(uriStr));
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback on result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //if result has error, raise execption
            if (e.Error != null)
            {
                if (OnExecption != null)
                {
                    OnExecption(e.Error);
                }
            }
            //else, parse and publish the result
            else
            {
                var result = JsonConvert.DeserializeObject<Result>(e.Result);
                if (OnResult != null)
                {
                    OnResult(result);
                }
            }
        }

        #endregion

    }
}