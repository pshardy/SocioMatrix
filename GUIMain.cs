using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocioMatrix {
    public partial class GUIMain : Form {
        private SocioMatrix m_socioMatrix;

        public GUIMain(SocioMatrix matrix) {
            InitializeComponent();
            m_socioMatrix = matrix;
        }

        public void LoadFile() {
            openFileDialog.Title = "Load a file.";
            openFileDialog.FileName = "";
            // User pressed ok on a file.
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                m_socioMatrix.LoadFromFile(openFileDialog.FileName);
                BindRawDataGrid();
            }
        }

        public void SaveFile() {
            saveFileDialog.Title = "Save a file.";
            saveFileDialog.FileName = "";
            // User pressed ok on a file.
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                m_socioMatrix.SaveToFile(saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Loads columns and cells. Very inefficient and should be data bound instead.
        /// </summary>
        private void BindRawDataGrid() {
            this.dataGridView.Columns.Clear();

            // Create columns.
            for (int i = 0; i < m_socioMatrix.headers.Count; i++) {
                this.dataGridView.Columns.Add(m_socioMatrix.headers[i].name, m_socioMatrix.headers[i].name);
            }

            // Fill rows.
            this.dataGridView.Rows.Add(m_socioMatrix.itemList.Count);
            for (int i = 0; i < m_socioMatrix.itemList.Count; i++) {
                for (int j = 0; j < m_socioMatrix.headers.Count; j++) {
                    this.dataGridView.Rows[i].Cells[j].Value = m_socioMatrix.itemList[i].keyValues[m_socioMatrix.headers[j].name];
                }
            }
        }

        private void BindCompleteDataGrid() {
            if (m_socioMatrix != null && m_socioMatrix.headers.Count > 0) {
                this.dataGridView.Columns.Clear();

                this.dataGridView.RowHeadersWidth = 100;

                // Create columns.
                for (int i = 0; i < m_socioMatrix.headers.Count; i++) {
                    this.dataGridView.Columns.Add(m_socioMatrix.headers[i].name, m_socioMatrix.headers[i].name);
                }

                // Fill rows.
                this.dataGridView.Rows.Add(m_socioMatrix.headers.Count);
                for (int i = 0; i < m_socioMatrix.headers.Count; i++) {
                    this.dataGridView.Rows[i].HeaderCell.Value = m_socioMatrix.headers[i].name;
                    for (int j = 0; j < m_socioMatrix.headers.Count; j++) {
                        if (m_socioMatrix.headers[i].sameTypes.ContainsKey(m_socioMatrix.headers[j].name)) {
                            this.dataGridView.Rows[i].Cells[j].Value = m_socioMatrix.headers[i].sameTypes[this.dataGridView.Columns[j].HeaderText];
                        }
                    }
                }

                this.dataGridView.Columns.RemoveAt(0);
                this.dataGridView.Columns.RemoveAt(this.dataGridView.Columns.Count - 1);
                this.dataGridView.Rows.RemoveAt(0);
                this.dataGridView.Rows.RemoveAt(this.dataGridView.Rows.Count - 1);
            }
        }

        private void loadButton_Click(object sender, EventArgs e) {
            try {
                LoadFile();
            } catch (Exception ex) {
                Program.HandleException(ex);
            }
        }

        private void saveButton_Click(object sender, EventArgs e) {
            try {
                SaveFile();
            } catch (Exception ex) {
                Program.HandleException(ex);
            }
        }

        private void calculateButton_Click(object sender, EventArgs e) {
            try {
                m_socioMatrix.CalculateMatrix();
                BindCompleteDataGrid();
            } catch (Exception ex) {
                Program.HandleException(ex);
            }
        }
    }
}
