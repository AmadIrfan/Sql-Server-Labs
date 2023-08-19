using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Xml.Linq;
using System.Data.SqlClient;
using CRUDA;
using CrystalDecisions.Shared.Json;
using System.Drawing;

namespace SqlProject.BL
{
    internal class Pdf
    {
        public static void GenerateReport(DataGridView dataGridView, String fileName, String name)
        {
            Document document = new Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName + ".pdf", FileMode.Create));
            
            document.Open();
            document.AddTitle(name);
            PdfPCell titleCell = new PdfPCell(new Phrase(name));
            PdfPTable table = new PdfPTable(dataGridView.Columns.Count);
            table.WidthPercentage = 100;
            titleCell.Colspan = dataGridView.ColumnCount;
            titleCell.BackgroundColor = new BaseColor(192, 192, 192);
            titleCell.Padding = 10;
            table.AddCell(titleCell);
            List<float> cWidths = new List<float>();
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                float num = 36f;
                cWidths.Add(num);
            }
            float[] columnWidths = cWidths.ToArray();

            table.SetWidths(columnWidths);
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new BaseColor(SystemColors.ControlLight);
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                    {
                        if (cell.Value == null) { continue; }
                        PdfPCell pdfCell = new PdfPCell(new Phrase(" "));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        table.AddCell(pdfCell);
                    }
                    else
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Value.ToString()));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        table.AddCell(pdfCell);
                    }

                }
            }
            document.Add(table);
            document.Close();
            MessageBox.Show(name + " Report generated successfully. \n Your File is in Debug Folder", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
