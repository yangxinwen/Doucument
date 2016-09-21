using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUtls;
using DAL;

namespace ServiceDemo
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
            var dal = new UserInfoDal();

            LogHelper.WriteLog("test");
        }
    }
}
