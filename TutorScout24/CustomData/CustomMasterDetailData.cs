using System;
using MvvmNano.Forms.MasterDetail;
using Xamarin.Forms;

namespace TutorScout24.CustomData
{
    public class CustomMasterDetailData : MvvmNanoMasterDetailData
    {
      

        public string ImageCode
        {
            get;
            set;
        }
           
        public CustomMasterDetailData(string title, string imageCode) : base(title)
        {
            ImageCode = imageCode;
            Title = title;

        }
    }
}
