using MvvmNano;
using TutorScout24.ViewModels;

namespace TutorScout24.CustomData
{
    /// <summary>
    ///     Data Transfer Object that is sent with a Change Mode Event.
    /// </summary>
    internal class ChangeModeMessage : IMessage
    {
        public ChangeModeMessage(MasterDetailViewModel.Mode mode)
        {
            NewMode = mode;
        }

        public MasterDetailViewModel.Mode NewMode { get; set; }
    }
}