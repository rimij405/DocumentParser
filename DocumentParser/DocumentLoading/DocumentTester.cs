using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace DocumentParser.DocumentLoading
{
	public static class DocumentTester
	{
		private static string fileName;

		public static void DocumentLoad()
		{
			// Get the file.
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.FilterIndex = 1;
			dialog.Filter = "Portable Document Format (Adobe PDF) (*.pdf)|*.pdf|Microsoft Word Document (2007+) (*.docx)|*.docx|Microsoft Word Document (2003+) (*.doc)|*.doc|All Files (*.*)|*.*";
			dialog.InitialDirectory = Directory.GetCurrentDirectory();
			dialog.Multiselect = false;
			dialog.CheckFileExists = true;

			do
			{
				Program.Write("Please open up a *.pdf, *.docx, or *.doc file to test.");

				var userConfirmation = dialog.ShowDialog();

				if (userConfirmation == DialogResult.OK)
				{
					if (File.Exists(dialog.FileName))
					{
						break;
					}
				}
				else
				{
					Program.Write("There was an error catching this file.");
					Program.Write("Check if the file exists.");
					Program.Exit(true);
				}
			} while (!File.Exists(dialog.FileName));

			fileName = dialog.FileName;
		}

		public static void BriefDocumentParser()
		{
			DocumentLoad();

			// The file now exists.
			FileStream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 8, false);

			using (StreamReader sr = new StreamReader(fStream))
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();
				int timeTillTimeOut = 5000; // milliseconds.
				string line;

				while ((line = sr.ReadLine()) != null)
				{
					double elapsedTime = sw.Elapsed.TotalSeconds;
					double timeLeft = TimeSpan.FromMilliseconds(timeTillTimeOut).TotalSeconds - elapsedTime;

					Program.Write(line);
					Program.Write("< Current time till time out: " + timeLeft + " seconds. >");

					if (timeLeft <= 0)
					{
						sw.Stop();
						Program.Pause("Reading timed out!");
						break;
					}
				}

				if (sw.Elapsed.TotalSeconds <= TimeSpan.FromMilliseconds(timeTillTimeOut).TotalSeconds)
				{
					Program.Write("Reading did not time out!");
				}

				sw.Reset();
				sr.Close();
			}
		}

		public static void DocumentConversion()
		{
			DocumentLoad();

			FileInfo info = new FileInfo(fileName);
			string input = info.FullName;
			string ext = info.Extension;
			string inputWithoutExtension = info.Name.Substring(0, info.Name.Length - ext.Length);
			string dir = info.Directory.FullName;
			string outputName = inputWithoutExtension + "_output.doc";
			string outputPath = dir + "\\" + outputName;

			Program.Write("Converting " + info.Name + " to " + outputName);

			Convert(input, outputPath, WdSaveFormat.wdFormatDocument);

			string outputName2 = inputWithoutExtension + "_output.xps";
			string outputPath2 = dir + "\\" + outputName2;
			Program.Write("Converting " + info.Name + " to " + outputName2);

			Convert(input, outputPath2, WdSaveFormat.wdFormatXPS);

			string outputName3 = inputWithoutExtension + "_output.pdf";
			string outputPath3 = dir + "\\" + outputName3;
			Program.Write("Converting " + info.Name + " to " + outputName3);

			Convert(input, outputPath3, WdSaveFormat.wdFormatPDF);

			Program.Pause("Check for errors.");
		}

		public static void DocumentParsing(FileInfo file)
		{
			using (DocX document = DocX.Load(file.FullName))
			{
				// Read through the headers.
				if (document.Headers != null)
				{
					Program.Write("Checking document headers:");
					// First header.
					if (document.Headers.first != null)
					{
						Program.Write("|\tReading even headers:");
						foreach (Novacode.Paragraph p in document.Headers.first.Paragraphs)
						{
							Program.Write(p.Text);
						}
					}

					// Even
					if (document.Headers.even != null)
					{
						Program.Write("|\tReading even headers:");
						foreach (Novacode.Paragraph p in document.Headers.even.Paragraphs)
						{
							Program.Write(p.Text);
						}
					}

					// Odd
					if (document.Headers.odd != null)
					{
						Program.Write("|\tReading odd headers:");
						foreach (Novacode.Paragraph p in document.Headers.odd.Paragraphs)
						{
							Program.Write(p.Text);
						}
					}
				}

				// Check paragraphs.
				if (document.Paragraphs != null)
				{
					Program.Write("Check paragraphs: ");

					int paras = 1;

					foreach (Novacode.Paragraph p in document.Paragraphs)
					{
						Program.Write("Paragraph #" + paras + ": ");
						Program.Write(p.Text);
						Program.Write();

						++paras;
					}
				}

				if (document.Tables != null)
				{
					Program.Write("Checking document tables: ");
					// Check the body.
					List<Novacode.Table> tables = document.Tables;

					if (tables.Count > 0)
					{
						foreach (Novacode.Table table in tables)
						{
							Program.Write(GetTableContents(table));
						}
					}
				}

				/*
				// Check tables.
				if (document.Tables != null)
				{
					Program.Write("Checking document tables: ");
					// Check the body.
					List<Novacode.Table> tables = document.Tables;
					int count = 1;

					foreach (Novacode.Table table in tables)
					{
						Program.Write("Table: " + count);
						Program.Write("" + table.TableCaption);
						Program.Write("" + table.TableDescription);
						Program.Write(table.ColumnCount + " columns, " + table.RowCount + " rows.");

						foreach (Novacode.Row row in table.Rows)
						{
							if (row != null)
							{
								bool hasTableHeader = false; 

								try
								{
									hasTableHeader = row.TableHeader;
								}
								catch
								{
									hasTableHeader = false;
								}


								if (hasTableHeader)
								{
									foreach (Novacode.Cell cell in row.Cells)
									{
										Program.Write("|* " + cell.Xml.Value + " *", false);
									}
								}
								else
								{
									foreach (Novacode.Cell cell in row.Cells)
									{
										Program.Write("| " + cell.Xml.Value + "", false);
									}
								}
							}
						}

						++count;
					}
				}
				*/



				// Check footers.
				if (document.Footers != null)
				{
					Program.Write("Checking document footers:");
					// First footers.
					if (document.Footers.first != null)
					{
						Program.Write("|\tReading even footers:");
						foreach (Novacode.Paragraph p in document.Footers.first.Paragraphs)
						{
							Program.Write(p.Text);
						}
					}

					// Even
					if (document.Footers.even != null)
					{
						Program.Write("|\tReading even footers:");
						foreach (Novacode.Paragraph p in document.Footers.even.Paragraphs)
						{
							Program.Write(p.Text);
						}
					}

					// Odd
					if (document.Footers.odd != null)
					{
						Program.Write("|\tReading odd footers:");
						foreach (Novacode.Paragraph p in document.Footers.odd.Paragraphs)
						{
							Program.Write(p.Text);
						}
					}
				}
			}
		}

		public static void DocumentParsing()
		{
			FileInfo info = new FileInfo(fileName);
			string input = info.FullName;
			string ext = info.Extension;

			DocumentParsing(info);

		}

		public static void DocumentTesting()
		{
			DocumentLoad();

			FileInfo info = new FileInfo(fileName);
			string input = info.FullName;
			string ext = info.Extension;

			Stopwatch stopwatch = new Stopwatch();
			Program.Clear();
			Program.Write("Reading from " + info.Name);
			Program.Write();
			Program.Write("The full directory location is: " + info.FullName);
			int timeMarker = 0;
			stopwatch.Start();

			++timeMarker;
			Program.Write("Time Marker " + timeMarker + " [Retrieving Page]: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalMilliseconds + " ms.");
			PdfReader reader = new PdfReader(input);
			Dictionary<int, List<string>> document = new Dictionary<int, List<string>>();
			List<string> pageContent = new List<string>();
			int pageNumber = reader.NumberOfPages;
			string[] words;
			string line;

			for (int i = 1; i <= pageNumber; i++)
			{
				int lNumber = 0;
				timeMarker++;
				Program.Write("Time Marker " + timeMarker + " [Retrieving Page]: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalMilliseconds + " ms.");

				var text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());

				++timeMarker;
				Program.Write("Time Marker " + timeMarker + " [Creating List]: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalMilliseconds + " ms.");

				pageContent = new List<string>();

				words = text.Split('\n');
				for (int j = 0; j < words.Length; j++)
				{
					lNumber++;

					++timeMarker;
					double storeLineTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalMilliseconds;
					Program.Write("Time Marker " + timeMarker + " [Retrieving Line]: " + storeLineTime + " ms.");

					line = Encoding.UTF32.GetString(Encoding.UTF32.GetBytes(words[j]));
					string response = "Page " + i + ", Line " + lNumber + " [" + storeLineTime + " ms] : \"" + line + "\".";

					pageContent.Add(response);
				}

				if (!document.ContainsKey(i))
				{

					++timeMarker;
					Program.Write("Time Marker " + timeMarker + " [Adding New Page]: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalMilliseconds + " ms.");
					document.Add(i, pageContent);
				}
				else
				{
					++timeMarker;
					Program.Write("Time Marker " + timeMarker + " [Overwriting Existing Page]: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalMilliseconds + " ms.");
					document[i] = pageContent;
				}
			}

			stopwatch.Stop();
			Program.Write();
			Program.Write("Reading of " + info.Name + " completed. Total elapsed time: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds + " seconds.");

			Program.Pause("Will now display results from the storage.");

			if (document == null || document.Count < 0)
			{
				Program.Write("This document is empty.");
			}
			else
			{
				for (int page = 1; page <= document.Count; page++)
				{
					if (document.ContainsKey(page))
					{
						foreach (string value in document[page])
						{
							Program.Write(value);
						}
					}
					else
					{
						Program.Write("Page " + page + " is empty.");
					}
				}
			}
		}

		public static void CreateAndDisplayHelloWorldPDF()
		{
			// TODO
			// WRITING A BLANK PDF DOCUMENT
			PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();
			PdfSharp.Pdf.PdfPage page = document.AddPage();
			XGraphics gfx = XGraphics.FromPdfPage(page);

			// Create font.
			XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
			gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

			// File name.
			string fileName = "HelloWorld_test_output.pdf";
			document.Save(fileName);

			// Display the pdf.
			Process.Start(fileName);
		}


		public static void DocumentConversionStatus()
		{
			DocumentLoad();

			DocumentParsing();

			FileInfo info = new FileInfo(fileName);
			string input = info.FullName;
			string ext = info.Extension;

			if (ext == ".doc")
			{
				string inputWithoutExtension = info.Name.Substring(0, info.Name.Length - ext.Length);
				string dir = info.Directory.FullName;
				string outputName = inputWithoutExtension + "_output.docx";
				string outputPath = dir + "\\" + outputName;

				Program.Write("Converting " + info.Name + " to " + outputName);
				ConvertDoc(input);
			}
			else
			{
				string inputWithoutExtension = info.Name.Substring(0, info.Name.Length - ext.Length);
				string dir = info.Directory.FullName;
				string outputName = inputWithoutExtension + "_output.doc";
				string outputPath = dir + "\\" + outputName;

				Program.Write("Converting " + info.Name + " to " + outputName);
				Convert(input, outputPath, WdSaveFormat.wdFormatDocument);
			}

			Program.Pause("Check for errors.");

			DocumentParsing(info);
		}

		public static bool ConvertDoc(string input)
		{
			try
			{
				Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
				word.Visible = false;

				var inputFile = new FileInfo(input);
				var document = word.Documents.Open(inputFile.FullName);

				string outputName = inputFile.FullName.Replace(".doc", "_output.docx");
				document.SaveAs2(outputName, WdSaveFormat.wdFormatXMLDocument, CompatibilityMode: WdCompatibilityMode.wdWord2010);

				word.ActiveDocument.Close();
				word.Quit();

				return true;
			}
			catch (Exception e)
			{
				throw new Exception();
				return false;
			}
		}

		public static string GetTableContents(Novacode.Table baseTable)
		{
			return GetTableContents(baseTable, 0);
		}

		public static string GetTableContents(Novacode.Table table, int level)
		{
			PrintPipes("Checking table: ", level);
			if (table == null || level < 0 || table.Rows == null || table.Rows.Count < 0)
			{
				return "Empty table.";
			}
			else
			{
				string tableResponse = "";

				// If base case is done, check the next level down.
				foreach (Novacode.Row row in table.Rows)
				{
					// Check the row content.
					PrintPipes("Checking row. Status: ", level, false);
					if (row == null || row.Cells == null || row.Cells.Count < 0)
					{
						Program.Write("Empty row.");
						return "Empty row.";
					}
					else
					{
						Program.Write("Row content detected.");

						// Check the cells.
						foreach (Novacode.Cell cell in row.Cells)
						{
							string cellResponse = "";

							// Check the content of the cells.
							PrintPipes("Checking cell. Status: ", level, false);
							if (cell == null || String.IsNullOrEmpty(cell.Xml.Value) || cell.Paragraphs.Count < 0)
							{
								Program.Write("Empty cell.");
								cellResponse += "Empty cell.";
							}
							else
							{
								Program.Write("Cell content detected.");

								foreach (Novacode.Paragraph paragraph in cell.Paragraphs)
								{
									string subResponse = paragraph.Text;

									// Process the text.
									subResponse = subResponse.Replace(";", Environment.NewLine);

									cellResponse += GetPipes(level + 1) + "Content: " + subResponse + "\n";
								}
							}

							// Check the row for any nested tables.
							if (cell.Tables == null || cell.Tables.Count < 0)
							{
								foreach (Novacode.Table nestedTable in cell.Tables)
								{
									cellResponse += "Nested Contents: " + GetTableContents(nestedTable, (level + 1)) + "\n";
								}
							}

							tableResponse += cellResponse + "\n";
						}
						

					}
				}

				return tableResponse;
			}
		}

		public static string GetPipes(int numberOfTimes)
		{
			string pipes = "";

			for (int count = 0; count < numberOfTimes; count++)
			{
				pipes += " | ";
			}

			return pipes;
	}

		public static void PrintPipes(int numberOfTimes)
		{
			for (int count = 0; count < numberOfTimes; count++)
			{
				Program.Write(" | ", false);
			}
		}
		
		public static void PrintPipes(string message, int numberOfTimes)
		{
			PrintPipes(numberOfTimes);
			Program.Write(message);
		}
		
		public static void PrintPipes(string message, int numberOfTimes, bool newline)
		{
			PrintPipes(numberOfTimes);
			Program.Write(message, newline);
		}


		public static void Convert(string input, string output, WdSaveFormat format)
		{
			Word._Application oWord = new Word.Application();
			oWord.Visible = false;

			// Interop objects.
			object oMissing = System.Reflection.Missing.Value;
			object isVisible = true;
			object readOnly = false;
			object oInput = input;
			object oOutput = output;
			object oFormat = format;

			// Load a doc into instance of word.
			Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			oDoc.Activate();

			oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			oWord.Quit(ref oMissing, ref oMissing, ref oMissing);

			Program.Write("Pause. Converted document.");
		}

	}
}
