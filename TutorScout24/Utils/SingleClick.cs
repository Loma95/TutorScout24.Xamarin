using System;
using System.Threading.Tasks;

namespace TutorScout24.Utils
{
    public class SingleClick  
{  
    bool hasClicked;    
    Action < object, EventArgs > _setClick;  
    public SingleClick(Action < object, EventArgs > setClick)  
    {  
        _setClick = setClick;  
    }
  
    public void Click(object s, EventArgs e)   
    {  
        if (!hasClicked)  
        {  
            _setClick(s, e);  
            hasClicked = true;  
        }  
        Reset();  
    }  
    async void Reset()   
    {  
        await Task.Delay(100);  
        await Task.Run(new Action(() => hasClicked = false));  
    }  
} 
}
