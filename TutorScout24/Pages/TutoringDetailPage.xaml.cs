using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorScout24.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TutoringDetailPage
	{
		public TutoringDetailPage ()
		{
			InitializeComponent ();
		    var label = new Label
		    {
		        Text = "\uf08e",
                FontFamily = "fontawesome",
		        HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
   
		    };
		    UserGrid.Children.Add(label, 1, 0);



		}

	    protected override void OnBindingContextChanged()
	    {
	        base.OnBindingContextChanged();
	        try
	        {
                var gr = new TapGestureRecognizer();
	            gr.Tapped += new SingleClick(ViewModel.ToProfile).Click;
                UserFrame.GestureRecognizers.Add(gr);
                ViewModel.AddToolBarItem();
	        }
	        catch (Exception ex)
	        {

	        }
        }
	}
}