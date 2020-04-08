using System;
using System.Collections.Generic;
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
                    layoutTab.KeySets = new List<LayoutParam.KeySet>();

                    var keysets = tabs.Elements(ElementNameKeyset);
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
        private static readonly string ElementNameLayout = "layout";
        
        /// <summary>
        /// 
        /// </summary>
        private static readonly string ElementNameTab = "tab";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string ElementNameKeyset = "keyset";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string AttributeLabel = "label";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string AttributeName = "name";
    }
}
