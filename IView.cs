using System;
namespace SimpleModelViewPresenter
{
    public interface IView
    {
        object Key { get; set; }
        string StatusChange { set; }
        bool isDirty { get; set; }
        event EventHandler<EventArgs> Save;
        event EventHandler<EventArgs> New;
        event EventHandler<EventArgs> Previous;
        event EventHandler<EventArgs> Next;
        bool AllowNext { set; }
        bool AllowPrevious { set; }
	bool AllowSave { set; }
    }
}
