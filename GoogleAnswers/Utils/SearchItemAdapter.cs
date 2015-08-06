#region Copyright

// <copyright file="SearchItemAdapter.cs">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Sarthak M/author>
// <date>08/06/2015 06:00:00 PM </date>
// <summary>Adpter for Search history result</summary> 

#endregion

#region Usings

using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

#endregion

namespace GoogleAnswers.Utils
{
    class SearchItemAdapter : ArrayAdapter<Result>
    {
        #region Contructors

        public SearchItemAdapter(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public SearchItemAdapter(Context context, int textViewResourceId)
            : base(context, textViewResourceId)
        {
        }

        public SearchItemAdapter(Context context, int resource, int textViewResourceId)
            : base(context, resource, textViewResourceId)
        {
        }

        public SearchItemAdapter(Context context, int textViewResourceId, Result[] objects)
            : base(context, textViewResourceId, objects)
        {
        }

        public SearchItemAdapter(Context context, int resource, int textViewResourceId, Result[] objects)
            : base(context, resource, textViewResourceId, objects)
        {
        }

        public SearchItemAdapter(Context context, int textViewResourceId, IList<Result> objects)
            : base(context, textViewResourceId, objects)
        {
        }

        public SearchItemAdapter(Context context, int resource, int textViewResourceId, IList<Result> objects)
            : base(context, resource, textViewResourceId, objects)
        {

        }

        #endregion

        #region Overrides

        /// <summary>
        /// GetView
        /// </summary>
        /// <param name="position"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Result result = GetItem(position);

            ViewHolder viewHolder; // view lookup cache stored in tag
            if (convertView == null)
            {
                viewHolder = new ViewHolder();
                LayoutInflater inflater = LayoutInflater.From(Context);
                convertView = inflater.Inflate(Resource.Layout.SearchListItem, parent, false);
                viewHolder.SearchQ = (TextView)convertView.FindViewById(Resource.Id.txtSearchQ);
                viewHolder.SearchA = (TextView)convertView.FindViewById(Resource.Id.txtSearchA);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }
            // Populate the data into the template view using the data object
            viewHolder.SearchQ.Text = result.queries.request[0].searchTerms;
            viewHolder.SearchA.Text = result.items[0].snippet;
            // Return the completed view to render on screen
            return convertView;
        }


        #endregion

        #region ViewHolder Class

        private class ViewHolder : Java.Lang.Object
        {
            public TextView SearchQ;
            public TextView SearchA;
        } 

        #endregion
    }


}