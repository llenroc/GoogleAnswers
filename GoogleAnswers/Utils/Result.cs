#region Copyright

// <copyright file="Result.cs">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Sarthak M/author>
// <date>08/06/2015 06:00:00 PM </date>
// <summary>Result Json Object</summary> 

#endregion

#region Using

using System.Collections.Generic;

#endregion

namespace GoogleAnswers.Utils
{
    #region Result

    class Result
    {
        public string kind { get; set; }
        public Url url { get; set; }
        public Queries queries { get; set; }
        public SearchInformation searchInformation { get; set; }
        public List<Item> items { get; set; }
    } 

    #endregion

    #region ResultItems

    public class Url
    {
        public string type { get; set; }
        public string template { get; set; }
    }

    public class NextPage
    {
        public string title { get; set; }
        public string totalResults { get; set; }
        public string searchTerms { get; set; }
        public int count { get; set; }
        public int startIndex { get; set; }
        public string inputEncoding { get; set; }
        public string outputEncoding { get; set; }
        public string safe { get; set; }
        public string cx { get; set; }
    }

    public class Request
    {
        public string title { get; set; }
        public string totalResults { get; set; }
        public string searchTerms { get; set; }
        public int count { get; set; }
        public int startIndex { get; set; }
        public string inputEncoding { get; set; }
        public string outputEncoding { get; set; }
        public string safe { get; set; }
        public string cx { get; set; }
    }

    public class Queries
    {
        public List<NextPage> nextPage { get; set; }
        public List<Request> request { get; set; }
    }



    public class SearchInformation
    {
        public double searchTime { get; set; }
        public string formattedSearchTime { get; set; }
        public string totalResults { get; set; }
        public string formattedTotalResults { get; set; }
    }

    public class CseImage
    {
        public string src { get; set; }
    }

    public class CseThumbnail
    {
        public string width { get; set; }
        public string height { get; set; }
        public string src { get; set; }
    }

    public class Pagemap
    {
        public List<CseImage> cse_image { get; set; }
        public List<CseThumbnail> cse_thumbnail { get; set; }
    }

    public class Label
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string label_with_op { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string title { get; set; }
        public string htmlTitle { get; set; }
        public string link { get; set; }
        public string displayLink { get; set; }
        public string snippet { get; set; }
        public string htmlSnippet { get; set; }
        public string cacheId { get; set; }
        public string formattedUrl { get; set; }
        public string htmlFormattedUrl { get; set; }
        public Pagemap pagemap { get; set; }
        public List<Label> labels { get; set; }
    } 

    #endregion

}