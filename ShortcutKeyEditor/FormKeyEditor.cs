using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShortcutKeyLib;

namespace ShortcutKeyEditor
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class FormKeyEditor : Form
    {
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

            if (!CheckLayoutParam(layout))
            {
                Close();
                return;
            }

            foreach (var layoutTab in layout.Tabs)
            {
                var tabPage = new TabPage();
                tabPage.Text = layoutTab.Label;
                tabPage.AutoScroll = true;
                tabPage.BackColor = layout.Skin.PanelColor.Background;

                var flowPanel = new FlowLayoutPanel();
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.AutoScroll = true;
                flowPanel.BackColor = layout.Skin.PanelColor.Background;
                foreach (var layoutKeySet in layoutTab.KeySets)
                {
                    var keyset = new KeySetControl();
                    keyset.KeySetName = layoutTab.Name + "_" + layoutKeySet.Name;
                    keyset.SetLabelText(layoutKeySet.Label);
                    keyset.SetKeyText(layoutKeySet.KeyText);
                    keyset.LabelColor = layout.Skin.KeyColor.Label;
                    keyset.TextColor = layout.Skin.KeyColor.Text;
                    keyset.TextboxColor = layout.Skin.KeyColor.Textbox;
                    keyset.BackColor = layout.Skin.KeyColor.Background;
                    keyset.Tag = layoutKeySet;
                    flowPanel.Controls.Add(keyset);
                }
                tabPage.Controls.Add(flowPanel);
                tabControlMain.TabPages.Add(tabPage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_save_settings_Click(object sender, EventArgs e)
        {
            var keysets = new List<ShortcutKeyManager.KeySet>();
            foreach (TabPage tabPage in tabControlMain.TabPages)
            {
                var flowPanel = tabPage.Controls[0] as FlowLayoutPanel;
                foreach (var control in flowPanel.Controls)
                {
                    if (control is KeySetControl)
                    {
                        var keysetControl = control as KeySetControl;
                        // ここでショートカットキーをテキストとしてだけではなくKeys の値も数値として書き込むと扱いやすいが
                        // 設定ファイルを直接開いて、ショートカットキーのテキストだけをユーザーが書き換える可能性がある
                        // （書き換える使い方を許容したい）ので、キーはテキストとして書き込むだけにしておく
                        var keyset = new ShortcutKeyManager.KeySet();
                        keyset.Name = keysetControl.KeySetName;
                        keyset.KeyText = keysetControl.KeyText;
                        keysets.Add(keyset);
                    }
                }
            }

            if (ShortcutKeyManager.WriteSettings(@"./shortcutkeys.xml", keysets))
            {
                Close();
            }
        }

        /// <summary>
        /// LayoutParam が正しいかをチェックする
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        private bool CheckLayoutParam(LayoutParam layout)
        {
            if (layout == null)
            {
                MessageBox.Show(
                    "レイアウト設定ファイルの読み込みに失敗しました。", 
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (var layoutTabA in layout.Tabs)
            {
                if (string.IsNullOrWhiteSpace(layoutTabA.Name))
                {
                    MessageBox.Show(
                        "「" + layoutTabA.Label + "」の識別名が未入力です。",
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                foreach (var layoutTabB in layout.Tabs)
                {
                    if (layoutTabA == layoutTabB)
                    {
                        continue;
                    }

                    if (layoutTabA.Name == layoutTabB.Name)
                    {
                        MessageBox.Show(
                            "「" + layoutTabA.Label + "」と「" + layoutTabB.Label + "」の識別名が重複しています。",
                            Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            foreach (var layoutTab in layout.Tabs)
            {
                foreach (var layoutKeySetA in layoutTab.KeySets)
                {
                    if (string.IsNullOrWhiteSpace(layoutKeySetA.Name))
                    {
                        MessageBox.Show(
                            "タブ「" + layoutTab.Label + "」の「" + layoutKeySetA.Label + "」の識別名が未入力です。",
                            Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    foreach (var layoutKeySetB in layoutTab.KeySets)
                    {
                        if (layoutKeySetA == layoutKeySetB)
                        {
                            continue;
                        }

                        if (layoutKeySetA.Name == layoutKeySetB.Name)
                        {
                            MessageBox.Show(
                                "タブ「" + layoutTab.Label + "」の「" + layoutKeySetA.Label + "」と「" + layoutKeySetB.Label + "」の識別名が重複しています。",
                                Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
