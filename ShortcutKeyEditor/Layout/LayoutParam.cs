using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutKeyEditor
{
    /// <summary>
    /// 
    /// </summary>
    public class LayoutParam
    {
        /// <summary>
        /// タブのリスト
        /// </summary>
        public List<Tab> Tabs { get; set; }

        /// <summary>
        /// 所属キーのリスト
        /// </summary>
        public List<KeySet> KeySets { get; set; }

        /// <summary>
        /// タブ
        /// </summary>
        public class Tab
        {
            /// <summary>
            /// ラベル
            /// </summary>
            public string Label { get; set; }

            /// <summary>
            /// 所属キーのリスト
            /// </summary>
            public List<KeySet> KeySets { get; set; }
        }

        /// <summary>
        /// 割り当てるキー
        /// </summary>
        public class KeySet
        {
            /// <summary>
            /// ラベル
            /// </summary>
            public string Label { get; set; }

            /// <summary>
            /// 名前
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// キー表示テキスト
            /// </summary>
            public string KeyText { get; set; }
        }
    }
}
