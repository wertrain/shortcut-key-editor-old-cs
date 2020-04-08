using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutKeyEditor
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class FormKeyEditor : Form
    {
        /// <summary>
        /// レイアウト
        /// </summary>
        private LayoutLoader LayoutLoader { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormKeyEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームのロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyEditor_Load(object sender, EventArgs e)
        {
            var layout = LayoutLoader.Load(@"D:\Develop\C#\shortcut-key-editor-cs\ShortcutKeyEditor\EditorLayout.xml");

            if (layout == null)
            {
                Close();

                return;
            }
            
            foreach (var layoutTab in layout.Tabs)
            {
                var tabPage = new TabPage();
                tabPage.Text = layoutTab.Label;
                tabPage.AutoScroll = true;

                int yPos = 0;
                foreach (var layoutKeySet in layoutTab.KeySets)
                {
                    var keyset = new KeySetControl();
                    keyset.SetLabelText(layoutKeySet.Label);
                    keyset.SetKeyText(layoutKeySet.KeyText);
                    keyset.Tag = layoutKeySet;
                    keyset.Location = new Point(0, yPos);
                    yPos += keyset.Height + 4;
                    tabPage.Controls.Add(keyset);
                }
                tabControlMain.TabPages.Add(tabPage);
            }
        }
    }
}
