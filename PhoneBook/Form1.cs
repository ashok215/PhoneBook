using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        
        public Form1()
        {
            InitializeComponent();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = db.Phones;
            dataGridView1.Columns["dbname"].Visible = true;
            dataGridView1.Columns["dbnumber"].Visible = true;
            dataGridView1.Columns["Name1"].Visible = false;
            dataGridView1.Columns["Number"].Visible = false;
            button1.Enabled = false;
      
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // currentView = View.Insert;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns["dbName"].Visible = false;
            dataGridView1.Columns["dbNumber"].Visible = false;
            dataGridView1.Columns["Name1"].Visible = true;
            dataGridView1.Columns["Number"].Visible = true;
            

            button1.Enabled = true;
            dataGridView1.Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
        

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Phone[] newNumbers = new Phone[dataGridView1.RowCount - 1];
            for (int i = 0; i < newNumbers.Length; i++)
            {
                newNumbers[i] = new Phone();
                newNumbers[i].ContactName = dataGridView1[3, i].Value.ToString();
                newNumbers[i].ContactNumber = dataGridView1[4, i].Value.ToString();
                db.Phones.InsertOnSubmit(newNumbers[i]);
            }
            db.SubmitChanges();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.database1DataSet.Table);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            var dbquery =
                from phone in db.Phones
                where (phone.ContactName.Contains(textBox1.Text) ||
                phone.ContactNumber.Contains(textBox1.Text))
                select phone;

            int i = 0;
            foreach (Phone p in dbquery)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = p.Id;
                dataGridView1[1, i].Value = p.ContactName;
                dataGridView1[2, i].Value = p.ContactNumber;
                i++;
            }
        }
    }
}
