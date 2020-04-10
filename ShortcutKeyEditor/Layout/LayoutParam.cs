using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        /// スキン
        /// </summary>
        public SkinSet Skin { get; set; }

        /// <summary>
        /// スキン
        /// </summary>
        public class SkinSet
        {
            /// <summary>
            /// スキン
            /// </summary>
            public Key KeyColor { get; set; }

            /// <summary>
            /// スキン
            /// </summary>
            public Panel PanelColor { get; set; }

            /// <summary>
            /// キー項目
            /// </summary>
            public class Key
            {
                /// <summary>
                /// キー項目の背景色
                /// </summary>
                public Color Background { get; set; }

                /// <summary>
                /// キー項目のラベル文字色
                /// </summary>
                public Color Label { get; set; }

                /// <summary>
                /// キー項目のテキスト文字色
                /// </summary>
                public Color Text { get; set; }

                /// <summary>
                /// キー項目のテキストボックス背景色
                /// </summary>
                public Color Textbox { get; set; }
            }

            /// <summary>
            /// パネル
            /// </summary>
            public class Panel
            {
                /// <summary>
                /// パネルの背景色
                /// </summary>
                public Color Background { get; set; }

                /// <summary>
                /// パネルのラベル文字色
                /// </summary>
                public Color Label { get; set; }
            }
        }

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
            /// 識別名
            /// </summary>
            public string Name { get; set; }

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
            /// 識別名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// キー表示テキスト
            /// </summary>
            public string KeyText { get; set; }
        }
    }
}
