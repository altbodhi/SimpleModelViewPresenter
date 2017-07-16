using NUnit.Framework;
using System;
namespace SimpleModelViewPresenter
{
    [TestFixture()]
    public class NUnitTests
    {
        [Test()]
        public void TestNoteDataSet()
        {
            var ds = new NoteDataSet();
            var note = new Note();
            note.Text = "Привет";

            ds.Insert(note);
            Console.WriteLine(note);
            foreach (var n in ds.List())
                Console.WriteLine(n);
            // ds.Delete(ds.List()[0].Id);

        }
    }
}
