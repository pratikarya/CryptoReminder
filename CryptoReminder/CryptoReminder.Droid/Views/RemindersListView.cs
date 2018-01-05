using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CryptoReminder.Droid.Common;
using CryptoReminder.Core.ViewModels;
using Android.Support.Design.Widget;
using System.ComponentModel;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace CryptoReminder.Droid.Views
{
    [Activity(Label = "")]
    public class RemindersListView : BaseActivity<RemindersListViewModel>
    {
        Toolbar _toolbar;
        TextView _toolBarTitle;
        FloatingActionButton _fabAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.reminders_list_view);

            GetReferences();

            CreateUi();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.LoadRemindersCommand.Execute(null);
        }

        private void CreateUi()
        {
            _toolBarTitle.Text = ViewModel.Current.MarketName;
            _fabAdd.Click += _fabAdd_Click;
        }

        private void GetReferences()
        {
            _fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAddReminder);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolBarTitle = FindViewById<TextView>(Resource.Id.toolbarTitle);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void _fabAdd_Click(object sender, EventArgs e)
        {
            ViewModel.NewReminderCommand.Execute(null);
        }
    }
}