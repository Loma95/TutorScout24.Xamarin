using System;
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

namespace TutorScout24.Pages
{
    public partial class MasterDetailPage
    {

        public MasterDetailPage()
        {
            InitializeComponent();

           
            AddDetailData<SearchWeatherViewModel>(new CustomMasterDetailData("Feed", ImageSource.FromResource("TutorScout24.Resources.icons8-activity-feed.png")));
            AddDetailData<TutorialsViewModel>(new CustomMasterDetailData("Tutorien", ImageSource.FromResource("TutorScout24.Resources.icons8-classroom.png")));
            AddDetailData<CurrentLocationWeatherViewModel>(new CustomMasterDetailData("Nachrichten", ImageSource.FromResource("TutorScout24.Resources.icons8-message.png")));
            AddDetailData<ProfileViewModel>(new CustomMasterDetailData("Profil", ImageSource.FromResource("TutorScout24.Resources.icons8-customer.png")));
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe<DialogMessage>(this, (object arg1, DialogMessage arg2) =>
            {
                DisplayAlert("Alert", arg2.Text, "ok");
            });

           
        }


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
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Source = ImageSource.FromResource("TutorScout24.Resources.Placeholder_person.png")
        };


        /// <summary>
        /// Gets the user info.
        /// </summary>
        private async Task<UserInfos> GetUserInfo()
        {
            return await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetUserInfo();

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
                Image detailImage = new Image
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };


                //Bind the values from CustomMasterDetailData to the views
                titleLabel.SetBinding(Label.TextProperty, nameof(CustomMasterDetailData.Title));
                detailImage.SetBinding(Image.SourceProperty, nameof(CustomMasterDetailData.Source));

                //fill the RelativeLayout with the views
                AddToLayoutWithConstraints(detailImage,20,0,relLayout);
                AddToLayoutWithConstraints(titleLabel,0,0,relLayout);


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
        private void AddToLayoutWithConstraints(View view,double xCons,double yCons,RelativeLayout l){
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
        /// Adds the toggle button to tool bar.
        /// </summary>
        private void AddToggleButtonToToolBar(){

           /* MySwitch switchI = new MySwitch();
           
            this.ToolbarItems.Add(switchI);
         

            switchI.Clicked += async (o, i) =>
            {
                UserInfos userI = await GetUserInfo();
                MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage(userI.UserCount.ToString()));
            };*/

        }

        /// <summary>
        /// Creates the master page.
        /// </summary>
        /// <returns>The master page.</returns>
        protected override Page CreateMasterPage()
        {
            AddToggleButtonToToolBar();

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
