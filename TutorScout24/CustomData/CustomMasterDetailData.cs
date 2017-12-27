using MvvmNano.Forms.MasterDetail;

namespace TutorScout24.CustomData
{
    /// <summary>
    /// Data Object for DetailPages in Navigation.
    /// </summary>
    public class CustomMasterDetailData : MvvmNanoMasterDetailData
    {
        public CustomMasterDetailData(string title, string imageCode) : base(title)
        {
            ImageCode = imageCode;
            Title = title;
        }


        public string ImageCode { get; set; }
    }
}