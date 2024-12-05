using System;
using System.IO;

using iText.IO.Font;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
namespace MatrixWeb.UtilitarioPdf
{
    public class GeneradorPDF
    {

        public static readonly String BOLD = "../fonts/OpenSans-Bold.ttf";
        public static readonly String FONT = "../fonts/OpenSans-Regular.ttf";
        private PdfFormXObject template;

        private Image total;

        private PdfFont font;

        private PdfFont bold;
        static bool EsNumero(string input)
        {
            // Remover las comas de la cadena para convertir correctamente el número
            input = input.Replace(",", "");

            if (decimal.TryParse(input, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public enum TipoHorientacion
        {
            Vertical = 1, Horizontal = 2
        }
        public enum TipoPagina
        {
            PaginaA4 = 1, PaginaA3 = 2
        }
        public MemoryStream GenerarPDF(DataSet dataset, string TituloPrincipal, List<string> ListaTitulosSecundarios, TipoHorientacion Horientacion, List<float[]> ListaAnchosColumnas, TipoPagina Pagina = TipoPagina.PaginaA4, List<int> ListaAnchoPorcentajeTabla = null)
        {
            font = PdfFontFactory.CreateFont("fonts/OpenSans-Regular.ttf", PdfEncodings.IDENTITY_H);
            bold = PdfFontFactory.CreateFont("fonts/OpenSans-Bold.ttf", PdfEncodings.IDENTITY_H);
            PageSize pageSize = null;

            //if (Pagina == TipoPagina.PaginaA4)
            //{
            //    if (Horientacion == TipoHorientacion.Horizontal)
            //    {
            //        doc = new Document(PageSize.A4.Rotate(), 30, 30, 30, 30);
            //    }
            //    else
            //    {
            //        doc = new Document(PageSize.A4, 30, 30, 30, 30);
            //    }
            //}

            //if (Pagina == TipoPagina.PaginaA3)
            //{
            //    if (Horientacion == TipoHorientacion.Horizontal)
            //    {
            //        doc = new Document(PageSize.A3.Rotate(), 30, 30, 30, 30);
            //    }
            //    else
            //    {
            //        doc = new Document(PageSize.A3, 30, 30, 30, 30);
            //    }
            //}

            if (Pagina == TipoPagina.PaginaA4)
            {
                if (Horientacion == TipoHorientacion.Horizontal)
                {
                    pageSize = new PageSize(PageSize.A4).Rotate();
                }
                else
                {
                    pageSize = new PageSize(PageSize.A4);
                }
            }

            if (Pagina == TipoPagina.PaginaA3)
            {
                if (Horientacion == TipoHorientacion.Horizontal)
                {
                    pageSize = new PageSize(PageSize.A3).Rotate();
                }
                else
                {
                    pageSize = new PageSize(PageSize.A3);
                }
            }

            // Crear una instancia de MemoryStream para almacenar el PDF generado
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdf = new PdfDocument(writer);
            Document doc = new Document(pdf, pageSize);
            doc.SetMargins(30, 30, 30, 30);
            // Crear el escritor PDF
            // Agregar evento para la numeración de páginas
            //writer.PageEvent = new NumeracionPaginas();

            // Agregar cabecera con el logo y el título
            // Puedes personalizar esta parte según tus necesidades
            Table headerTable = new Table(1);
            // headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            // Agregar logo al encabezado
            string logoPath = ConfigurationManager.AppSettings["RutaImagenesLogoEntidadReportePdf"]; // Ruta al archivo de imagen del logo
                                                                                                     //Image logo = Image.GetInstance(logoPath);

            iText.Layout.Element.Image logo = new iText.Layout.Element.Image(ImageDataFactory.Create(logoPath))
              .SetWidth(200) // Ancho de la imagen en puntos
              .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            logo.ScaleAbsolute(300.00f, 30.00f);
            logo.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            logo.SetPaddingTop(10f);
            doc.Add(logo);

            // Creates a header for every page in the document
            template = new PdfFormXObject(new Rectangle(775, 575, 30, 30));
            PdfCanvas canvas = new PdfCanvas(template, pdf);

            total = new Image(template);
            total.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);

            // Creates a header for every page in the document
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new HeaderHandler(this));

            PdfDictionary parameters = new PdfDictionary();
            parameters.Put(PdfName.ModDate, new PdfDate().GetPdfObject());

            // Agregar el título

            //Cell titleCell = new Cell().Add(new Paragraph(TituloPrincipal));
            //headerTable.AddCell(titleCell);
            //doc.Add(headerTable);
            var phraseTituloPrincipal = new Paragraph(TituloPrincipal);
            doc.Add(phraseTituloPrincipal);
            // Escribir la frase en la posición exacta        

            var phraseFecha = new Paragraph("Fecha de emisión: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            doc.Add(phraseFecha);
           // BaseColor colorEncabezado = new BaseColor(23, 55, 95); // Azul oscuro
            for (int t = 0; t <= dataset.Tables.Count - 1; t++)
            {
                if (ListaTitulosSecundarios != null)
                {
                    doc.Add(new Paragraph(ListaTitulosSecundarios[t]));
                }
                // Agregar un salto de línea más pequeño
                Paragraph paragraph2 = new Paragraph();
                doc.Add(paragraph2);


                // Agregar tabla con los datos
                Table dataTable = new Table(dataset.Tables[t].Columns.Count + 1);
                dataTable.SetWidth(ListaAnchoPorcentajeTabla == null ? 100 : ListaAnchoPorcentajeTabla[t]);

                // Agregar las celdas con los datos
                Cell cellN = new Cell().Add(new Paragraph("N°"))
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetBackgroundColor(ColorConstants.BLACK)
                    .SetFontColor(ColorConstants.WHITE)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetBorder(Border.NO_BORDER).SetBackgroundColor(ColorConstants.BLACK)
                    .SetWidth(0.1f);
                dataTable.AddCell(cellN);

                foreach (DataColumn column in dataset.Tables[t].Columns)
                {
                    Cell cell = new Cell().Add(new Paragraph(column.ColumnName))
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetBackgroundColor(ColorConstants.WHITE)
                      .SetFontColor(ColorConstants.WHITE)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetBorder(Border.NO_BORDER).SetBackgroundColor(ColorConstants.BLACK)
                    .SetWidth(2f);
                    dataTable.AddCell(cell);
                }

                int x = 1;


                foreach (DataRow row in dataset.Tables[t].Rows)
                {

                    Cell cellNN = new Cell().Add(new Paragraph(x.ToString()));
                    //cellNN.HorizontalAlignment = Element.ALIGN_RIGHT;
                    //cellNN.BorderColor = new Color(222, 226, 230);
                    //cellNN.BorderWidth = 0.1f;
                    if (x % 2 == 0)
                    {
                        cellNN.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                    }
                    else
                    {
                        cellNN.SetBackgroundColor(ColorConstants.WHITE);
                    }

                    //cellNN.NoWrap = true;
                    dataTable.AddCell(cellNN);
                    foreach (object item in row.ItemArray)
                    {
                        Cell cell = new Cell().Add(new Paragraph(item.ToString()));
                        if (item.GetType() == typeof(decimal) || item.GetType() == typeof(int))
                        {
                            cell.SetHorizontalAlignment(HorizontalAlignment.LEFT);
                        }
                        else
                        {
                            if (EsNumero(item.ToString()))
                            {
                                cell.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                            }
                            else
                            {
                                cell.SetHorizontalAlignment(HorizontalAlignment.LEFT);
                            }
                        }
                        //cell.BorderColor = new BaseColor(222, 226, 230);
                        //cell.BorderWidth = 0.1f;
                        //cell.BackgroundColor = colorFila;
                        //cell.NoWrap = false;
                        if (x % 2 == 0)
                        {
                            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                        }
                        else
                        {
                            cell.SetBackgroundColor(ColorConstants.WHITE);
                        }
                        dataTable.AddCell(cell);
                    }
                    x++;
                }

                //dataTable.DefaultCell.BorderWidth = 0.1f;
                //dataTable.SetWidths(ListaAnchosColumnas[t]);
                doc.Add(dataTable);
            }
            //canvas.BeginText();
            //canvas.SetFontAndSize(bold, 12);
            //canvas.MoveText(820, 600);
            //canvas.ShowText(pdf.GetNumberOfPages().ToString());
            //canvas.EndText();
            //canvas.Stroke();
            doc.Close();
            return memoryStream;
        }
        private class HeaderHandler : IEventHandler
        {
            private readonly GeneradorPDF enclosing;

            public HeaderHandler(GeneradorPDF enclosing)
            {
                this.enclosing = enclosing;
            }

            public virtual void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfPage page = docEvent.GetPage();
                int pageNum = docEvent.GetDocument().GetPageNumber(page);

                PdfCanvas canvas = new PdfCanvas(page);

                // Creates header text content
                canvas.BeginText();
                canvas.SetFontAndSize(enclosing.font, 12);
                canvas.BeginMarkedContent(PdfName.Artifact);
                canvas.MoveText(page.GetPageSize().GetWidth() - 50, 10);
                //canvas.ShowText("Test");
                //canvas.MoveText(703, 0);
                canvas.ShowText(String.Format("Pág {0:d}", pageNum));
                canvas.EndText();
                canvas.Stroke();
                canvas.AddXObjectAt(enclosing.template, 0, 0);
                canvas.EndMarkedContent();
                canvas.Release();
            }
        }
    }

}