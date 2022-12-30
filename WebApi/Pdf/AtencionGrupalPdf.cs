using Dominio.Models.AtencionesGrupales;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Layout;
using iText.Layout.Renderer;

namespace WebApi.Pdf
{
    public class AtencionGrupalPdf
    {
        public static readonly string LOGO_SECRETARIA = "Pdf/images/logo_secretaria.png";
        public static readonly string LOGO_SICUENTANOS = "Pdf/images/logo_sicuentanos.png";

        public static readonly PageSize pageSize = PageSize.A4;
        public string generar(AtencionGrupal atencionGrupal)
        {
            // Create a new memory stream
            using (var stream = new MemoryStream())
            {
                // Create a new PDF document
                using (var writer = new PdfWriter(stream))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf, pageSize))
                {                    
                    doc.SetMargins(109, 36, 72, 36);

                    TableHeaderEventHandler handler = new TableHeaderEventHandler(doc);
                    pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, handler);

                    Table tableFooter = new Table(UnitValue.CreatePercentArray(new float[] { 20, 80 }));

                    tableFooter.AddCell(CreateImageCell(1, 1, LOGO_SICUENTANOS));
                    tableFooter.AddCell(new Cell(1, 1).Add(new Paragraph("Página X/X")));
                    
                    //cell.SetBackgroundColor(ColorConstants.ORANGE);
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tableFooter));


                    doc.Add(new Paragraph($"Fecha del reporte: {DateTime.Now}"));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 30, 20, 30 }));
                    table.SetMarginTop(5);
                    table.AddCell(new Cell(1, 4).Add(new Paragraph("Datos del caso")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Número del caso:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Número del anexos:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Tema brindado:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Fecha de orientación")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Motivo:")));
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Sub Motivo:")));
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));


                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Tiempo de duración:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Número de usuarios")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Tipo de actividad:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Tipo de gestión:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Aclaraciones gestión:")));
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Localidad:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Lugar:")));
                    table.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table.AddCell(new Cell(1, 1).Add(new Paragraph("Usuario Actual:")));
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXX XXXXXXX XXXXXXX XXXXXXXXXX")));

                    doc.Add(table);


                    Table table1 = new Table(UnitValue.CreatePercentArray(new float[] { 20, 30, 20, 30 }));
                    table1.SetMarginTop(5);
                    table1.AddCell(new Cell(1, 4).Add(new Paragraph("Datos del caso")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Número del caso:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Número del anexos:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Tema brindado:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Fecha de orientación")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Motivo:")));
                    table1.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Sub Motivo:")));
                    table1.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));


                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Tiempo de duración:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Número de usuarios")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Tipo de actividad:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Tipo de gestión:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Aclaraciones gestión:")));
                    table1.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Localidad:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Lugar:")));
                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table1.AddCell(new Cell(1, 1).Add(new Paragraph("Usuario Actual:")));
                    table1.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXX XXXXXXX XXXXXXX XXXXXXXXXX")));

                    doc.Add(table1);


                    Table table2 = new Table(UnitValue.CreatePercentArray(new float[] { 20, 30, 20, 30 }));
                    table2.SetMarginTop(5);
                    table2.AddCell(new Cell(1, 4).Add(new Paragraph("Datos del caso")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Número del caso:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Número del anexos:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Tema brindado:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Fecha de orientación")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Motivo:")));
                    table2.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Sub Motivo:")));
                    table2.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));


                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Tiempo de duración:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Número de usuarios")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Tipo de actividad:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Tipo de gestión:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Aclaraciones gestión:")));
                    table2.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Localidad:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Lugar:")));
                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("XXX")));

                    table2.AddCell(new Cell(1, 1).Add(new Paragraph("Usuario Actual:")));
                    table2.AddCell(new Cell(1, 3).Add(new Paragraph("XXXXXXX XXXXXXX XXXXXXX XXXXXXXXXX")));

                    doc.Add(table2);



                    doc.Close();
                }

                // Encode the PDF as a Base64 string
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private static Cell CreateImageCell(int rowspan, int colspan, string path)
        {
            Image img = new Image(ImageDataFactory.Create(path));
            return new Cell(rowspan,colspan).Add(img.SetAutoScale(true).SetWidth(UnitValue.CreatePercentValue(100)));
        }

        private class TableFooterEventHandler : IEventHandler
        {
            private Table table;

            public TableFooterEventHandler(Table table)
            {
                this.table = table;
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

                new Canvas(canvas, new Rectangle(36, 20, page.GetPageSize().GetWidth() - 72, 50))
                    .Add(table)
                    .Close();
            }
        }

        private class TableHeaderEventHandler : IEventHandler
        {
            private Table table;
            private float tableHeight;
            private Document doc;

            public TableHeaderEventHandler(Document doc)
            {
                this.doc = doc;
                InitTable();

                TableRenderer renderer = (TableRenderer)table.CreateRendererSubTree();
                renderer.SetParent(new DocumentRenderer(doc));

                // Simulate the positioning of the renderer to find out how much space the header table will occupy.
                LayoutResult result = renderer.Layout(new LayoutContext(new LayoutArea(0, pageSize)));
                tableHeight = result.GetOccupiedArea().GetBBox().GetHeight();
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                PageSize pageSize = pdfDoc.GetDefaultPageSize();
                float coordX = pageSize.GetX() + doc.GetLeftMargin();
                float coordY = pageSize.GetTop() - doc.GetTopMargin();
                float width = pageSize.GetWidth() - doc.GetRightMargin() - doc.GetLeftMargin();
                float height = GetTableHeight();
                Rectangle rect = new Rectangle(coordX, 720, width, height);

                new Canvas(canvas, rect)
                    .Add(table)
                    .Close();
            }

            public float GetTableHeight()
            {
                return tableHeight;
            }

            private void InitTable()
            {
                table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 80 }));

                table.AddCell(CreateImageCell(2, 1, LOGO_SECRETARIA));
                table.AddCell(new Cell(1, 1).Add(new Paragraph("Sistema de información SiCuéntanos Bogotá")));
                table.AddCell(new Cell(1, 1).Add(new Paragraph("Resumen del caso de atenciones grupales - capacitaciones")));

            }
        }

    }
}
