using System;
namespace SimpleModelViewPresenter
{
    public interface IViewNote : IView
    {

        string Text { get; set; }
        DateTime? Created { get; set; }
        DateTime? Updated { get; set; }
        bool IsArhive { get; set; }
        event EventHandler<EventArgs> DeleteArhiveNotes;
    }
}
