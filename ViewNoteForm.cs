using System;
using System.Windows.Forms;
using System.Drawing;
namespace SimpleModelViewPresenter
{
    public class ViewNoteForm : Form, IViewNote
    {
        private NotePresenter presenter;
        TableLayoutPanel notesLayout = new TableLayoutPanel();
        Label lText = new Label();
        TextBox text = new TextBox();
        Label lCreated = new Label();
        Label created = new Label();
        Label lupdated = new Label();
        Label updated = new Label();
        Label lArhive = new Label();
        CheckBox isArhive = new CheckBox();
        Button buttonSave = new Button();

        Button buttonAdd = new Button();
        Button buttonDeleteOld = new Button();
        Button buttonNext = new Button();

        Button buttonPrevious = new Button();
        private string Caption = "Записная книжка";
        Label listStatus = new Label();
        public ViewNoteForm()
        {
            lText.Text = "Заметка:";
            lCreated.Text = "Создано:";
            lupdated.Text = "Обновлено:";
            lArhive.Text = "Архив:";
            text.Multiline = true;
            text.Size = new Size(300, 60);


            this.Text = Caption;

            presenter = new NotePresenter(this);

            Controls.Add(notesLayout);
            notesLayout.Dock = DockStyle.Fill;
            notesLayout.RowCount = 8;
            notesLayout.ColumnCount = 1;

            var textLayout = CreateHorizontalPanel();
            textLayout.Controls.Add(lText);
            textLayout.Controls.Add(text);
            notesLayout.Controls.Add(textLayout);

            var isArhivLayout = CreateHorizontalPanel();
            isArhivLayout.Controls.Add(lArhive);
            isArhivLayout.Controls.Add(isArhive);
            notesLayout.Controls.Add(isArhivLayout);

            var createdLayout = CreateHorizontalPanel();
            createdLayout.Controls.Add(lCreated);
            createdLayout.Controls.Add(created);
            notesLayout.Controls.Add(createdLayout);




            var updatedLayout = CreateHorizontalPanel();
            updatedLayout.Controls.Add(lupdated);
            updatedLayout.Controls.Add(updated);
            notesLayout.Controls.Add(updatedLayout);

            var buttonsLayout = CreateHorizontalPanel();

            buttonsLayout.Controls.Add(buttonAdd);

            buttonsLayout.Controls.Add(buttonSave);
            buttonsLayout.Controls.Add(buttonDeleteOld);
            buttonsLayout.Controls.Add(buttonPrevious);
            buttonsLayout.Controls.Add(buttonNext);
            notesLayout.Controls.Add(buttonsLayout);

            var statusLayout = CreateHorizontalPanel();
            statusLayout.Controls.Add(listStatus);
            listStatus.Text = "состояние";
            listStatus.BackColor = Color.LightYellow;

            listStatus.AutoSize = false;

            listStatus.Dock = DockStyle.Fill;
            notesLayout.Controls.Add(listStatus);
	    
            buttonSave.Text = "Сохранить";
            buttonSave.Click += (o, e) =>
            {
                if (string.IsNullOrWhiteSpace(text.Text))
                {
                    MessageBox.Show("Введите текст заметки", "Заметки",
MessageBoxButtons.OK, MessageBoxIcon.Error);

                    text.Focus();
                    return;
                }
                if (Save != null)
                    Save(o, e);
            };
            buttonAdd.Text = "Добавить";
            buttonAdd.Click += (o, e) => { if (New != null) New(o, e); text.Focus();};
            buttonNext.Text = "Далее";
            buttonNext.Click += (o, e) => { if (Next != null) Next(o, e); };
            buttonPrevious.Text = "Назад";
            buttonPrevious.Click += (o, e) => { if (Previous != null) Previous(o, e); };
            buttonDeleteOld.Text = "Удалить \nархивные";
            buttonDeleteOld.Click += (o, e) =>
            {
                if (MessageBox.Show("Удалить архивные записи?", "Удаление", MessageBoxButtons.YesNo)
                   != DialogResult.Yes)
                    return;
                if (DeleteArhiveNotes != null)
                    DeleteArhiveNotes(o, e);
            };
	   
            text.TextChanged += AllowedSave;
	    isArhive.CheckedChanged += AllowedSave;
            notesLayout.MinimumSize = new Size(480,300);
		//foreach(RowStyle rowStyle in notesLayout.RowStyles)
			//rowStyle.SizeType = SizeType.AutoSize;
	    notesLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
	    notesLayout.AutoSize = true;
	    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
	    this.AutoSize = true;
        }
	private void AllowedSave(object o,EventArgs e)
	{
		AllowSave = true;
	}
        private FlowLayoutPanel CreateHorizontalPanel()
        {
            var panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.Dock = DockStyle.Fill;
            return panel;
        }
        string IViewNote.Text
        {
            get
            {
                return text.Text;
            }
            set
            {
                text.Text = value;
            }
        }
        public DateTime? Updated
        {
            get => throw new NotImplementedException(); set => updated.Text = value.ToString();
        }
        public bool IsArhive { get => isArhive.Checked; set => isArhive.Checked = value; }
        public string StatusChange { set => listStatus.Text = value; }
        public bool isDirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime? IViewNote.Created { get => throw new NotImplementedException(); set => created.Text = value.ToString(); }
        public object Key { get => key; set => key = value; }
        public bool AllowNext { set => buttonNext.Enabled = value; }
	public bool AllowSave { set => buttonSave.Enabled = value; }
        public bool AllowPrevious { set => buttonPrevious.Enabled = value; }

        private object key;

        public event EventHandler<EventArgs> Save;
        public event EventHandler<EventArgs> New;
        public event EventHandler<EventArgs> Previous;
        public event EventHandler<EventArgs> Next;
        public event EventHandler<EventArgs> DeleteArhiveNotes;
    }
}
