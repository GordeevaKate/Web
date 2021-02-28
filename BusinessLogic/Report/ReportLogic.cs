using BusinessLogic.ViewModel;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BusinessLogic.Report
    {
        public class ReportLogic
        {




            public void SaveFile(string fileName, ServiceViewModel service, PacientViewModel pacient, 
                DoctorViewModel doctor)
            {
                   CreateDoc(new PdfInfo
                    {
                        FileName = fileName,
                            Title = "Рецепт",
                       service = service,
                       pacient = pacient,
                       Doctor =doctor

                   });
            }















        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph.Style = "Normal";
            if (info.Title == "Рецепт")
            {
                Paragraph paragraph1 = section.AddParagraph($"на лекарство {info.service.Name} пациенту {info.pacient.FIO}");
                paragraph1.Format.SpaceAfter = "1cm";
                paragraph1.Format.Alignment = ParagraphAlignment.Center;
                paragraph1.Style = "NormalTitle";
                paragraph1.Style = "Normal";
                Paragraph paragraph3 = section.AddParagraph($"Дата {DateTime.Now}");
                paragraph3.Format.SpaceAfter = "1cm";
                paragraph3.Format.Alignment = ParagraphAlignment.Left;
                paragraph3.Style = "NormalTitle";
                paragraph3.Style = "Normal";
                Paragraph paragraph4 = section.AddParagraph($"Подпись {info.Doctor.Login}");
                paragraph4.Format.SpaceAfter = "1cm";
                paragraph4.Format.Alignment = ParagraphAlignment.Left;
                paragraph4.Style = "NormalTitle";
                paragraph4.Style = "Normal";
            }
            if (info.counts!=null)
            {
                var table = document.LastSection.AddTable();
                List<string> columns = new List<string> { "5cm" };
                var colons = new List<string> { "Пациент-диагноз" };
                foreach (var d in info.diagnosis)
                {
                    columns.Add("3cm");
                    colons.Add(d.Name);
                }

                foreach (var elem in columns)
                {
                    table.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = colons,
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                
                foreach (var pac in info.pacients)
                {
                    var texts = new List<string> {$"{pac.FIO}" };
                    foreach (var d in info.diagnosis)
                    {
                        texts.Add($"{info.counts.Where(x => x.PacientId==pac.Id).ToList().Count}");
                    }
                    CreateRow(new PdfRowParameters
                    {
                        Table = table,
                        Texts = texts,
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
               
            }
            if (info.heals != null)
            {
                var table = document.LastSection.AddTable();
                List<string> columns = new List<string> { "5cm", "5cm", "4cm" };

                foreach (var elem in columns)
                {
                    table.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = info.Colon,
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                for (int i = 1; i <= 12; i++)
                {
                   
                  string  []month = new string []
                  { "","январь","февраль","март", "апрель","май", "июнь","июль","август","сентябрь","октябрь","ноябрь","декабрь"};
                   if(i==1)
                    CreateRow(new PdfRowParameters
                    {
                        Table = table,
                        Texts = new List<string>
                       {   month[i]
                          ,
                          Convert.ToString( info.heals.ToList().Where(x => x.Data.Month==new DateTime(2020,i,i).Month).ToList().Count),
                      ""
                       },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    else
                    {
                        CreateRow(new PdfRowParameters
                        {
                            Table = table,
                            Texts = new List<string>
                       {   month[i]
                          ,
                          Convert.ToString( info.heals.ToList().Where(x => x.Data.Month==new DateTime(2020,i,i).Month).ToList().Count),
                       Convert.ToString( info.heals.ToList().Where(x => x.Data.Month==new DateTime(2020,i,i).Month).ToList().Count -info.heals.ToList().Where(x => x.Data.Month==new DateTime(2020,i-1,i-1).Month).ToList().Count ),
                       },
                            Style = "Normal",
                            ParagraphAlignment = ParagraphAlignment.Left
                        });
                    }
                }
               
            }
            if (info.services != null)
            {
                var table = document.LastSection.AddTable();
                List<string> columns = new List<string> { "4cm", "5cm", "4cm", "3cm" };

                foreach (var elem in columns)
                {
                    table.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = info.Colon,
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                foreach (var dia in info.diagnosis)
                {
                    foreach (var ds in info.ds)
                        foreach (var serv in info.services)
                    {
                        if (ds.DiagnosisId == dia.Id && ds.ServiceId==serv.Id)
                            CreateRow(new PdfRowParameters
                            {
                                Table = table,
                                Texts = new List<string>
                       {
                          dia.Name,

                             serv.Name,
                           Convert.ToString(  serv.Status),
                             Convert.ToString(serv.Cena)
                       },
                                Style = "Normal",
                                ParagraphAlignment = ParagraphAlignment.Left
                            });
                    }
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }

        public void SaveFileService(string fileName, List<ServiceViewModel> services, List<DiagnosisServiceViewModel> ds, List<DiagnosisViewModel> dia)
        {
            CreateDoc(new PdfInfo
            {
                Colon = new List<string> { "Диагноз","Название", "Статус",  "Цена" },
                FileName = fileName,
                Title = "Прейскурант цен за услуги",
                services = services,
                ds=ds,
                diagnosis=dia

            });
        }

        public void SaveFileAnalitic(string fileName, List<HealingViewModel> heals,string Data)
        {

            CreateDoc(new PdfInfo
            {
                Colon = new List<string> { "Месяц", "Количевство", "Разница" },
                FileName = fileName,
                Title = $"Аналитический отчет за {Data}",
                heals = heals
            });
        }
        public class counts
        {
            public int DiagnosisId { get; set; }
            public int PacientId { get; set; }
            public int Count { get; set; }
        }
        public void SaveFilePere(string fileName,
            List<DiagnosisViewModel> diagnoses, List<PacientViewModel> pacients, 
            List<counts> counts, string Data)
        {
            CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = $"Перекрестный отчет за {Data}",
                diagnosis = diagnoses,
                pacients=pacients,
                counts=counts
            });
        }
    }
    }
