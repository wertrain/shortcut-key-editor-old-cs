using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ShortcutKeyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class ShortcutKeyManager
    {
        /// <summary>
        /// 
        /// </summary>
        public class KeySet
        {
            /// <summary>
            /// ショートカットキー文字列（"Ctrl+Z" のようなフォーマット）
            /// </summary>
            public string KeyText { get; set; }

            /// <summary>
            /// 識別名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Keys Keys { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private ShortcutKeyManager()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="keysets"></param>
        /// <returns></returns>
        public static bool WriteSettings(string fileName, List<KeySet> keysets)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(fileName))
                {
                    writer.WriteStartElement(ElementNameShortcutkeys);

                    foreach (var keyset in keysets)
                    {
                        writer.WriteStartElement(ElementNameKey);
                        writer.WriteAttributeString(AttributeName, keyset.Name);
                        writer.WriteValue(keyset.KeyText);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, KeySet> LoadSettings(string fileName)
        {
            var shortcutKeys = new Dictionary<string, KeySet>();

            try
            {
                var xml = XDocument.Load(fileName);

                var root = xml.Element(ElementNameShortcutkeys);
                

                var keys = root.Elements(ElementNameKey);
                foreach (var key in keys)
                {
                    var keyName = key.Attribute(AttributeName)?.Value;
                    if (!string.IsNullOrWhiteSpace(keyName))
                    {
                        var keySet = new KeySet();
                        keySet.Name = keyName;
                        keySet.KeyText = key.Value;
                        keySet.Keys = KeyTextToKey(key.Value);
                        shortcutKeys.Add(keyName, keySet);
                    }
                }
            }
            catch
            {
                return null;
            }

            return shortcutKeys;
        }

        /// <summary>
        /// キー入力テキストを設定
        /// </summary>
        /// <param name="text"></param>
        private Keys KeyTextToKey(string text)
        {
            var textList = new List<string>(text.Split(InputKeySeparator.ToCharArray()));

            Keys keyValue = 0;
            foreach (var keyText in textList)
            {
                var key = keyText.Trim();
                switch (key)
                {
                    case "Ctrl": keyValue |= Keys.Control; break;
                    case "Shift": keyValue |= Keys.Shift; break;
                    case "Alt": keyValue |= Keys.Alt; break;
                }

                Keys parseKey;
                if (Enum.TryParse(key, out parseKey))
                {
                    keyValue |= parseKey;
                }
            }

            return keyValue;
        }

        /// <summary>
        /// 要素名：ショートカットキールート
        /// </summary>
        private static readonly string ElementNameShortcutkeys = "shortcutkeys";

        /// <summary>
        /// 要素名：キー
        /// </summary>
        private static readonly string ElementNameKey = "key";

        /// <summary>
        /// 属性名：名前
        /// </summary>
        private static readonly string AttributeName = "name";

        /// <summary>
        /// キー表示のセパレータ
        /// </summary>
        private static string InputKeySeparator = "+";
    }
}
