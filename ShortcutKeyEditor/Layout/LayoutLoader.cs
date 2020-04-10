using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortcutKeyEditor
{
    /// <summary>
    /// レイアウトローダー
    /// </summary>
    public class LayoutLoader
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private LayoutLoader()
        {

        }

        /// <summary>
        /// レイアウト設定ファイルを読み込み
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static LayoutParam Load(string fileName)
        {
            LayoutParam param = new LayoutParam();

            try
            {
                var xml = XDocument.Load(fileName);

                var layout = xml.Element(ElementNameLayout);

                var tabs = layout.Elements(ElementNameTab);

                param.Tabs = new List<LayoutParam.Tab>();
                foreach (var tab in tabs)
                {
                    var layoutTab = new LayoutParam.Tab();
                    layoutTab.Label = tab.Attribute(AttributeLabel)?.Value;
                    layoutTab.Name = tab.Attribute(AttributeName)?.Value;
                    layoutTab.KeySets = new List<LayoutParam.KeySet>();

                    var keysets = tab.Elements(ElementNameKeySet);
                    foreach (var keyset in keysets)
                    {
                        var layoutKeyset = new LayoutParam.KeySet();
                        layoutKeyset.Label = keyset.Attribute(AttributeLabel)?.Value;
                        layoutKeyset.Name = keyset.Attribute(AttributeName)?.Value;
                        layoutKeyset.KeyText = keyset.Value;
                        layoutTab.KeySets.Add(layoutKeyset);
                    }
                    param.Tabs.Add(layoutTab);
                }

                var skin = layout.Element(ElementNameSkin);
                var skinSet = new LayoutParam.SkinSet();
                var skinKeyset = skin?.Element(ElementNameKeySet);
                skinSet.KeyColor = new LayoutParam.SkinSet.Key();
                skinSet.KeyColor.Label = TranslateColor(skinKeyset?.Element(ElementNameLabel)?.Value, SystemColors.ControlText);
                skinSet.KeyColor.Text = TranslateColor(skinKeyset?.Element(ElementNameText)?.Value, SystemColors.ControlText);
                skinSet.KeyColor.Textbox = TranslateColor(skinKeyset?.Element(ElementNameTextbox)?.Value, SystemColors.Control);
                skinSet.KeyColor.Background = TranslateColor(skinKeyset?.Element(ElementNameBackground)?.Value, SystemColors.Window);
                var skinPanel = skin?.Element(ElementNamePanel);
                skinSet.PanelColor = new LayoutParam.SkinSet.Panel();
                skinSet.PanelColor.Label = TranslateColor(skinPanel?.Element(ElementNameLabel)?.Value, SystemColors.ControlText);
                skinSet.PanelColor.Background = TranslateColor(skinPanel?.Element(ElementNameBackground)?.Value, SystemColors.Control);

                param.Skin = skinSet;
            }
            catch
            {
                return null;
            }
            return param;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="defaultColor"></param>
        /// <returns></returns>
        private static Color TranslateColor(string color, Color defaultColor)
        {
            if (color == null)
            {
                return defaultColor;
            }

            try
            {
                return ColorTranslator.FromHtml(color);
            }
            catch
            {
                return defaultColor;
            }
        }

        /// <summary>
        /// 要素名：レイアウトルート
        /// </summary>
        private static readonly string ElementNameLayout = "layout";

        /// <summary>
        /// 要素名：スキン
        /// </summary>
        private static readonly string ElementNameSkin = "skin";

        /// <summary>
        /// 要素名：タブ
        /// </summary>
        private static readonly string ElementNameTab = "tab";

        /// <summary>
        /// 要素名：キー情報
        /// </summary>
        private static readonly string ElementNameKeySet = "keyset";

        /// <summary>
        /// 要素名：パネル
        /// </summary>
        private static readonly string ElementNamePanel = "panel";

        /// <summary>
        /// 要素名：ラベル
        /// </summary>
        private static readonly string ElementNameLabel = "label";

        /// <summary>
        /// 要素名：テキスト
        /// </summary>
        private static readonly string ElementNameText = "text";

        /// <summary>
        /// 要素名：テキストボックス
        /// </summary>
        private static readonly string ElementNameTextbox = "textbox";

        /// <summary>
        /// 要素名：背景
        /// </summary>
        private static readonly string ElementNameBackground = "background";

        /// <summary>
        /// 属性名：ラベル
        /// </summary>
        private static readonly string AttributeLabel = "label";

        /// <summary>
        /// 属性名：名前
        /// </summary>
        private static readonly string AttributeName = "name";
    }
}
