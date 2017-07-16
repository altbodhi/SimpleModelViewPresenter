using System;
namespace SimpleModelViewPresenter
{
    public class Note
    {
        public int Id { get; set; }
        private string text;
        private DateTime? created;
        private DateTime? updated;
        private bool isArhive;

        public bool IsArhive { get => isArhive; set => isArhive = value; }
        public DateTime? Updated { get => updated; set => updated = value; }
        public DateTime? Created { get => created; set => created = value; }
        public string Text { get => text; set => text = value; }
        public override string ToString()
        {
            return string.Format("[Note: Id={0}, IsArhive={1}, Updated={2}, Created={3}, Text={4}]", Id, IsArhive, Updated, Created, Text);
        }
    }
}
