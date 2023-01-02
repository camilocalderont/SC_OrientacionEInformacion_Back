using Dominio.Models.AtencionesGrupales;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Kernel.Pdf.Colorspace;

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


                   
                    doc.Add(new Paragraph($"Fecha del reporte: {DateTime.Now}"));

                    //Table table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 30, 20, 30 }));

                    Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();

                   //table.SetMarginBottom(30);

                   //table.SetMarginTop(5);
                   //
           

                    Cell cell_1 = new Cell(1, 4).Add(new Paragraph("Datos del caso"));
                    cell_1.SetBorder(Border.NO_BORDER)
                          .SetBorderTop(new SolidBorder(ColorConstants.BLUE, 1))
                          .SetBorderBottom(new SolidBorder(ColorConstants.BLUE, 2))
                          .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                          .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1))
                          .SetBackgroundColor(ColorConstants.WHITE)
                          .SetFontSize(20f)
                          .SetHeight(35);
                         
                    table.AddCell(cell_1);

                             

                    Cell cell_2 = new Cell(1, 1).Add(new Paragraph("Número de caso:"));
                    cell_2.SetBorder(Border.NO_BORDER)
                          .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                          .SetFontColor(DeviceGray.BLACK)
                          .SetFontSize(12f)
                          .SetHeight(55);
                    table.AddCell(cell_2);

                    Cell cell_3 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_3.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_3);


                    Cell cell_4 = new Cell(1, 1).Add(new Paragraph("Número de anexos:"));
                    cell_4.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_4);

                    Cell cell_5 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_5.SetBorder(Border.NO_BORDER)
                          .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_5);

                    Cell cell_6 = new Cell(1, 1).Add(new Paragraph("Tema brindado:"));
                    cell_6.SetBorder(Border.NO_BORDER)
                          .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))                        
                          .SetHeight(55);
                    table.AddCell(cell_6);

                    Cell cell_7 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_7.SetBorder(Border.NO_BORDER);                      
                    table.AddCell(cell_7);


                    Cell cell_8 = new Cell(1, 1).Add(new Paragraph("Fecha de orientación:"));
                    cell_8.SetBorder(Border.NO_BORDER);                        
                    table.AddCell(cell_8);

                    Cell cell_9 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_9.SetBorder(Border.NO_BORDER)
                          .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));                      
                    table.AddCell(cell_9);


                    Cell cell_10 = new Cell(1, 1).Add(new Paragraph("Motivo:"));
                    cell_10.SetBorder(Border.NO_BORDER)
                           .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))                        
                           .SetHeight(55);
                    table.AddCell(cell_10);

                    Cell cell_11 = new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"));
                    cell_11.SetBorder(Border.NO_BORDER)
                           .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));                         
                    table.AddCell(cell_11);


                    Cell cell_12 = new Cell(1, 1).Add(new Paragraph("Sub Motivo:"));
                    cell_12.SetBorder(Border.NO_BORDER)
                           .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                           .SetHeight(55);
                    table.AddCell(cell_12);

                    Cell cell_13 = new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"));
                    cell_13.SetBorder(Border.NO_BORDER)
                         .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_13);

                    Cell cell_14 = new Cell(1, 1).Add(new Paragraph("Tiempo de duración:"));
                    cell_14.SetBorder(Border.NO_BORDER)
                            .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                            .SetHeight(55);
                    table.AddCell(cell_14);

                    Cell cell_15 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_15.SetBorder(Border.NO_BORDER);      
                    table.AddCell(cell_15);

                    Cell cell_16 = new Cell(1, 1).Add(new Paragraph("Número de usuarios:"));
                    cell_16.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_16);

                    Cell cell_17 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_17.SetBorder(Border.NO_BORDER)
                           .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_17);


                    Cell cell_18 = new Cell(1, 1).Add(new Paragraph("Tipo de actividad:"));
                    cell_18.SetBorder(Border.NO_BORDER)
                           .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                           .SetHeight(55);
                    table.AddCell(cell_18);

                    Cell cell_19 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_19.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_19);

                    Cell cell_20 = new Cell(1, 1).Add(new Paragraph("Tipo de gestión:"));
                    cell_20.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_20);

                    Cell cell_21 = new Cell(1, 1).Add(new Paragraph("xxx"));
                    cell_21.SetBorder(Border.NO_BORDER)
                          .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_21);

                    Cell cell_22 = new Cell(1, 1).Add(new Paragraph("Aclaraciones gestión:"));
                    cell_22.SetBorder(Border.NO_BORDER)
                           .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                           .SetHeight(55);
                    table.AddCell(cell_22);

                    Cell cell_23 = new Cell(1, 3).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"));
                    cell_23.SetBorder(Border.NO_BORDER)
                         .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_23);


                    Cell cell_24 = new Cell(1, 1).Add(new Paragraph("Localidad:"));
                    cell_24.SetBorder(Border.NO_BORDER)
                           .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                           .SetHeight(55);
                    table.AddCell(cell_24);

                    Cell cell_25 = new Cell(1, 1).Add(new Paragraph("XXX"));
                    cell_25.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_25);


                    Cell cell_26 = new Cell(1, 1).Add(new Paragraph("Lugar:"));
                    cell_26.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell_26);

                    Cell cell_27 = new Cell(1, 1).Add(new Paragraph("XXX"));
                    cell_27.SetBorder(Border.NO_BORDER)
                           .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_27);

                    Cell cell_28 = new Cell(1, 1).Add(new Paragraph("Usuario Actual:"));
                    cell_28.SetBorder(Border.NO_BORDER)
                           .SetBorderLeft(new SolidBorder(ColorConstants.BLUE, 1))
                           .SetBorderBottom(new SolidBorder(ColorConstants.BLUE, 1))
                           .SetHeight(55);
                    table.AddCell(cell_28);

                    Cell cell_29 = new Cell(1, 5).Add(new Paragraph("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"));
                    cell_29.SetBorder(Border.NO_BORDER)
                         .SetBorderRight(new SolidBorder(ColorConstants.BLUE, 1))
                         .SetBorderBottom(new SolidBorder(ColorConstants.BLUE, 1));
                    table.AddCell(cell_29);

                    doc.Add(table);
                   

                    Table tableFooter = new Table(UnitValue.CreatePercentArray(new float[] { 20, 80 }));

                    tableFooter.AddCell(CreateImageCell(1, 1, LOGO_SICUENTANOS));
                    tableFooter.AddCell(new Cell(1, 1).SetBorder(Border.NO_BORDER).Add(new Paragraph("Página X/X")));
                    tableFooter.SetTextAlignment(TextAlignment.CENTER);

                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tableFooter));

                    doc.Close();
                }

                // Encode the PDF as a Base64 string
                return Convert.ToBase64String(stream.ToArray());
            }
        }

   

        private static Cell CreateImageCell(int rowspan, int colspan, string path)
        {
            Image img = new Image(ImageDataFactory.Create(path));
                  
            return new Cell(rowspan,colspan).SetBorder(Border.NO_BORDER).Add(img.SetAutoScale(true).SetWidth(UnitValue.CreatePercentValue(100)));
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

                table.AddHeaderCell(CreateImageCell(2, 1, LOGO_SECRETARIA));
               

                Cell cell_1 = new Cell(1, 1).Add(new Paragraph("Sistema de información SiCuéntanos Bogotá"));
                cell_1.SetBorder(Border.NO_BORDER)
                      .SetFontSize(20f);
                table.AddHeaderCell(cell_1);

                Cell cell_2 = new Cell(1, 1).Add(new Paragraph("Resumen del caso de atenciones grupales - capacitaciones"));
                cell_2.SetBorder(Border.NO_BORDER)
                      .SetFontSize(15f);
                table.AddHeaderCell(cell_2);
            }


            private void Bodytable()
            {


            }


          
        }

    }
}
