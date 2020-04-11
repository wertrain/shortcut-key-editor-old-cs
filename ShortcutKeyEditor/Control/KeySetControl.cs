using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutKeyEditor
{
    /// <summary>
    /// ショートカットキー入力コントロール
    /// </summary>
    public partial class KeySetControl : UserControl
    {
        /// <summary>
        /// セットされているキー
        /// </summary>
        public Keys Key { get; private set; }

        /// <summary>
        /// セットされているキーの文字列（"Ctrl+Z" のようなフォーマット）
        /// </summary>
        public string KeyText { get { return KeysToString(Key); } }

        /// <summary>
        /// コントロールの識別名
        /// </summary>
        public string KeySetName { get; set; }

        /// <summary>
        /// ラベル表示幅
        /// </summary>
        public int LabelWidth { get { return labelKeyName.Width; } }

        /// <summary>
        /// ラベル文字色
        /// </summary>
        public Color LabelColor
        {
            set
            {
                labelKeyName.ForeColor = value;
                labelSeparator.ForeColor = value;
            }
        }

        /// <summary>
        /// テキスト文字色
        /// </summary>
        public Color TextColor { set { textBoxKeyEdit.ForeColor = value; } }

        /// <summary>
        /// テキストボックス色
        /// </summary>
        public Color TextboxColor { set { textBoxKeyEdit.BackColor = value; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeySetControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ラベル表示テキストを設定
        /// </summary>
        /// <param name="label"></param>
        public void SetLabelText(string text)
        {
            labelKeyName.Text = text;
        }

        /// <summary>
        /// キー入力テキストを設定
        /// </summary>
        /// <param name="text"></param>
        public void SetKeyText(string text)
        {
            var textList = new List<string>(text.Split(InputKeySeparator.ToCharArray()));

            foreach (var keyText in textList)
            {
                var key = keyText.Trim();
                switch (key)
                {
                    case "Ctrl": Key |= Keys.Control; break;
                    case "Shift": Key |= Keys.Shift; break;
                    case "Alt": Key |= Keys.Alt; break;
                }

                Keys parseKey;
                if (Enum.TryParse(key, out parseKey))
                {
                    Key |= parseKey;
                }
            }

            textBoxKeyEdit.Text = KeysToString(Key);
        }

        /// <summary>
        /// オプションキーを文字列に変換
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string ModifierKeysToString(Keys key)
        {
            var pressKey = new StringBuilder();

            if ((key & Keys.Control) == Keys.Control)
            {
                pressKey.Append("Ctrl");
            }

            if ((key & Keys.Shift) == Keys.Shift)
            {
                if (pressKey.Length > 0)
                {
                    pressKey.Append(InputKeySeparator);
                }

                pressKey.Append("Shift");
            }

            if ((key & Keys.Alt) == Keys.Alt)
            {
                if (pressKey.Length > 0)
                {
                    pressKey.Append(InputKeySeparator);
                }

                pressKey.Append("Alt");
            }

            return pressKey.ToString();
        }

        /// <summary>
        /// Keys を文字列に変換する
        /// （オプションキーも統合されている前提）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string KeysToString(Keys key)
        {
            var pressKey = new StringBuilder();
            pressKey.Append(ModifierKeysToString(Key));

            if (key != Keys.ControlKey && key != Keys.ShiftKey && key != Keys.Menu)
            {
                if (pressKey.Length > 0)
                {
                    pressKey.Append(InputKeySeparator);
                }

                var appendKey = key;
                appendKey &= ~Keys.Control;
                appendKey &= ~Keys.Shift;
                appendKey &= ~Keys.Alt;
                pressKey.Append(appendKey);
            }

            return pressKey.ToString();
        }

        /// <summary>
        /// キー入力イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxKeyEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var textbox = sender as TextBox;

            Key = ModifierKeys;
            textbox.Text = ModifierKeysToString(ModifierKeys);
        }

        /// <summary>
        /// キー入力イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxKeyEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            var pressKey = new StringBuilder();

            pressKey.Append(ModifierKeysToString(ModifierKeys));

            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Menu)
            {
                if (pressKey.Length > 0)
                {
                    pressKey.Append(InputKeySeparator);
                }

                Key = ModifierKeys | e.KeyCode;

                pressKey.Append(e.KeyCode.ToString().ToUpper());
            }

            textbox.Text = pressKey.ToString();
        }

        /// <summary>
        /// 入力されたキー表示のセパレータ
        /// </summary>
        private static string InputKeySeparator = "+";
    }
}
