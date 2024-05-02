using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetPackCreator
{
    public partial class MarkdownPreview : Form
    {
        private string html;
        public MarkdownPreview(string html)
        {
            this.html = html;
            InitializeComponent();
            Task t = webView.EnsureCoreWebView2Async();
            while(!t.IsCompletedSuccessfully)
            {
                Application.DoEvents();
            }
        }

        private void MarkdownPreview_Load(object sender, EventArgs e)
        {
            webView.CoreWebView2.NavigateToString(html);
        }
    }
}
