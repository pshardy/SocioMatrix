using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioMatrix {
    public class SocioType {
        public string name { get; set; }
        public Dictionary<string, int> sameTypes = new Dictionary<string, int>();
    }
    public class SocioItem {
        public Dictionary<string, string> keyValues = new Dictionary<string, string>();
    }

    public class SocioMatrix {
        public Dictionary<string, SocioType> headerDict = new Dictionary<string, SocioType>();
        public List<SocioType> headers = new List<SocioType>();

        public BindingList<SocioItem> itemList = new BindingList<SocioItem>();


        public void CalculateMatrix() {
            for (int i = 0; i < itemList.Count; i++) {
                foreach (SocioType header in headers) {
                    foreach (SocioType nextHeader in headers) {
                        if (header != nextHeader && itemList[i].keyValues[header.name] == itemList[i].keyValues[nextHeader.name] &&
                            (itemList[i].keyValues[header.name] == "3" || itemList[i].keyValues[header.name] == "1")) {
                            if (!header.sameTypes.ContainsKey(nextHeader.name)) {
                                header.sameTypes.Add(nextHeader.name, 1);
                            } else {
                                header.sameTypes[nextHeader.name]++;
                            }
                        }
                    }
                }
            }
        }


        public void SaveToFile(string fileName) {

        }

        /// <summary>
        /// Loads a data file with tab separated values.
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFromFile(string fileName) {
            if (File.Exists(fileName)) {
                headerDict.Clear();
                headers.Clear();
                itemList.Clear();
                StreamReader reader = new StreamReader(File.OpenRead(fileName));
                int lineNum = 0;
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] values = line.Split('\t');
                    if (lineNum == 0) {
                        foreach (string v in values) {
                            SocioType type = new SocioType();
                            type.name = v;
                            headers.Add(type);
                            // Set for O(1) access.
                            headerDict[v] = type;
                        }
                    } else {
                        SocioItem item = new SocioItem();
                        for (int i = 0; i < headers.Count; i++) {
                            item.keyValues[headers[i].name] = values[i];
                        }

                        itemList.Add(item);
                    }

                    lineNum++;
                }
            }
        }
    }
}
