using System;
using System.Collections.Generic;
using TutorScout24.ViewModels;
using MvvmNano.Forms.MasterDetail;
using Xamarin.Forms;
using System.Diagnostics;
using NControl.Abstractions;
using NGraphics;
using TutorScout24.CustomData;

namespace TutorScout24.Pages
{
    public partial class MasterDetailPage
    {

        public MasterDetailPage()
        {
            InitializeComponent();

            AddDetailData<SearchWeatherViewModel>(new CustomMasterDetailData("Feed", ImageSource.FromResource("TutorScout24.Resources.icons8-marker.png")));
            AddDetailData<CurrentLocationWeatherViewModel>(new CustomMasterDetailData("Nachrichten", ImageSource.FromResource("TutorScout24.Resources.icons8-message.png")));

        }


        //TODO: Use NGraphics

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
            var SwitchItem = new ToolbarItem
            {
                Text = "Switch Mode",
                Order = ToolbarItemOrder.Primary

            };
            this.ToolbarItems.Add(SwitchItem);
        }

        /// <summary>
        /// Creates the master page.
        /// </summary>
        /// <returns>The master page.</returns>
        protected override Page CreateMasterPage()
        {
            AddToggleButtonToToolBar();

           // AddToLayoutWithConstraints(profileImage,0,0,_headerLayout);


            var t = new NControlView
            {
                DrawingFunction = (canvas, rect) => {
                    canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
                    canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
                }
            };
            AddToLayoutWithConstraints(profileImage,0,0,_headerLayout);
            DetailListView.Header = _headerLayout;

            //Call base Constructor and set MasterPage Title
            Page p = base.CreateMasterPage();
            //this title is the name of the IOS Button
            p.Title = "Menü";
            return p;
        }

    }
}
