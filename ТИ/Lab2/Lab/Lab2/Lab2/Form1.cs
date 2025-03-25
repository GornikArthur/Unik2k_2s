using System.Data.Common;
using System.Linq;
using System.Text;

namespace Lab1
{
	public partial class Form1 : Form
	{
		static string alphabet = "01";
		private int alphabetSize = alphabet.Length;
		private bool isLoaded = false;
		private Shif new_shif = null;
		private byte[] fileBytes;
		public Form1()
		{
			InitializeComponent();
			RegTextBox.Enabled = true;
			SiphreButton.Enabled = false;
			SaveButton.Enabled = false;
			CreatedKeyTextBox.Multiline = true;
			InputTextBox.Multiline = true;
			OutputTextBox.Multiline = true;
		}
		private bool CheckTextBox(TextBox MyTextBox)
		{
			int oldPos = MyTextBox.SelectionStart;
			int lengthBefore = MyTextBox.Text.Length;

			string newText = MyTextBox.Text.ToUpper().Replace(" ", "");

			newText = new string(newText.Where(c => alphabet.Contains(c)).ToArray());

			MyTextBox.Text = newText;

			int lengthAfter = newText.Length;
			int delta = lengthBefore - lengthAfter;
			int newPos = Math.Max(0, oldPos - delta);

			MyTextBox.SelectionStart = newPos;
			MyTextBox.SelectionLength = 0;

			return MyTextBox.Text.Length == 27;
		}

		private void ResTextBox_TextChanged(object sender, EventArgs e)
		{
			if (InputTextBox.Text.Length > 0)
			{
				SaveButton.Enabled = true;
			}
			else
			{
				SaveButton.Enabled = false;
			}
		}
		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = "��� ����� (*.*)|*.*",
				Title = "��������� ����"
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
					using (BinaryWriter writer = new BinaryWriter(fileStream))
					{
						byte[] encryptedBytes = new_shif.GetRes();
						writer.Write(encryptedBytes);

						MessageBox.Show("���� ������� ��������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"������ ��� ����������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void LoadButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "��� ����� (*.*)|*.*",
				Title = "������� ����"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
					using (BinaryReader reader = new BinaryReader(fileStream))
					{
						long fileLength = fileStream.Length;
						fileBytes = new byte[fileLength];

						// ��������� ���� � �����
						fileBytes = reader.ReadBytes((int)fileLength);

						isLoaded = true;
						MessageBox.Show("���� ������� ��������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"������ ��� ������ �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
					isLoaded = false;
				}
			}
		}

		private void RegTextBox_TextChanged(object sender, EventArgs e)
		{
			SiphreButton.Enabled = CheckTextBox(RegTextBox) && isLoaded;
		}

		private string FormatBitsWithSpaces(string bits, int groupSize = 8)
		{
			StringBuilder formatted = new StringBuilder();
			for (int i = 0; i < bits.Length; i++)
			{
				formatted.Append(bits[i]);

				// ��������� ������ ����� ������ groupSize ���
				if ((i + 1) % groupSize == 0 && i != bits.Length - 1)
				{
					formatted.Append(" ");
				}
			}
			return formatted.ToString();
		}

		private void SiphreButton_Click(object sender, EventArgs e)
		{
			new_shif = new Shif(RegTextBox.Text, fileBytes);
			int bitLength = 56;

			string shiftedBits = new_shif.PrintShiftedBits();
			string inputBits = new_shif.PrintInputBits();
			string outputBits = new_shif.PrintOutputBits();

			CreatedKeyTextBox.Text = "";
			CreatedKeyTextBox.Text += $"������ {Math.Min(shiftedBits.Length, bitLength)} ��� ����������� �����:" + Environment.NewLine;
			CreatedKeyTextBox.Text += FormatBitsWithSpaces(shiftedBits.Substring(0, Math.Min(shiftedBits.Length, bitLength)));

			InputTextBox.Text = "";
			InputTextBox.Text += $"������ {Math.Min(inputBits.Length, bitLength)} ���:" + Environment.NewLine;
			InputTextBox.Text += FormatBitsWithSpaces(inputBits.Substring(0, Math.Min(inputBits.Length, bitLength))) + Environment.NewLine + Environment.NewLine;
			InputTextBox.Text += $"��������� {Math.Min(inputBits.Length, bitLength)} ���:" + Environment.NewLine;
			InputTextBox.Text += FormatBitsWithSpaces(inputBits.Substring(inputBits.Length - Math.Min(bitLength, inputBits.Length)));

			OutputTextBox.Text = "";
			OutputTextBox.Text += $"������ {Math.Min(outputBits.Length, bitLength)} ���:" + Environment.NewLine;
			OutputTextBox.Text += FormatBitsWithSpaces(outputBits.Substring(0, Math.Min(outputBits.Length, bitLength))) + Environment.NewLine + Environment.NewLine;
			OutputTextBox.Text += $"��������� {Math.Min(outputBits.Length, bitLength)} ���:" + Environment.NewLine;
			OutputTextBox.Text += FormatBitsWithSpaces(outputBits.Substring(outputBits.Length - Math.Min(bitLength, outputBits.Length)));

			ActionTextBox.Text = "";
			StringBuilder actionText = new StringBuilder();

			for (int i = 0; i < shiftedBits.Length; i++)
			{
				actionText.AppendLine($"�������� {i + 1}. ��������� ���: {shiftedBits[i]}");

				// Every 300 iterations, push the chunk to the TextBox
				if (i % 300 == 0 && i != 0)
				{
					ActionTextBox.AppendText(actionText.ToString());
					actionText.Clear(); // Proper way to reset StringBuilder
					Application.DoEvents(); // Keeps UI responsive during large loops
				}
			}

			// Push remaining content
			ActionTextBox.AppendText(actionText.ToString());


		}
	}
}
