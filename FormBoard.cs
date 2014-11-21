using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsUploadApp
{
    public partial class FormBoard : Form
    {
        MySqlDataAdapter mAdapter;
        DataSet mDataSet;

        private int mMouseDownX = 0;
        private int mMouseDownY = 0;

        private int mOps = 0;
        public int mSelectedId { get; set; }
        public String mSelectedName { get; set; }

        public FormBoard()
        {
            InitializeComponent();
        }
        public FormBoard(int val)
        {
            InitializeComponent();
            mOps = val;
        }
        /* 此函数为自动生成 */
        private void LoadData()
        {
            var conn = new MySqlConnection();
            conn.ConnectionString = Properties.Settings.Default.MySqlCon;
            conn.Open();
            mAdapter = new MySqlDataAdapter("select * from tb_1_board", conn);
            mDataSet = new DataSet();
            mAdapter.Fill(mDataSet);
            dataGridView1.DataSource = mDataSet.Tables[0];

            mAdapter.InsertCommand = new MySqlCommand("insert into tb_1_board (id,name,instruction)values(id,@name,@instruction)", conn);
            mAdapter.InsertCommand.Parameters.Add("@name", MySqlDbType.VarChar, 50, "name");
            mAdapter.InsertCommand.Parameters.Add("@instruction", MySqlDbType.VarChar, 255, "instruction");


            mAdapter.UpdateCommand = new MySqlCommand("update tb_1_board set id=id,name=@name,instruction=@instruction  where id=@id", conn);
            mAdapter.UpdateCommand.Parameters.Add("@id", MySqlDbType.Int16, 4, "id");
            mAdapter.UpdateCommand.Parameters.Add("@name", MySqlDbType.VarChar, 50, "name");
            mAdapter.UpdateCommand.Parameters.Add("@instruction", MySqlDbType.VarChar, 255, "instruction");

            mAdapter.DeleteCommand = new MySqlCommand("delete from tb_1_board where id=@id", conn);
            mAdapter.DeleteCommand.Parameters.Add("@id", MySqlDbType.Int16, 4, "id");
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.Columns[0].Visible = false;
            this.btnTest.Visible = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            mAdapter.Update(mDataSet);
        }

        private void menuDeleteClick(object sender, System.EventArgs e)
        {
            int count = dataGridView1.SelectedRows.Count;
            for (int i = 0; i < count; i++)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请先选择需要操作的行");
                    return;
                }
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Delete", menuDeleteClick));

                m.Show(dataGridView1, new Point(mMouseDownX, mMouseDownY));
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (mOps)
            {
                case 0:
                    mAdapter.Update(mDataSet);
                    LoadData();
                    MessageBox.Show("更新完成");
                    break;
                case 1:
                    if (this.dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("请先选择行");
                        return;
                    }
                    this.DialogResult = DialogResult.OK;
                    mSelectedId = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    mSelectedName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    Close();
                    break;
            }
        }

        private void SetNumber(DataGridView dv)
        {
            foreach (DataGridViewRow row in dv.Rows)
            {
                row.HeaderCell.Value = row.Index + 1 + "";
            }
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SetNumber(dataGridView1);
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            mMouseDownX = e.X;
            mMouseDownY = e.Y;
        }
    }
}
