using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NHibernateGenLib.Base;
using NHibernateGenLib.Domain.AppUpload;

using WindowsFormsUploadApp.Method;
using System.IO;
using Iteedee.ApkReader;

namespace WindowsFormsUploadApp
{
    public partial class FormApp : Form
    {
        public FormApp()
        {
            InitializeComponent();
        }

        private String mUploadPath = "";
        private String mLastUploadFilePath = "";
        private int mMouseDownX = 0;
        private int mMouseDownY = 0;

        private void FormApp_Load(object sender, EventArgs e)
        {
            mLastUploadFilePath = ConfDao.QLastUploadFilePath();
            mUploadPath = ConfDao.QUploadPath();
            btnTest.Visible = false;
            this.dataGridView1.AllowUserToAddRows = false;
            LoadData();
        }

        private void SetRowNum(DataGridView dg)
        {
            foreach (DataGridViewRow row in dg.Rows)
            {
                row.HeaderCell.Value = row.Index + 1 + "";
                row.Height = 40;
                row.ReadOnly = true;
            }
        }

        private void LoadData()
        {
            DataGridView dg = dataGridView1;
            int count = dg.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dg.Rows.RemoveAt(0);
            }
            var list = AppDao.QAll();
            for (int i = 0; i < list.Count; i++)
            {
                var app = list.ElementAt(i);
                Stream s = File.Open(app.IconPath, FileMode.Open);
                Image img = Image.FromStream(s);
                s.Close();
                String size = app.FileSize / 1024  + "KB";
                if (app.FileSize > 1024 * 1024)
                {
                    size = (app.FileSize * 1.0 / 1024 / 1024).ToString("#.##") + "M";
                }
                dg.Rows.Add(new Object[] { app.Id, app.BoardName, img, app.Pkg, size });
            }
            SetRowNum(dg);
        }
        private void boardSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBoard().ShowDialog();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(mUploadPath))
            {
                MessageBox.Show("目录已经不存在,请重新选择");
                return;
            }
            if (mUploadPath.Length < 1)
            {
                MessageBox.Show("请先设置上传路径");
                return;
            }
            var form = new FormBoard(1);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int boardId = form.mSelectedId;
                String boardName = form.mSelectedName;
                var dig = new OpenFileDialog();
                dig.Filter = "apk|*.apk";
                if (mLastUploadFilePath.Length > 0) dig.InitialDirectory = Path.GetDirectoryName(mLastUploadFilePath);
                if (dig.ShowDialog() == DialogResult.OK)
                {
                    mLastUploadFilePath = dig.FileName;
                    var app = MyUtil.a(boardName, mLastUploadFilePath, mUploadPath);
                    app.BoardId = boardId;
                    app.BoardName = boardName;
                    if (new FormAppUpload(app).ShowDialog() == DialogResult.OK)
                    {
                        ConfDao.UpdateLastUploadFilePath(mLastUploadFilePath);
                        LoadData();
                    }
                    else
                    {
                        MyUtil.DelAppFiles(app);
                    }
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //test();
            var info = MyUtil.ReadApkInfo("C:\\Users\\Administrator\\Desktop\\UCliulanqi_141.apk");
        }

        private void uploadDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (mUploadPath.Length > 0) fb.SelectedPath = mUploadPath;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                mUploadPath = fb.SelectedPath;
                ConfDao.UpdateUploadPath(mUploadPath);
            }
        }

        private void menuDeleteClick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                AppDao.Del(Int32.Parse(dataGridView1.SelectedRows[i].Cells[0].Value.ToString()));
            }
            LoadData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请先进行选择");
                    return;
                }
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Delete", menuDeleteClick));
                m.Show(dataGridView1, new Point(mMouseDownX, mMouseDownY));
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            mMouseDownX = e.X;
            mMouseDownY = e.Y;
        }

        private void test()
        {
            string apkPath = "C:\\Users\\Administrator\\Desktop\\baidushurufa_75.apk";
            ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(apkPath));
            var filestream = new FileStream(apkPath, FileMode.Open, FileAccess.Read);
            ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
            ICSharpCode.SharpZipLib.Zip.ZipEntry item;


            while ((item = zip.GetNextEntry()) != null)
            {
                if (item.Name == "AndroidManifest.xml")
                {
                    byte[] bytes = new byte[50 * 1024];

                    Stream strm = zipfile.GetInputStream(item);
                    int size = strm.Read(bytes, 0, bytes.Length);

                    using (BinaryReader s = new BinaryReader(strm))
                    {
                        byte[] bytes2 = new byte[size];
                        Array.Copy(bytes, bytes2, size);
                        AndroidDecompress decompress = new AndroidDecompress();
                        var content = decompress.decompressXML(bytes);
                        Console.WriteLine(content);
                    }
                }
            }
        }

    }
}
