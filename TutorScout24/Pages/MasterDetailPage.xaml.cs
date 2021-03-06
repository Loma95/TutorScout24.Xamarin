﻿using System;
using System.Collections.Generic;
using TutorScout24.ViewModels;
using MvvmNano.Forms.MasterDetail;
using Xamarin.Forms;
using System.Diagnostics;
using NControl.Abstractions;
using NGraphics;
using TutorScout24.CustomData;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using System.Threading.Tasks;
using TutorScout24.Controls;
using MvvmNano.Forms;

namespace TutorScout24.Pages
{
    public partial class MasterDetailPage
    {

        public MasterDetailPage()
        {
            InitializeComponent();



            AddDetailData<FeedTabViewModel>(new CustomMasterDetailData("Feed", "\xf09e"));
            AddDetailData<TutorialsViewModel>(new CustomMasterDetailData("Tutorien", "\xf212"));
            AddDetailData<MessageViewModel>(new CustomMasterDetailData("Chats", "\xf0e6"));
            AddDetailData<ProfileViewModel>(new CustomMasterDetailData("Profil", "\xf007"));
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe<DialogMessage>(this, (object arg1, DialogMessage arg2) =>
            {
                DisplayAlert(arg2.Header, arg2.Text, "ok");
            });

            this.ToolbarItems.Clear();
            this.ToolbarItems.Add(switchI);
            switchI.SetBinding(ToolbarItem.CommandProperty, nameof(MasterDetailViewModel.ChangeCommand));
        }


        private ToolbarItem switchI = new ToolbarItem
        {
            Text = "\uf0ec"
        };


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MvvmNanoIoC.Resolve<IMessenger>().Unsubscribe<DialogMessage>(this);
        }


        private RelativeLayout _headerLayout = new RelativeLayout
        {
            HeightRequest = 100,
            WidthRequest = 500
        };

        private Image profileImage = new Image
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = 5,
            Source = "icon.png"
        };


        protected override void DetailSet(MvvmNanoMasterDetailData lastDetailData, MvvmNanoMasterDetailData newDetailData, Page page)
        {
            base.DetailSet(lastDetailData, newDetailData, page);
            MvvmNanoNavigationPage navi = (MvvmNanoNavigationPage)page.Parent;
            navi.BarBackgroundColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }




        /// <summary>
        /// We hook up our own item template here.
        /// Our item template will add the background color from our <see cref="CustomMasterDetailData"/> to each cell.
        /// </summary>
        /// <returns></returns>
        protected override DataTemplate GetItemTemplate()
        {
            return new DataTemplate(() =>
            {
                //root Layout for each cell
                RelativeLayout relLayout = new RelativeLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,

                };

                //label for each cell
                Label titleLabel = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center


                };

                //icon for each cell
                Label detailImage = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    FontFamily = "fontawesome"
                };


                //Bind the values from CustomMasterDetailData to the views
                titleLabel.SetBinding(Label.TextProperty, nameof(CustomMasterDetailData.Title));
                detailImage.SetBinding(Label.TextProperty, nameof(CustomMasterDetailData.ImageCode));

                //fill the RelativeLayout with the views
                AddToLayoutWithConstraints(detailImage, 20, 0, relLayout);
                AddToLayoutWithConstraints(titleLabel, 0, 0, relLayout);


                return new ViewCell
                {

                    View = relLayout
                };
            });
        }

        /// <summary>
        /// Adds View to layout with constraints.
        /// </summary>
        /// <param name="view">View</param>
        /// <param name="xCons">X Constraint</param>
        /// <param name="yCons">Y Constraint</param>
        /// <param name="l">RelativeLayout where you want to add the View  </param>
        private void AddToLayoutWithConstraints(View view, double xCons, double yCons, RelativeLayout l)
        {
            l.Children.Add(view,
                           Constraint.Constant(xCons),
                           Constraint.Constant(yCons),
               Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height;
                }));
        }


        /// <summary>
        /// Creates the master page.
        /// </summary>
        /// <returns>The master page.</returns>
        protected override Page CreateMasterPage()
        {
            
            AddToLayoutWithConstraints(profileImage, 0, 0, _headerLayout);
            DetailListView.Header = _headerLayout;


            //Call base Constructor and set MasterPage Title
            Page p = base.CreateMasterPage();

     
            //this title is the name of the IOS Button
            p.Title = "Menü";
            return p;
        }

    }
}
