using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NHibernateGenLib.Domain.AppUpload;
using WindowsFormsUploadApp.Method;
using System.IO;

namespace WindowsFormsUploadApp
{
    public partial class FormAppUpload : Form
    {
        private TbApp mApp;
        public FormAppUpload(TbApp app)
        {
            InitializeComponent();
            mApp = app;
        }

        private void FormAppUpload_Load(object sender, EventArgs e)
        {
            labelBoard.Text = mApp.BoardName;
            Stream s = File.Open(mApp.IconPath, FileMode.Open);
            pictureBoxIcon.Image = Image.FromStream(s);
            s.Close();
            labelPath.Text = mApp.SavePath;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var title = textBoxTitile.Text;
            var note = textNote.Text;
            mApp.Title = title;
            mApp.Note = note;
            mApp.UpTime = DateTime.Now;
            AppDao.addOrUpdate(mApp);
            pictureBoxIcon.Image.Dispose();
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
