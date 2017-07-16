using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace SimpleModelViewPresenter
{
    public class NoteDataSet : IDataManager<Note>
    {
        private string dataFile = "notes.xml";
        private DataTable table = new DataTable("notes");
        private DataSet dataset = new DataSet("dataset");
        private void Init()
        {
            table.Columns.Add(nameof(Note.Id), typeof(int)).AutoIncrement = true;
            table.Columns[nameof(Note.Id)].AutoIncrementSeed = 1;
            table.Columns[nameof(Note.Id)].AutoIncrementStep = 1;
            table.Columns.Add(nameof(Note.Created), typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add(nameof(Note.Updated), typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add(nameof(Note.Text), typeof(string));
            table.Columns.Add(nameof(Note.IsArhive), typeof(bool));
            dataset.Tables.Add(table);
            if (File.Exists(dataFile))
                dataset.ReadXml(dataFile);
        }

        public IList<Note> List()
        {
            var list = new List<Note>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Note()
                {
                    Id = (int)row[nameof(Note.Id)],
                    Created = ConvertToNullableDateTime(row[nameof(Note.Created)]),
                    Updated = ConvertToNullableDateTime(row[nameof(Note.Updated)]),
                    Text = row[nameof(Note.Text)]?.ToString(),
                    IsArhive = (bool)row[nameof(Note.IsArhive)]

                });
            }
            return list;
        }
        private DateTime? ConvertToNullableDateTime(object cell)
        {
            if (cell is DateTime)
                return (DateTime?)cell;
            else
                return null;
        }

        public void Insert(Note data)
        {
            var inserted = DateTime.Now;
            var addedRow = table.Rows.Add(new object[] { null, inserted, data.Updated, data.Text, data.IsArhive });
            dataset.WriteXml(dataFile);
            data.Created = inserted;
            data.Id = (int)addedRow["Id"];
        }

        public void Update(Note data)
        {
            var row = table.Select($"Id = {data.Id}");
            if (row == null)
                return;
            row[0][nameof(Note.IsArhive)] = data.IsArhive;
            row[0][nameof(Note.Text)] = data.Text;
            var updated = DateTime.Now;
            row[0][nameof(Note.Updated)] = updated;
            dataset.WriteXml(dataFile);
            data.Updated = updated;
        }
        public void Delete(object key)
        {
            var row = table.Select($"Id = {key}");
            if (row == null)
                return;
            table.Rows.Remove(row[0]);
            dataset.WriteXml(dataFile);
        }

        public NoteDataSet()
        {
            Init();
        }
    }
}
