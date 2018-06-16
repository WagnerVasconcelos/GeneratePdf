using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;


namespace GenerateLayoutPdf
{
    public partial class Generate_PDF : Form
    {
        public Generate_PDF()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            string applicationRoot = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;

            FileStream fs = new FileStream(applicationRoot + "/FileExample2.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document document = new Document(PageSize.A4, 2f, 2f, 10f, 10f);

            var pdfWriter = PdfWriter.GetInstance(document, fs);

            var pdfTable = new PdfPTable(11);

            document.Open();

            MontaCabecalhoPdf(pdfWriter, document);
            MontarContent(pdfWriter, document);
            MontaRodapePdf(pdfWriter, document);

            document.Close();

        }

        private void MontaRodapePdf(PdfWriter writer, Document document)
        {
            //PdfContentByte cb = writer.DirectContent;

            //var rect = new iTextSharp.text.Rectangle(580, 35, 20, 10); //Widht, Heighy,
            ////rect.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            ////rect.BorderWidth = 5;
            //rect.BackgroundColor = new BaseColor(0, 139, 67);
            ////rect.BorderColor = new BaseColor(0, 139, 67);
            //cb.Rectangle(rect);

            PdfContentByte cb = writer.DirectContent;
            PdfTemplate template = cb.CreateTemplate(50, 50);
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            String text = "";
            float len = bf.GetWidthPoint(text, 8);
            Rectangle pageSize = document.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Página " + writer.PageNumber,pageSize.GetRight(40),pageSize.GetBottom(30), 0);
            cb.EndText();

            //Chunk c = new Chunk("Total Cost:");
            //c.SetBackground(BaseColor.RED);
            //Paragraph p = new Paragraph(c);
            //document.Add(p);


        }

        private void MontaCabecalhoPdf(PdfWriter writer, Document document)
        {

            var gridInfo = FontFactory.GetFont("Avenir-Heavy",9f, 1);
            var gridInfo1 = FontFactory.GetFont("Avenir-Heavy_Bold", 9f);
            var fontNomeEmpresa = FontFactory.GetFont("Avenir-Black", 10f, 1, new BaseColor(0, 67, 28));
            var fontDetalheEmpresa = FontFactory.GetFont("Avenir-Light", 10f, new BaseColor(0, 67, 28));

            PdfContentByte cb = writer.DirectContent;
            var rect = new iTextSharp.text.Rectangle(580, 835, 20, 810); //Widht, Heighy,
                                                                         //rect.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                                                         //rect.BorderWidth = 5;
            rect.BackgroundColor = new BaseColor(0, 139, 67);
            //rect.BorderColor = new BaseColor(0, 139, 67);
            cb.Rectangle(rect);


            //IMAGEM
            //definimos as imagens do projeto
            //QUE SERÁ APRESENTADA NO CANTO DIREIRTO DO RELATORIO
            var image = iTextSharp.text.Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "/images/logo.png");
            image.ScalePercent(100f);
            image.SetAbsolutePosition(document.PageSize.Width - 532, document.PageSize.Height - 150);
            document.Add(image);


            var pdfTable = new PdfPTable(11);
            //ESPAÇAMENTOS
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 11, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 11, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 11, Border = 0 });
            //ESPAÇAMENTOS

            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 4, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("ALLPARK EMPREEND. PARTIC. E SERV. S/A", fontNomeEmpresa)) { Colspan = 7, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 4, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("Demonstrativo do aluguel referente ao estacionamento: ", fontDetalheEmpresa)) { Colspan = 7, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 4, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("CENTRO DE INTEGRAÇÃO EMPRESA ESCOLA - CIEE", fontDetalheEmpresa)) { Colspan = 7, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 4, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("Referência: " + DateTime.Now.Date.ToString("MM/yyyy"), fontDetalheEmpresa)) { Colspan = 7, Border = 0 });

            //ESPAÇAMENTOS
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 11, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 11, Border = 0 });
            pdfTable.AddCell(new PdfPCell(new Phrase("\n")) { Colspan = 11, Border = 0 });
            //ESPAÇAMENTOS

            Phrase phraseFilial = new Phrase();
            phraseFilial.Add(
                new Chunk("Filial: ",new Font(gridInfo))
            );
            phraseFilial.Add(new Chunk(string.Format("{0} - {1}", "0933", "CIEE"), new Font(gridInfo1)));

            var cellInfo = new PdfPCell(phraseFilial);
            cellInfo.BorderColor = BaseColor.WHITE;
            cellInfo.Colspan = 11;
            cellInfo.BackgroundColor = new BaseColor(227, 226, 219);
            pdfTable.AddCell(cellInfo);

            Phrase phraseContrato = new Phrase();
            phraseContrato.Add(
                new Chunk("Contrato: ", new Font(gridInfo))
            );
            phraseContrato.Add(new Chunk("0300115900011AL", new Font(gridInfo1)));

            cellInfo = new PdfPCell(phraseContrato);
            cellInfo.BorderColor = BaseColor.WHITE;
            cellInfo.Colspan = 11;
            cellInfo.BackgroundColor = new BaseColor(227, 226, 219);
            pdfTable.AddCell(cellInfo);


            Phrase phraseCentroCusto = new Phrase();
            phraseCentroCusto.Add(
                new Chunk("Centro de Custo: ", new Font(gridInfo))
            );
            phraseCentroCusto.Add(new Chunk("001093301", new Font(gridInfo1)));

            //phraseCentroCusto.Add(new Chunk(" - Código da Garagem: ", new Font(gridInfo)));

            //phraseCentroCusto.Add(new Chunk(string.Format("{0}", "001093301"), new Font(gridInfo1)));

            cellInfo = new PdfPCell(phraseCentroCusto);
            cellInfo.BorderColor = BaseColor.WHITE;
            cellInfo.Colspan = 11;
            cellInfo.BackgroundColor = new BaseColor(227, 226, 219);
            pdfTable.AddCell(cellInfo);

            Phrase phraseLocador = new Phrase();
            phraseLocador.Add(
                new Chunk("Locador: ", new Font(gridInfo))
            );
            phraseLocador.Add(new Chunk("61.600.839/0001-55 - CENTRO DE INTEGRAÇÃO EMPRESA ESCOLA - CIEE", new Font(gridInfo1)));

            cellInfo = new PdfPCell(phraseLocador);
            cellInfo.BorderColor = BaseColor.WHITE;
            cellInfo.Colspan = 11;
            cellInfo.BackgroundColor = new BaseColor(227, 226, 219);
            pdfTable.AddCell(cellInfo);

            Phrase phraseEndereco = new Phrase();
            phraseEndereco.Add(
                new Chunk("Endereço: ", new Font(gridInfo))
            );
            phraseEndereco.Add(new Chunk(string.Format("{0} - {1} - {2} - {3}", "RUA TABAPUA, 540", "ITAIM BIBI", "SÃO PAULO", "SP"), new Font(gridInfo1)));

            cellInfo = new PdfPCell(phraseEndereco);
            cellInfo.BorderColor = BaseColor.WHITE;
            cellInfo.Colspan = 11;
            cellInfo.BackgroundColor = new BaseColor(227, 226, 219);
            pdfTable.AddCell(cellInfo);

            cellInfo = new PdfPCell(new Phrase("\n"));
            cellInfo.Border = 0;
            //cellImage.BorderColor = BaseColor.WHITE;
            cellInfo.Colspan = 11;
            pdfTable.AddCell(cellInfo);

            // imprime o pagagrafo no documento
            document.Add(pdfTable);

        }


        private void MontarContent(PdfWriter pdfWriter, Document document)
        {

            var fontDescricao = FontFactory.GetFont("Avenir-Light", 10f);
            var fontDescricaoTotal = FontFactory.GetFont("Avenir-Medium", 10f, 1);

            var fontValor = FontFactory.GetFont("Avenir-Light", 10f);
            var fontValorTotal = FontFactory.GetFont("Avenir-Light", 10f, 1);

            var pdfTable = new PdfPTable(11);

            var cell = new PdfPCell(new Phrase("\n"));
            cell.BorderColor = BaseColor.WHITE;
            cell.BorderColorBottom = BaseColor.BLACK;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("AVULSOS", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("20,000.00", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("CONVÊNIOS", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("5,750.00", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("MENSALISTAS", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("50,000.00", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("OUTRAS RECEITAS", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("0.00", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("FATURAMENTO BRUTO", fontDescricaoTotal));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            //cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("75,750.00", fontValorTotal));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("IMPOSTOS SOBRE FATURAMENTO - (14,25%)", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("-10,794.38", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("FATURAMENTO LÍQUIDO", fontDescricaoTotal));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("64,955.63", fontValorTotal));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("BASE DE CÁLCULO DO ALUGUEL", fontDescricaoTotal));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("64,955.63", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("\n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);



            cell = new PdfPCell(new Phrase("Faixa 1", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("Até 15.000,00 - 25%", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 4;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("3,750.00", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 5;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Faixa 2", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("De 15.000,01 a 30,000,00 - 45%", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 4;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("6,750.00", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 5;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Faixa 3", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("Acima de 30,000,00 - 60%", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 4;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("20,973.38", fontValor));
            cell.BorderColor = BaseColor.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 5;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("\n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("TOTAL DO ALUGUEL", fontDescricaoTotal));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("31,473.38", fontValorTotal));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("PISO MÍNIMO", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("30,000.00", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthTop = 0;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ACRÉSCIMOS / DECRÉSCIMOS", fontDescricao));
            cell.Colspan = 12;
            cell.Rowspan = 3;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 0;
            cell.UseVariableBorders = true;
            //cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            //cell.BorderWidthLeft = 0;
            //cell.BorderWidthRight = 0;
            //cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);

            //cell = new PdfPCell(new Phrase("\n"));
            ////AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            ////cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //cell.BorderColor = BaseColor.WHITE;
            //cell.Colspan = 11;
            //pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ACRÉSCIMOS / DECRÉSCIMOS", fontDescricao));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("0.00", fontValor));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;           
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 1f;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n \n"));
            //AQUI NOS PODEMOS ALINHAR NOSSO TEXTO DENTRO DA CELULA
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.WHITE;
            cell.Colspan = 11;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("TOTAL DO REPASSE", fontDescricaoTotal));
            cell.Colspan = 9;
            cell.Rowspan = 3;
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            pdfTable.AddCell(cell);


            cell = new PdfPCell(new Phrase("31,473.38", fontValorTotal));
            cell.UseVariableBorders = true;
            cell.BorderColorBottom = (BaseColor.BLACK);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.Rowspan = 3;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(cell);

            document.Add(pdfTable);
        }
    }
}
