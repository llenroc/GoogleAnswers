#region Copyright

// <copyright file="MainActivity.cs">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Sarthak M/author>
// <date>08/06/2015 06:00:00 PM </date>
// <summary>MainActivity for Android App</summary> 

#endregion

#region Usings

using System;
using Android.App;
using Android.Widget;
using Android.OS;
using GoogleAnswers.Utils;
using Result = GoogleAnswers.Utils.Result;

#endregion

namespace GoogleAnswers
{
	[Activity(Label = "GoogleAnswers", MainLauncher = true, Icon = "@drawable/searchButton",
		ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
	{
		#region Privates

		private Dialog searchDialog;
		private EditText txtSearch;
		private ImageButton searchButtonButton;
		private SearchItemAdapter searchItemAdapter;
		private ListView searchHistoryListView;
		private ProgressDialog progressDialog;

		#endregion

		#region Overrides

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);


			//searchButton
			searchButtonButton = FindViewById<ImageButton>(Resource.Id.searchButton);
			searchButtonButton.Click += imgButton_Click;

			searchHistoryListView = FindViewById<ListView>(Resource.Id.searchHistory);

			//Initialize the search item adapter
			searchItemAdapter = new SearchItemAdapter(this, Resource.Layout.SearchListItem);

			//Assign adapter to listview
			searchHistoryListView.Adapter = searchItemAdapter;

		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Open ShowSearchDialog
		/// </summary>
		private void ShowSearchDialog()
		{

			//Create a dialog box without any title
			searchDialog = new Dialog(this, Android.Resource.Style.ThemeDeviceDefaultDialogNoActionBar);
			searchDialog.SetContentView(Resource.Layout.SearchAlertBox);

			var btnSearch = (Button)searchDialog.FindViewById(Resource.Id.btnSearch);

			txtSearch = (EditText)searchDialog.FindViewById(Resource.Id.txtSearch);


			btnSearch.Click += btnSearch_Click;

			//Show Dialogbox
			searchDialog.Show();
		}

		#endregion

		#region CallBacks

		/// <summary>
		/// On searchButton click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void imgButton_Click(object sender, EventArgs e)
		{
			ShowSearchDialog();
		}



		/// <summary>
		/// On click of  btnSearch
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnSearch_Click(object sender, EventArgs e)
		{
			//Get text from Search text box
			string text = txtSearch.Text;

			//Show error if text is empty
			if (text.Trim() == string.Empty)
			{
				txtSearch.Error = GetString(Resource.String.IsEmpty);
				return;
			}

			//Fetch result from the query
			var searchHttpRequest = new SearchHttpRequestHelper(text);
			searchHttpRequest.OnExecption += searchHttpRequest_OnExecption;
			searchHttpRequest.OnResult += searchHttpRequest_OnResult;
			searchHttpRequest.Run();

			//Dismiss Search dialog box
			searchDialog.Dismiss();

			//Show progress bar
			progressDialog = ProgressDialog.Show(this, GetString(Resource.String.PleaseWait),
				GetString(Resource.String.FetchingResult)
				, false, false);
		}

		/// <summary>
		/// On return of search result
		/// </summary>
		/// <param name="result"></param>
		void searchHttpRequest_OnResult(Result result)
		{
			//Dismiss progress bar
			if (progressDialog != null)
			{
				progressDialog.Dismiss();
			}

			//if result has item then add to adpter or show error.
			if (result.items != null && result.items.Count > 0)
			{
				searchItemAdapter.Add(result);
			}
			else
			{
				RunOnUiThread(() => Toast.MakeText(this, GetString(Resource.String.CannotFetch), ToastLength.Short).Show());
			}
		}

		/// <summary>
		/// On exection while getting search result
		/// </summary>
		/// <param name="ex"></param>
		void searchHttpRequest_OnExecption(Exception ex)
		{
			//Dismiss progress bar
			if (progressDialog != null)
			{
				progressDialog.Dismiss();
			}

			//Toast the error.
			RunOnUiThread(() => Toast.MakeText(this, GetString(Resource.String.CannotConnect), ToastLength.Short).Show());
		} 

		#endregion
	}
}

