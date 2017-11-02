using System;
using MvvmNano.Forms.MasterDetail;
using Xamarin.Forms;

namespace TutorScout24
{
    public class CustomMasterDetailData : MvvmNanoMasterDetailData
    {
      

        public ImageSource Source
        {
            get;
            set;
        }
           
        public CustomMasterDetailData(string title, ImageSource detailImageSource) : base(title)
        {
            Source = detailImageSource;
            Title = title;

        }
    }
}
