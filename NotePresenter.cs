using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleModelViewPresenter
{
    public class NotePresenter
    {
        private readonly IViewNote viewNote;
        private List<Note> notes;

        private int currentIndex = 0;
        private bool isNew = false;

        private NoteDataSet dataset;
        public NotePresenter(IViewNote viewNote)
        {
            dataset = new NoteDataSet();
            this.viewNote = viewNote;
            Initialize();
        }



        private void Initialize()
        {

            viewNote.New += New;
            viewNote.Save += Save;
            viewNote.Next += Next;
            viewNote.Previous += Previous;
            viewNote.DeleteArhiveNotes += DeleteArhiveNotes;

            LoadNotes();
        }

        private void BlankNote()
        {
            viewNote.Text = "";
            viewNote.Key = 0;
            viewNote.Created = null;
            viewNote.IsArhive = false;
            viewNote.Updated = null;
            currentIndex = notes.Count;
            viewNote.StatusChange = "Новая запись." + CountNotes();
            isNew = true;
	    NavigateRule();
        }
        private string CountNotes() => $" Всего записей: {notes.Count}";
        private void New(object sender, EventArgs e)
        {
            BlankNote();
        }
        private Note Get(object key)
        {
            return notes.Where(note => note.Id == (int)key).SingleOrDefault();
        }
        private void Save(object sender, EventArgs e)
        {
            var note = Get(viewNote.Key);
            if (isNew)
                note = new Note();

            note.Text = viewNote.Text;
            note.IsArhive = viewNote.IsArhive;

            if (isNew)
            {
                dataset.Insert(note);
                isNew = false;
                notes.Add(note);
            }
            else
                dataset.Update(note);

            UpdateView(note);
            viewNote.StatusChange = $"запись {note.Id} сохранена";
        }
        private void UpdateView(Note note)
        {
            NavigateRule();
            viewNote.Created = note.Created;
            viewNote.IsArhive = note.IsArhive;
            viewNote.Updated = note.Updated;
            viewNote.Text = note.Text;
            viewNote.Key = note.Id;
	    viewNote.AllowSave = isNew;	
            if (isNew)
                viewNote.StatusChange = $"новая запись";
            else
                viewNote.StatusChange = $"запись {currentIndex + 1} из {notes.Count}";
        }

        private void NavigateRule()
        {
            if (notes.Count < 1)
            {
                viewNote.AllowPrevious = false;
                viewNote.AllowNext = false;
            }
            else
            {
                if (currentIndex <= 0)
                {
                    viewNote.AllowPrevious = false;
                    viewNote.AllowNext = true;
                }
                if (currentIndex >= notes.Count - 1)
                {
                    viewNote.AllowPrevious = true;
                    viewNote.AllowNext = false;
                }

                if (currentIndex > 0 && currentIndex < notes.Count - 1)
                {
                    viewNote.AllowPrevious = true;
                    viewNote.AllowNext = true;
                }
            }
        }

        private void Next(object sender, EventArgs e)
        {
            if (currentIndex < (notes.Count - 1))
            {
                currentIndex++;
                isNew = false;
                viewNote.StatusChange = $"запись {currentIndex + 1} из {notes.Count}";
                UpdateView(notes[currentIndex]);
            }

        }
        private void Previous(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                isNew = false;
                UpdateView(notes[currentIndex]);
            }

        }
        private void DeleteArhiveNotes(object sender, EventArgs e)
        {
            foreach (var note in notes)
                if (note.IsArhive)
                    dataset.Delete(note.Id);
            LoadNotes();
        }

        private void LoadNotes()
        {
            notes = new List<Note>(dataset.List());
            if (notes.Count > 0)
            {
                currentIndex = 0;
                UpdateView(notes[currentIndex]);

            }
            else
                BlankNote();
        }
    }
}
