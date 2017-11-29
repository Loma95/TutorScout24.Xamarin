using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmNano;
using TutorScout24.ViewModels;

namespace TutorScout24.CustomData
{
    class ChangeModeMessage : IMessage
    {
        public MasterDetailViewModel.Mode newMode { get; set; }

        public ChangeModeMessage(MasterDetailViewModel.Mode mode)
        {
            this.newMode = mode;
        }
    }
}
